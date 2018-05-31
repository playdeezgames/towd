using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using EditorCommon;
using Microsoft.Xna.Framework;
using MonoGameCommon;

namespace TombsPlayer
{
    public class MainMenuState : CyMenuState<GameState>
    {
        private static readonly List<string> _items = new List<string>() { "Play","Help...","New...","Load...","Save...","Options...","Quit" };

        public MainMenuState(StateManager<GameState, Command> manager, ColorBuffer<CyColor> screen, CyFont font)
            :base(manager, screen, font, "Tombs of Woeful Doom!", _items)
        {
        }

        public override void OnItemSelected(int itemIndex)
        {
            switch(itemIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    DoLoad();
                    break;
                case 4:
                    DoSave();
                    break;
                case 5:
                    break;
                case 6:
                    Manager.Set(GameState.ConfirmQuit);
                    break;
            }
        }

        private void DoSave()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            var result = dialog.ShowDialog();
            if(result == DialogResult.OK)
            {
            }
        }

        private void DoLoad()
        {
            OpenFileDialog dialog = new OpenFileDialog() { };
            var result = dialog.ShowDialog();
            if(result == DialogResult.OK)
            {
            }
        }
    }
}
