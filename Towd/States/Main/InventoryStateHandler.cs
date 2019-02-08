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
        private ListBoxControl<string> _listBox;
        public InventoryStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Inventory", CyColor.White);
            _listBox = new ListBoxControl<string>(
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
        }

        private void OnListBoxActivate(int selected)
        {
            //TODO:
            //context menu
            //equip
            //unequip
            //eat
            //discard/drop?
            //examine?
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Red:
                    SetState(TowdState.Room);
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
            List<ListBoxItem<string>> listBoxItems = new List<ListBoxItem<string>>();
            var creatureInstance = World.GetAvatarCreatureInstance();
            foreach (var entry in creatureInstance.GetItems())
            {
                var itemDescriptor = World.Items[entry.Key];
                var meta = entry.Key;
                var prefix = (creatureInstance.HasEquipped(entry.Key)) ? ("E-") : ("");
                listBoxItems.Add(ListBoxItem<string>.Create(meta, $"{prefix}{itemDescriptor.DisplayName} x {entry.Value}"));
            }
            _listBox.Items = listBoxItems;
            _listBox.Selected = 0;
            _listBox.Focus();
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
