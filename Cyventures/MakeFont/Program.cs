using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeFont
{
    class Program
    {
        const int CellColumns = 16;
        const int CellRows = 6;
        const int CharacterStart = 32;
        static void Main(string[] args)
        {
            string inFile = args[1];
            string outFile = args[0];

            Bitmap bmp = (Bitmap)Image.FromFile(inFile);

            var cellWidth = bmp.Width / CellColumns;
            var cellHeight = bmp.Height / CellRows;

            Dictionary<char, CyGlyph> font = new Dictionary<char, CyGlyph>();
            for(int column=0;column<CellColumns;++column)
            {
                for(int row = 0;row<CellRows;++row)
                {
                    CyGlyph cell = new CyGlyph(cellWidth);
                    font[(char)(row * CellColumns + column+CharacterStart)] = cell;
                    for(int y =0;y<cellHeight;++y)
                    {
                        for(int x=0;x<cellWidth;++x)
                        {
                            var color = bmp.GetPixel(column * cellWidth + x, row * cellHeight + y);
                            if(color.R==0)
                            {
                                cell.Set(x, y);
                            }
                        }
                    }
                }
            }
            CyFont wrapper = new CyFont()
            {
                Height = cellHeight,
                Glyphs = font
            };
            Utility.Save(wrapper,outFile);
        }
    }
}
