using Common;
using Engine;
using Microsoft.VisualBasic;
using MonoGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Towd
{
    public class TerrainChooseRoleStateHandler : TowdStateHandler
    {
        private ListBoxControl _listBox;
        public TerrainChooseRoleStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var items = Enum.GetValues(typeof(TileRole)).Cast<TileRole>().Select(x => x.ToString());
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Role:", CyColor.White);
            _listBox = new ListBoxControl(
                this, 
                true, 
                CyRect.Create(0, font.Height, Width, Height-font.Height), 
                font,
                items,                
                0, 
                CyColor.Black, 
                CyColor.White, 
                OnListBoxActivate);
        }

        private void OnListBoxActivate(int selected)
        {
            switch(selected)
            {
                default:
                    var terrain = World.Terrains[EditorState.Terrain];
                    terrain.Role = (TileRole)_listBox.Selected;
                    SetState(TowdState.EditTerrain);
                    break;
            }
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Red:
                    SetState(TowdState.EditTerrain);
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
            var terrain = World.Terrains[EditorState.Terrain];
            _listBox.Focus();
            _listBox.Selected = (int)terrain.Role;
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
