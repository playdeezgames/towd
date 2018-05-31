using Common;
using EditorCommon;
using MonoGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    public class CreateState : StateOld<EditorState, Command>
    {
        private ColorBuffer<CyColor> _screen;
        private CyFontOld _font;
        private int _width=1;
        private int _height=1;
        public CreateState(StateManagerOld<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFontOld font)
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
                    Data.Map = new TileMap<int>(_width,_height,0);
                    Manager.Set(EditorState.Edit);
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
