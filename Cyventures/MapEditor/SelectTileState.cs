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
    public class SelectTileState : StateOld<EditorState, Command>
    {
        private ColorBuffer<CyColor> _screen;
        private CyFontOld _font;
        private int _column=0;
        public SelectTileState(StateManagerOld<EditorState, Command> manager, ColorBuffer<CyColor> screen, CyFontOld font)
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
                    _column = (_column + 1) % Data.TileSet.Items.Count();
                    break;
                case Command.Left:
                    _column = (_column + Data.TileSet.Items.Count() - 1) % Data.TileSet.Items.Count();
                    break;
                case Command.Green:
                case Command.Blue:
                    EditState.Current = _column;
                    Manager.Set(EditorState.Edit);
                    break;
                case Command.Red:
                    Manager.Set(EditorState.Edit);
                    break;
            }
        }

        public override void Update(TimeSpan elapsed)
        {
            _screen.Clear(CyColor.LightGray);
            int CellWidth = Data.TileSet.Items[0].Width;
            int CellHeight = Data.TileSet.Items[0].Height;

            for (int column=0;column< Data.TileSet.Items.Count(); ++column)
            {
                int plotX = _screen.Width / 2 + (column-_column) * (CellWidth + 2) - (CellWidth + 2)/2;
                int plotY = _screen.Height / 2 - (CellHeight + 2)/2;
                if(column==_column)
                {
                    _screen.Box(plotX, plotY, CellWidth + 2, CellHeight + 2, CyColor.DarkGray);
                    _screen.Box(plotX+1, plotY+1, CellWidth, CellHeight, CyColor.White);
                }
                Data.TileSet.Items[column].Draw(_screen, plotX+1, plotY+1,x=>true);
            }
            _screen.Box(0,0, _screen.Width, _font.Height, CyColor.White);
            _font.WriteText(_screen, CyColor.Black, 0, 0, $"Index = {_column}");

        }
    }
}
