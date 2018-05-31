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
    public class GeneratorMenuState : CyMenuState<EditorState>
    {
        public static string Current { get; set; }
        private static readonly List<string> _items = new List<string>() { "Go Back", "Delete Generator", "Add Entry...", "View/Edit/Delete Entry..." };

        public GeneratorMenuState(StateManager<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFont font)
            :base(manager, screen, font, "Generator Menu", _items)
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
                    Data.World.Generators.Remove(Current);
                    Manager.Pop();
                    if(Manager.Current== EditorState.GeneratorSelect && !Data.World.Generators.Any())
                    {
                        Manager.Pop();
                    }
                    break;
                case 2:
                    DoAddEntry();
                    break;
                case 3:
                    if (Data.World.Generators[Current].Any())
                    {
                        Manager.Push(EditorState.GeneratorEntrySelect);
                    }
                    break;
            }
        }

        private void DoAddEntry()
        {
            string text = Interaction.InputBox("Generated Value?", "Add Entry...");
            if(int.TryParse(text, out int value))
            {
                text = Interaction.InputBox("Generation Weight?", "Add Entry...");
                if(int.TryParse(text, out int weight))
                {
                    if(weight>0)
                    {
                        Data.World.Generators[Current][value] = weight;
                    }
                    else
                    {
                        Data.World.Generators[Current].Remove(value);
                    }
                }
            }
        }

        public override void Update(TimeSpan elapsed)
        {
            base.Update(elapsed);

            _screen.Box(0, _screen.Height - _font.Height, _screen.Width, _font.Height, CyColor.DarkGray);
            _font.WriteText(_screen, CyColor.White, 0, _screen.Height - _font.Height, Current);
        }
    }
}
