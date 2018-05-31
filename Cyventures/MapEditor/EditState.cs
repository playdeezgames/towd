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
    public class EditState : State<EditorState, Command>
    {
        public static int Current;
        private CyColor _cursor = CyColor.White;
        private ColorBuffer<CyColor> _screen;
        private CyFont _font;
        private int _column = 0;
        private int _row = 0;
        public EditState(StateManager<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFont font)
            : base(manager)
        {
            _screen = screen;
            _font = font;
        }

        public override void DoCommand(Command command)
        {
            switch (command)
            {
                case Command.Right:
                    _column = (_column + 1) % Data.Map.Width;
                    break;
                case Command.Left:
                    _column = (_column + Data.Map.Width - 1) % Data.Map.Width;
                    break;
                case Command.Down:
                    _row = (_row + 1) % Data.Map.Height;
                    break;
                case Command.Up:
                    _row = (_row + Data.Map.Height - 1) % Data.Map.Height;
                    break;
                case Command.Yellow:
                    Manager.Set(EditorState.SelectTile);
                    break;
                case Command.Next:
                case Command.Previous:
                    _cursor =
                        (_cursor == CyColor.White) ? (CyColor.LightGray) :
                        (_cursor == CyColor.DarkGray) ? (CyColor.Black) :
                        (_cursor == CyColor.LightGray) ? (CyColor.DarkGray) :
                        (CyColor.White);
                    break;
                case Command.Blue:
                case Command.Green:
                    Data.Map.Data[_row][_column] = Current;
                    break;
                case Command.Red:
                    Manager.Set(EditorState.MainMenu);
                    break;
            }
        }

        public override void Update(TimeSpan elapsed)
        {
            _screen.Clear(CyColor.LightGray);
            int CellWidth = Data.TileSet.Items[0].Width;
            int CellHeight = Data.TileSet.Items[0].Height;

            for (int row = 0; row < Data.Map.Height; ++row)
            {
                for (int column = 0; column < Data.Map.Width; ++column)
                {
                    int plotX = _screen.Width / 2 + (column - _column) * (CellWidth) - (CellWidth) / 2;
                    int plotY = _screen.Height / 2 + (row - _row) * (CellHeight) - (CellHeight) / 2;
                    int tileIndex = Data.Map.Data[row][column];
                    Data.TileSet.Items[tileIndex].Draw(_screen, plotX, plotY, x => true);
                }
            }
            _screen.HLine(_screen.Width / 2 - CellWidth / 2, _screen.Height / 2 - CellHeight / 2 - 1, CellWidth, _cursor);
            _screen.HLine(_screen.Width / 2 - CellWidth / 2, _screen.Height / 2 + CellHeight / 2, CellWidth, _cursor);
            _screen.VLine(_screen.Width / 2 - CellWidth / 2-1, _screen.Height / 2 - CellHeight / 2, CellHeight, _cursor);
            _screen.VLine(_screen.Width / 2 + CellWidth / 2, _screen.Height / 2 - CellHeight / 2, CellHeight, _cursor);
            _screen.Box(0, 0, _screen.Width, _font.Height, CyColor.White);
            _font.WriteText(_screen, CyColor.Black, 0, 0, $"X = {_column}, Y = {_row}");

        }
    }
}
