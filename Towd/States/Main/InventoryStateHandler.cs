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
        private ListBoxControl _listBox;
        private Dictionary<int, string> _itemTable = new Dictionary<int, string>();
        public InventoryStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Inventory", CyColor.White);
            _listBox = new ListBoxControl(
                this,
                true,
                CyRect.Create(0, font.Height, Width, Height - font.Height),
                font,
                new string[]
                {
                },
                0,
                CyColor.Black,
                CyColor.White,
                OnListBoxActivate);
        }

        private void OnListBoxActivate(int selected)
        {
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
            List<string> listBoxItems = new List<string>();
            _itemTable.Clear();
            foreach(var entry in World.GetAvatarCreatureInstance().Items)
            {
                var itemType = World.Items[entry.Key];
                _itemTable[listBoxItems.Count()] = entry.Key;
                listBoxItems.Add($"{itemType.DisplayName} x {entry.Value}");
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
