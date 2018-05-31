﻿using Common;
using EditorCommon;
using MonoGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TombsEditor
{
    public class ConfirmQuitState : CyMenuState<EditorState>
    {
        private static readonly List<string> _items = new List<string>() { "No", "Yes" };

        public ConfirmQuitState(StateManager<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFont font)
            : base(manager, screen, font, "Are you sure?", _items)
        {
        }
        public override void OnItemSelected(int itemIndex)
        {
            switch(itemIndex)
            {
                case 0:
                    Manager.Set(EditorState.MainMenu);
                    break;
                case 1:
                    Manager.Set(EditorState.Quit);
                    break;
            }
        }
    }
}
