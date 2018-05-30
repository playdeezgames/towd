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
using TombsCommon;

namespace TombsEditor
{
    public class EditMenuState : CyMenuState<EditorState>
    {
        private static readonly List<string> _items = new List<string>() { "Back", "Generators" };

        public EditMenuState(StateManager<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFont font)
            :base(manager, screen, font, "Edit Menu", _items)
        {
        }

        public override void OnItemSelected(int itemIndex)
        {
            switch(itemIndex)
            {
                case 0:
                    Manager.Pop();
                    break;
                case 1:
                    Manager.Push(EditorState.GeneratorsMenu);
                    break;
            }
        }

    }
}
