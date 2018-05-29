using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontEditor
{
    public class EditState : State<EditorState, Command>
    {
        const int CellWidth = 8;
        const int CellHeight = 8;
        public static int Current;
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
                    _column = (_column + 1) % Data.Font.Width;
                    break;
                case Command.Left:
                    _column = (_column + Data.Font.Width - 1) % Data.Font.Width;
                    break;
                case Command.Down:
                    _row = (_row + 1) % Data.Font.Height;
                    break;
                case Command.Up:
                    _row = (_row + Data.Font.Height - 1) % Data.Font.Height;
                    break;
                case Command.Select:
                case Command.Enter:
                    if(Data.Font.Data[Current][_row].Contains(_column))
                    {
                        Data.Font.Data[Current][_row].Remove(_column);
                    }
                    else
                    {
                        Data.Font.Data[Current][_row].Add(_column);
                    }
                    break;
                case Command.Esc:
                    Manager.Current = EditorState.Browse;
                    break;
            }
        }

        public override void Update(TimeSpan elapsed)
        {
            _screen.Clear(CyColor.LightGray);

            for (int row = 0; row < Data.Font.Height; ++row)
            {
                for (int column = 0; column < Data.Font.Width; ++column)
                {
                    int plotX = _screen.Width / 2 + (column - _column) * (CellWidth) - (CellWidth) / 2;
                    int plotY = _screen.Height / 2 + (row - _row) * (CellHeight) - (CellHeight) / 2;
                    CyColor color = Data.Font.Data[Current][row].Contains(column) ? CyColor.Black : CyColor.White;
                    if(_column==column && _row==row)
                    {
                        _screen.Box(plotX, plotY, CellWidth, CellHeight, (color == CyColor.White)?(CyColor.Black):(CyColor.White));
                        _screen.Box(plotX+1, plotY+1, CellWidth-2, CellHeight-2, color);
                    }
                    else
                    {
                        _screen.Box(plotX, plotY, CellWidth, CellHeight, color);
                    }
                }
            }

        }
    }
}
