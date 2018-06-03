using Common;
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
    public class TerrainEditStateHandler : TowdStateHandler
    {
        private ListBoxControl _listBox;
        private Dictionary<bool, string[]> _listBoxItems = new Dictionary<bool, string[]>()
        {
            [true] = new string[]
                {
                    "<Go Back>",
                    "Role...",
                    "Image..."
                },
            [false] = new string[]
                {
                    "<Go Back>",
                    "Role...",
                    "Image...",
                    "Name...",
                    "<Delete>"
                }
        };
        public TerrainEditStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width/2, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Edit Terrain:", CyColor.White);
            new LabelControl(this, true, CyPoint.Create(Width / 2, 0), font, "Name:", CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(Width / 2, font.Height * 2), font, "Role:", CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(Width / 2, font.Height * 4), font, "Image:", CyColor.DarkGray);
            _listBox = new ListBoxControl(
                this, 
                true, 
                CyRect.Create(0, font.Height, Width/2, Height-font.Height), 
                font, 
                new string[] 
                {
                    "<Go Back>",
                    "Role...",
                    "Image...",
                    "Name...",
                    "<Delete>"
                }, 
                0, 
                CyColor.Black, 
                CyColor.LightGray, 
                OnListBoxActivate);
        }

        private void OnListBoxActivate(int selected)
        {
            switch(selected)
            {
                case 0://go back
                    SetState(TowdState.TerrainsList);
                    break;
                case 1://role
                    break;
                case 2://image
                    break;
                case 3://name
                    break;
                case 4://delete
                    break;
            }
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Red:
                    SetState(TowdState.TerrainsList);
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
            _listBox.Items = _listBoxItems[World.IsTerrainInUse(EditorState.Terrain)];
            _listBox.Selected = 0;
        }

        protected override void OnStop()
        {
            _listBox.Blur();
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
            CyFont font = FontManager[TowdFont.Large];
            var terrain = World.Terrains[EditorState.Terrain];
            font.Draw(pixelWriter, CyColor.Black, Width / 2, font.Height, EditorState.Terrain , clipRect);
            font.Draw(pixelWriter, CyColor.Black, Width / 2, font.Height*3, terrain.Role.ToString(), clipRect);
            Bitmap<CyColor> bitmap = BitmapSequenceManager[terrain.ResourceIdentifier][terrain.ResourceIndex];
            bitmap.Draw(pixelWriter, CyPoint.Create(Width / 2, font.Height * 5), x => true, clipRect);

        }
    }
}
