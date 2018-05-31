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
    public class GeneratorEntryMenuState : CyMenuState<EditorState>
    {
        public static int Current { get; set; }
        private static readonly List<string> _items = new List<string>() { "Go Back", "Delete Entry", "Change Weight..." };

        public GeneratorEntryMenuState(StateManagerOld<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFontOld font)
            :base(manager, screen, font, "Entry Menu", _items)
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
                    DoDelete();
                    break;
                case 2:
                    DoChangeWeight();
                    break;
            }
        }

        private void DoDelete()
        {
            Data.World.Generators[GeneratorMenuState.Current].Remove(Current);
            Manager.Pop();
            if (Manager.Current == EditorState.GeneratorEntrySelect && !Data.World.Generators[GeneratorMenuState.Current].Any())
            {
                Manager.Pop();
            }
        }

        private void DoChangeWeight()
        {
            string text = Interaction.InputBox("Generation Weight?", "Change Weight...");
            if(int.TryParse(text, out int weight))
            {
                if(weight>0)
                {
                    Data.World.Generators[GeneratorMenuState.Current][Current] = weight;
                }
                else
                {
                    DoDelete();
                }
            }
        }

        public override void Update(TimeSpan elapsed)
        {
            base.Update(elapsed);

            _screen.Box(0, _screen.Height - _font.Height, _screen.Width, _font.Height, CyColor.DarkGray);
            _font.WriteText(_screen, CyColor.White, 0, _screen.Height - _font.Height, $"[{Current}]={Data.World.Generators[GeneratorMenuState.Current][Current]}");
        }
    }
}
