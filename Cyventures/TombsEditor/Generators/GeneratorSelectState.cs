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
    public class GeneratorSelectState : State<EditorState, Command>
    {
        public static int Current;
        private ColorBuffer<CyColor> _screen;
        private CyFont _font;
        public GeneratorSelectState(StateManager<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFont font)
            : base(manager)
        {
            _screen = screen;
            _font = font;
        }

        public override void DoCommand(Command command)
        {
            switch (command)
            {
                case Command.Down:
                    Current = (Current + 1) % Data.World.Generators.Count();
                    break;
                case Command.Up:
                    Current = (Current + Data.World.Generators.Count() - 1) % Data.World.Generators.Count();
                    break;
                case Command.Blue:
                case Command.Green:
                    GeneratorMenuState.Current = Data.World.Generators.Keys.ToList()[Current];
                    Manager.Push(EditorState.GeneratorMenu);
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
            int rowCount = Data.World.Generators.Count();
            List<string> items = Data.World.Generators.Keys.ToList();

            for (int row = 0; row < rowCount; ++row)
            {
                int plotX = 0;
                int plotY = _screen.Height / 2 + (row - Current) * (_font.Height) - (_font.Height) / 2;
                _font.WriteText(_screen, CyColor.Black, plotX, plotY, items[row]);
            }
        }
    }
}
