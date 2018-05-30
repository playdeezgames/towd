using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontEditor
{
    public class BrowseState : State<EditorState, Command>
    {
        const int Columns = 16;
        const int Rows = 6;
        const int StartingCharacter=32;
        private ColorBuffer<CyColor> _screen;
        private CyFont _font;
        private int _column=0;
        private int _row=0;
        public BrowseState(StateManager<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFont font)
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
                    _column = (_column + 1) % Columns;
                    break;
                case Command.Left:
                    _column = (_column + Columns - 1) % Columns;
                    break;
                case Command.Down:
                    _row = (_row + 1) % Rows;
                    break;
                case Command.Up:
                    _row = (_row + Rows - 1) % Rows;
                    break;
                case Command.Select:
                case Command.Enter:
                    EditState.Current = _row * Columns + _column + StartingCharacter;
                    Manager.Current = EditorState.Edit;
                    break;
                case Command.Esc:
                    Manager.Current = EditorState.MainMenu;
                    break;
            }
        }

        public override void Update(TimeSpan elapsed)
        {
            _screen.Clear(CyColor.LightGray);

            for(int row=0;row<Rows;++row)
            {
                for(int column=0;column< Columns; ++column)
                {
                    int plotX = _screen.Width / 2 + (column-_column) * (Data.Font.Width + 2) - (Data.Font.Width + 2)/2;
                    int plotY = _screen.Height / 2 + (row-_row) * (Data.Font.Height + 2) - (Data.Font.Height + 2)/2;
                    if(column==_column && row==_row)
                    {
                        _screen.Box(plotX, plotY, Data.Font.Width + 2, Data.Font.Height + 2, CyColor.DarkGray);
                        _screen.Box(plotX+1, plotY+1, Data.Font.Width, Data.Font.Height, CyColor.White);
                    }
                    else
                    {
                        _screen.Box(plotX, plotY, Data.Font.Width + 2, Data.Font.Height + 2, CyColor.White);
                    }
                    Data.Font.WriteCharacter(_screen, CyColor.Black, plotX+1, plotY+1, row * Columns + column + StartingCharacter);
                }
            }
            _screen.Box(0,0, _screen.Width, _font.Height, CyColor.White);
            _font.WriteText(_screen, CyColor.Black, 0, 0, $"Char = {_row * Columns + _column + StartingCharacter}");

        }
    }
}
