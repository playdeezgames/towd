﻿using System;
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
            }
        }

        public override void Update(TimeSpan elapsed)
        {
            base.Update(elapsed);

            _screen.Box(0, _screen.Height - _font.Height, _screen.Width, _font.Height, CyColor.Black);
            _font.WriteText(_screen, CyColor.White, 0, _screen.Height - _font.Height, Current);
        }
    }
}
