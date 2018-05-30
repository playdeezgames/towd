using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using EditorCommon;
using Microsoft.Xna.Framework;

namespace MapEditor
{
    public class MainMenuState : CyMenuState<EditorState>
    {
        private static readonly List<string> _items = new List<string>() { "Edit", "Tileset...", "New...", "Open...","Save...","Quit" };

        public MainMenuState(StateManager<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFont font)
            :base(manager, screen, font, "Map Editor", _items)
        {
        }

        public override void OnItemSelected(int itemIndex)
        {
            switch(itemIndex)
            {
                case 0:
                    Manager.Set(EditorState.Edit);
                    break;
                case 1:
                    DoLoadTileSet();
                    break;
                case 2:
                    Manager.Set(EditorState.Create);
                    break;
                case 3:
                    DoLoad();
                    break;
                case 4:
                    DoSave();
                    break;
                case 5:
                    Manager.Set(EditorState.ConfirmQuit);
                    break;
            }
        }

        private void DoSave()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            var result = dialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                Utility.Save(Data.Map,dialog.FileName);
            }
        }

        private void DoLoad()
        {
            OpenFileDialog dialog = new OpenFileDialog() { };
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Data.Map = Utility.Load<TileMap<int>>(dialog.FileName);
            }
        }

        private void DoLoadTileSet()
        {
            OpenFileDialog dialog = new OpenFileDialog() { };
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Data.TileSet = Utility.Load<CyBitmapSequence>(dialog.FileName);
            }
        }
    }
}
