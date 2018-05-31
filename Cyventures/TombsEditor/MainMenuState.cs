using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using EditorCommon;
using EngineCommon;
using Microsoft.Xna.Framework;
using MonoGameCommon;
using TombsCommon;

namespace TombsEditor
{
    public class MainMenuState : CyMenuState<EditorState>
    {
        private static readonly List<string> _items = new List<string>() { "Edit","New...","Load...","Save...","Quit" };

        public MainMenuState(StateManagerOld<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFontOld font)
            :base(manager, screen, font, "Editor of Woeful Doom!", _items)
        {
        }

        public override void OnItemSelected(int itemIndex)
        {
            switch(itemIndex)
            {
                case 0:
                    Manager.Push(EditorState.EditMenu);
                    break;
                case 1:
                    Data.World = new World();
                    break;
                case 2:
                    DoLoad();
                    break;
                case 3:
                    DoSave();
                    break;
                case 4:
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
                Utility.Save(Data.World, dialog.FileName);
            }
        }

        private void DoLoad()
        {
            OpenFileDialog dialog = new OpenFileDialog() { };
            var result = dialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                Data.World = Utility.Load<World>(dialog.FileName);
            }
        }
    }
}
