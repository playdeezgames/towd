using Common;
using MonoGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Towd
{
    //TODO: have some content
    public class InventoryStateHandler : TowdStateHandler
    {
        private enum InventoryAction
        {
            Examine,
            Equip,
            Eat,
            Drop,
            Unequip
        }

        private ListBoxControl<string> _itemListBox;
        private ListBoxControl<InventoryAction> _contextListBox;
        public InventoryStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Inventory", CyColor.White);
            _itemListBox = new ListBoxControl<string>(
                this,
                true,
                CyRect.Create(0, font.Height, Width, Height - font.Height),
                font,
                new ListBoxItem<string>[]
                {
                },
                0,
                CyColor.Black,
                CyColor.White,
                OnListBoxActivate);
            _contextListBox = new ListBoxControl<InventoryAction>(
                this,
                false,
                CyRect.Create(Width/2, font.Height, Width, Height - font.Height),
                font,
                new ListBoxItem<InventoryAction>[]
                {
                },
                0,
                CyColor.Black,
                CyColor.White,
                OnContextListBoxActivate);
        }

        private void OnContextListBoxActivate(int selected)
        {
            var action = _contextListBox.Items.ToList()[selected].Meta;
            var itemName = _itemListBox.Items.ToList()[_itemListBox.Selected].Meta;
            switch (action)
            {
                case InventoryAction.Equip:
                    World.GetAvatarCreatureInstance().GetEquipped().Add(itemName);
                    RefreshItemListBox(itemName);
                    break;
                case InventoryAction.Unequip:
                    World.GetAvatarCreatureInstance().GetEquipped().Remove(itemName);
                    RefreshItemListBox(itemName);
                    break;
            }

            _itemListBox.Focus();
            _contextListBox.Enabled = false;
            _itemListBox.Foreground = CyColor.Black;
        }

        private void OnListBoxActivate(int selected)
        {
            if(selected<_itemListBox.Items.Count())
            {
                var itemName = _itemListBox.Items.ToList()[selected].Meta;
                var avatarCreatureInstance = World.GetAvatarCreatureInstance();
                var itemDescriptor = World.Items[itemName];

                List<ListBoxItem<InventoryAction>> contextMenu = new List<ListBoxItem<InventoryAction>>();
                contextMenu.Add(new ListBoxItem<InventoryAction> { Meta= InventoryAction.Examine, Caption="Examine" });

                if(avatarCreatureInstance.HasEquipped(itemName))
                {
                    contextMenu.Add(new ListBoxItem<InventoryAction> { Meta = InventoryAction.Unequip, Caption = "Unequip" });
                }
                else
                {
                    if(avatarCreatureInstance.CanEquip(itemDescriptor, World.Items))
                    {
                        contextMenu.Add(new ListBoxItem<InventoryAction> { Meta = InventoryAction.Equip, Caption = "Equip" });
                    }
                }
                if (avatarCreatureInstance.CanEat(itemDescriptor))
                {
                    contextMenu.Add(new ListBoxItem<InventoryAction> { Meta = InventoryAction.Eat, Caption = "Eat" });
                }

                contextMenu.Add(new ListBoxItem<InventoryAction> { Meta = InventoryAction.Drop, Caption = "Drop" });
                _contextListBox.Items = contextMenu;
                _contextListBox.Selected = 0;


                _itemListBox.Foreground = CyColor.LightGray;
                _contextListBox.Enabled = true;
                _contextListBox.Focus();

            }
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Red:
                    if (_contextListBox.Enabled)
                    {
                        _itemListBox.Focus();
                        _contextListBox.Enabled = false;
                        _itemListBox.Foreground = CyColor.Black;
                    }
                    else
                    {
                        SetState(TowdState.Room);
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
            RefreshItemListBox(string.Empty);
            _itemListBox.Focus();
        }

        private void RefreshItemListBox(string itemName)
        {
            List<ListBoxItem<string>> listBoxItems = new List<ListBoxItem<string>>();
            var creatureInstance = World.GetAvatarCreatureInstance();
            var selected = 0;
            foreach (var entry in creatureInstance.GetItems())
            {
                if(entry.Key== itemName)
                {
                    selected = listBoxItems.Count();
                }
                var itemDescriptor = World.Items[entry.Key];
                var meta = entry.Key;
                var prefix = (creatureInstance.HasEquipped(entry.Key)) ? ("E-") : ("");
                listBoxItems.Add(ListBoxItem<string>.Create(meta, $"{prefix}{itemDescriptor.DisplayName} x {entry.Value}"));
            }
            _itemListBox.Items = listBoxItems;
            _itemListBox.Selected = selected;
        }

        protected override void OnStop()
        {
            _itemListBox.Blur();
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
        }
    }
}
