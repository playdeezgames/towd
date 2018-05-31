using Common;
using EditorCommon;
using MonoGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontEditor
{
    public class CreateState : State<EditorState, Command>
    {
        private ColorBuffer<CyColor> _screen;
        private CyFont _font;
        private int _width=1;
        private int _height=1;
        public CreateState(StateManager<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFont font)
            :base(manager)
        {
            _screen = screen;
            _font = font;
        }

        public override void DoCommand(Command command)
        {
            switch(command)
            {
                case Command.Right:
                    _width = (_width <32) ? (_width + 1) : (_width);
                    break;
                case Command.Left:
                    _width = (_width > 1) ? (_width - 1) : (_width);
                    break;
                case Command.Up:
                    _height = (_height <32) ? (_height + 1) : (_height);
                    break;
                case Command.Down:
                    _height = (_height > 1) ? (_height - 1) : (_height);
                    break;
                case Command.Green:
                case Command.Blue:
                    Data.Font = CyFont.Create(_width, _height);
                    Manager.Set(EditorState.Browse);
                    break;
                case Command.Red:
                    Manager.Set(EditorState.MainMenu);
                    break;
            }
        }

        public override void Update(TimeSpan elapsed)
        {
            _screen.Clear(CyColor.White);

            _font.WriteText(_screen, CyColor.Black, 0, 0, $"Width: {_width}");
            _font.WriteText(_screen, CyColor.Black, 0, _font.Height, $"Height: {_height}");
        }
    }
}
