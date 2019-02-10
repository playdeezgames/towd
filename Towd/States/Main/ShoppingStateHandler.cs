using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Towd
{
    internal class ShoppingStateHandler : TowdStateHandler
    {
        private ListBoxControl<string> _listBox;
        private LabelControl _statusLabel;
        public ShoppingStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new FilledBoxControl(this, true, CyRect.Create(0, Height - font.Height, Width, font.Height), CyColor.LightGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Shoppe", CyColor.White);
            _statusLabel = new LabelControl(this, true, CyPoint.Create(0, Height-font.Height), font, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CyColor.DarkGray);
            _listBox = new ListBoxControl<string>(
                this,
                true,
                CyRect.Create(0, font.Height, Width, Height - font.Height * 2),
                font,
                new ListBoxItem<string>[]
                {
                },
                0,
                CyColor.Black,
                CyColor.White,
                OnListBoxActivate);
        }

        private void OnListBoxActivate(int selected)
        {
            switch(World.AvatarStatus.Shopping.State)
            {
                case Engine.ShoppeState.Initial:
                    OnInitialListBoxActivate(selected);
                    break;
                case Engine.ShoppeState.Buying:
                    OnBuyingListBoxActivate(selected);
                    break;
                case Engine.ShoppeState.Selling:
                    OnSellingListBoxActivate(selected);
                    break;
            }
        }

        private void OnSellingListBoxActivate(int selected)
        {
            var selectedItem = _listBox.Items.ToList()[selected];
            if(string.IsNullOrEmpty(selectedItem.Meta))
            {
                World.AvatarStatus.SetShopping(World.AvatarStatus.Shopping.ShoppeName, Engine.ShoppeState.Initial);
                UpdateMenu("Sell");
            }
            else
            {
                var itemDescriptor = World.Items[selectedItem.Meta];
                World.GetAvatarCreatureInstance().RemoveItem(selectedItem.Meta, 1);
                World.GetAvatarCreatureInstance().Money += itemDescriptor.SellPrice;
                UpdateMenu(selectedItem.Meta);
            }
        }

        private void OnBuyingListBoxActivate(int selected)
        {
            var selectedItem = _listBox.Items.ToList()[selected];
            if (string.IsNullOrEmpty(selectedItem.Meta))
            {
                World.AvatarStatus.SetShopping(World.AvatarStatus.Shopping.ShoppeName, Engine.ShoppeState.Initial);
                UpdateMenu("Buy");
            }
            else
            {
                var itemDescriptor = World.Items[selectedItem.Meta];
                if(World.GetAvatarCreatureInstance().Money>= itemDescriptor.BuyPrice)
                {
                    World.GetAvatarCreatureInstance().AddItem(selectedItem.Meta, 1);
                    World.GetAvatarCreatureInstance().Money -= itemDescriptor.BuyPrice;
                    UpdateMenu(selectedItem.Meta);
                }
                else
                {
                    _statusLabel.Text = "Not enough g!";
                }
            }
        }

        private void OnInitialListBoxActivate(int selected)
        {
            var selectedItem = _listBox.Items.ToList()[selected];
            switch(selectedItem.Meta)
            {
                case "Buy":
                    World.AvatarStatus.SetShopping(World.AvatarStatus.Shopping.ShoppeName, Engine.ShoppeState.Buying);
                    UpdateMenu(string.Empty);
                    break;
                case "Sell":
                    World.AvatarStatus.SetShopping(World.AvatarStatus.Shopping.ShoppeName, Engine.ShoppeState.Selling);
                    UpdateMenu(string.Empty);
                    break;
                case "Leave":
                    World.AvatarStatus.SetNormal();
                    SetState(TowdState.Room);
                    break;
            }
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Red:
                    switch (World.AvatarStatus.Shopping.State)
                    {
                        case Engine.ShoppeState.Initial:
                            World.AvatarStatus.SetNormal();
                            SetState(TowdState.Room);
                            break;
                        case Engine.ShoppeState.Buying:
                            World.AvatarStatus.SetShopping(World.AvatarStatus.Shopping.ShoppeName, Engine.ShoppeState.Initial);
                            UpdateMenu("Buy");
                            break;
                        case Engine.ShoppeState.Selling:
                            World.AvatarStatus.SetShopping(World.AvatarStatus.Shopping.ShoppeName, Engine.ShoppeState.Initial);
                            UpdateMenu("Sell");
                            break;
                    }
                    return true;
                default:
                    return false;
            }
        }

        protected override IResult OnMessage(IMessage message)
        {
            return null;
        }

        protected override void OnStart()
        {
            UpdateMenu(string.Empty);
            _listBox.Focus();
        }

        private void UpdateMenu(string lastSelected)
        {
            switch(World.AvatarStatus.Shopping.State)
            {
                case Engine.ShoppeState.Initial:
                    SetupInitialMenu(lastSelected);
                    break;
                case Engine.ShoppeState.Buying:
                    SetupBuyingMenu(lastSelected);
                    break;
                case Engine.ShoppeState.Selling:
                    SetupSellingMenu(lastSelected);
                    break;
            }
        }

        private void SetupSellingMenu(string lastSelected)
        {
            //Remember: Tagon is selling to shoppe, so this is a list of what the shoppe buys and Tagon has
            List<ListBoxItem<string>> listBoxItems = new List<ListBoxItem<string>>();
            listBoxItems.Add(ListBoxItem<string>.Create(string.Empty, "Never Mind..."));
            int selected = 0;

            var inventory = World.GetAvatarRoom().GetShoppeInventory(World.GetAvatarStatus().Shopping.ShoppeName);
            foreach (var inventoryItem in inventory.Buying.Where(x=>World.GetAvatarCreatureInstance().HasItem(x)))
            {
                var itemDescriptor = World.Items[inventoryItem];
                if (lastSelected == inventoryItem)
                {
                    selected = listBoxItems.Count();
                }
                listBoxItems.Add(ListBoxItem<string>.Create(inventoryItem, $"{itemDescriptor.DisplayName} @ {itemDescriptor.SellPrice}g"));
            }

            _listBox.Items = listBoxItems;
            _listBox.Selected = selected;
            _statusLabel.Text = $"Money: {World.GetAvatarCreatureInstance().Money}g";
        }

        private void SetupBuyingMenu(string lastSelected)
        {
            //Remember: Tagon is buying from the shoppe, so this is a list of what the shopped sells and Tagon
            List<ListBoxItem<string>> listBoxItems = new List<ListBoxItem<string>>();
            listBoxItems.Add(ListBoxItem<string>.Create(string.Empty, "Never Mind..."));
            int selected = 0;
            var inventory = World.GetAvatarRoom().GetShoppeInventory(World.GetAvatarStatus().Shopping.ShoppeName);
            foreach (var inventoryItem in inventory.Selling)
            {
                var itemDescriptor = World.Items[inventoryItem];
                if(lastSelected == inventoryItem)
                {
                    selected = listBoxItems.Count();
                }
                listBoxItems.Add(ListBoxItem<string>.Create(inventoryItem, $"{itemDescriptor.DisplayName} @ {itemDescriptor.BuyPrice}g"));

            }

            _listBox.Items = listBoxItems;
            _listBox.Selected = selected;
            _statusLabel.Text = $"Money: {World.GetAvatarCreatureInstance().Money}g";
        }

        private void SetupInitialMenu(string lastSelected)
        {
            List<ListBoxItem<string>> listBoxItems = new List<ListBoxItem<string>>();
            var shoppe = World.GetAvatarRoom().GetShoppeInventory(World.AvatarStatus.Shopping.ShoppeName);
            int selected = 0;
            if(shoppe.Buying.Any(x=>World.GetAvatarCreatureInstance().HasItem(x)))
            {
                if (lastSelected == "Sell")
                {
                    selected = listBoxItems.Count();
                }
                listBoxItems.Add(ListBoxItem<string>.Create("Sell","Sell..."));
            }
            if(shoppe.Selling.Any())
            {
                if (lastSelected == "Buy")
                {
                    selected = listBoxItems.Count();
                }
                listBoxItems.Add(ListBoxItem<string>.Create("Buy", "Buy..."));
            }
            listBoxItems.Add(ListBoxItem<string>.Create("Leave", "Leave"));
            _listBox.Items = listBoxItems;
            _listBox.Selected = selected;
            _statusLabel.Text = $"What'll it be?";
        }

        protected override void OnStop()
        {
            _listBox.Blur();
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
        }
    }
}