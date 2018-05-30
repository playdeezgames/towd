using Common;
using EditorCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
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
                case Command.Select:
                case Command.Enter:
                    Data.Map = new TileMap<int>(_width,_height,0);
                    Manager.Set(EditorState.Edit);
                    break;
                case Command.Esc:
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
