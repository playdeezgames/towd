using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using EditorCommon;
using EngineCommon;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using TombsCommon;

namespace TombsEditor
{
    public class GeneratorsMenuState : CyMenuState<EditorState>
    {
        private static readonly List<string> _items = new List<string>() { "Back", "Create...", "View/Edit/Delete..." };

        public GeneratorsMenuState(StateManager<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFont font)
            :base(manager, screen, font, "Generators Menu", _items)
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
                    DoCreate();
                    break;
                case 2:
                    Manager.Push(EditorState.GeneratorSelect);
                    break;
            }
        }

        private void DoCreate()
        {
            var name = Interaction.InputBox("Generator Name?", "Create Generator...");
            if(!string.IsNullOrEmpty(name))
            {
                Data.World.Generators.LookUp[name] = new Dictionary<int, int>();

                GeneratorMenuState.Current = name;
                Manager.Push(EditorState.GeneratorMenu);
            }
        }
    }
}
