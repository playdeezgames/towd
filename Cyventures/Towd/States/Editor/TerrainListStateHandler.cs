using Common;
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
    public class TerrainListStateHandler : TowdStateHandler
    {
        private ListBoxControl _listBox;
        public TerrainListStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Terrains:", CyColor.White);
            _listBox = new ListBoxControl(
                this, 
                true, 
                CyRect.Create(0, font.Height, Width, Height-font.Height), 
                font, 
                new string[] 
                {
                    "<Go Back>",
                    "<New>"
                }, 
                0, 
                CyColor.Black, 
                CyColor.White, 
                OnListBoxActivate);
        }

        private void OnListBoxActivate(int selected)
        {
            switch(selected)
            {
                case 0://go back
                    SetState(TowdState.Editor);
                    break;
                case 1://new terrain
                    DoNewTerrain();
                    break;
                default:
                    EditorState.Terrain = _listBox.Items.ToList()[selected];
                    SetState(TowdState.EditTerrain);
                    break;
            }
        }

        private void DoNewTerrain()
        {
            var name = Interaction.InputBox("New Name?","Create Terrain...");
            if(!string.IsNullOrEmpty(name))
            {
                World.Terrains[name] = new Engine.Terrain() { ResourceIdentifier=EditorState.DefaultImageResourceIdentifier };
                EditorState.Terrain = name;
                SetState(TowdState.EditTerrain);
            }
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Red:
                    SetState(TowdState.Editor);
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
            _listBox.Focus();
            RefreshTerrainsList();
        }

        private void RefreshTerrainsList()
        {
            List<string> terrainsList = _listBox.Items.Take(2).ToList();
            terrainsList.AddRange(World.Terrains.Keys.OrderBy(x=>x));
            _listBox.Items = terrainsList;
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
