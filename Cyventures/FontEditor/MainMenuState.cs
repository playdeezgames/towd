﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using EditorCommon;
using Microsoft.Xna.Framework;
using MonoGameCommon;

namespace FontEditor
{
    public class MainMenuState : CyMenuState<EditorState>
    {
        private static readonly List<string> _items = new List<string>() { "Edit","New...","Open...","Save...","Quit" };

        public MainMenuState(StateManagerOld<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFontOld font)
            :base(manager, screen, font, "Font Editor", _items)
        {
        }

        public override void OnItemSelected(int itemIndex)
        {
            switch(itemIndex)
            {
                case 0:
                    Manager.Set(EditorState.Browse);
                    break;
                case 1:
                    Manager.Set(EditorState.Create);
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
                Utility.Save(Data.Font,dialog.FileName);
            }
        }

        private void DoLoad()
        {
            OpenFileDialog dialog = new OpenFileDialog() { };
            var result = dialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                Data.Font = Utility.Load<CyFontOld>(dialog.FileName);
            }
        }
    }
}
