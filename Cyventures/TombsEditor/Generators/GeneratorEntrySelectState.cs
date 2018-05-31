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
    public class GeneratorEntrySelectState : StateOld<EditorState, Command>
    {
        public static int Current;
        private ColorBuffer<CyColor> _screen;
        private CyFontOld _font;
        public GeneratorEntrySelectState(StateManagerOld<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFontOld font)
            : base(manager)
        {
            _screen = screen;
            _font = font;
        }

        public override void DoCommand(Command command)
        {
            int rowCount = Data.World.Generators[GeneratorMenuState.Current].Count();
            switch (command)
            {
                case Command.Down:
                    Current = (Current + 1) % rowCount;
                    break;
                case Command.Up:
                    Current = (Current + rowCount - 1) % rowCount;
                    break;
                case Command.Blue:
                case Command.Green:
                    GeneratorEntryMenuState.Current = Data.World.Generators[GeneratorMenuState.Current].Keys.ToList()[Current];
                    Manager.Push(EditorState.GeneratorEntryMenu);
                    break;
                case Command.Red:
                    Manager.Pop();
                    break;
            }
        }

        public override void Update(TimeSpan elapsed)
        {
            _screen.Clear(CyColor.LightGray);
            _screen.Box(0, _screen.Height / 2 - _font.Height / 2, _screen.Width, _font.Height, CyColor.White);
            List<string> items = Data.World.Generators[GeneratorMenuState.Current].Select(x=>$"[{x.Key}] = {x.Value}").ToList();
            int rowCount = Data.World.Generators[GeneratorMenuState.Current].Count();

            for (int row = 0; row < rowCount; ++row)
            {
                int plotX = 0;
                int plotY = _screen.Height / 2 + (row - Current) * (_font.Height) - (_font.Height) / 2;
                _font.WriteText(_screen, CyColor.Black, plotX, plotY, items[row]);
            }
        }
    }
}
