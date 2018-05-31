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
using MonoGameCommon;
using TombsCommon;

namespace TombsEditor
{
    public class GeneratorsMenuState : CyMenuState<EditorState>
    {
        private static readonly List<string> _items = new List<string>() { "Back", "Create...", "View/Edit/Delete..." };

        public GeneratorsMenuState(StateManagerOld<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFontOld font)
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
                    if (Data.World.Generators.Any())
                    {
                        Manager.Push(EditorState.GeneratorSelect);
                    }
                    break;
            }
        }

        private void DoCreate()
        {
            var name = Interaction.InputBox("Generator Name?", "Create Generator...");
            if(!string.IsNullOrEmpty(name))
            {
                Data.World.Generators[name] = new Dictionary<int, int>();

                GeneratorMenuState.Current = name;
                Manager.Push(EditorState.GeneratorMenu);
            }
        }
    }
}
