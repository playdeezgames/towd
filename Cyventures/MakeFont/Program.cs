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
            string inFile = args[0];
            string outFile = args[1];

            Bitmap bmp = (Bitmap)Image.FromFile(inFile);

            var cellWidth = bmp.Width / CellColumns;
            var cellHeight = bmp.Height / CellRows;

            Dictionary<int, List<List<int>>> font = new Dictionary<int, List<List<int>>>();
            for(int column=0;column<CellColumns;++column)
            {
                for(int row = 0;row<CellRows;++row)
                {
                    List<List<int>> cell = new List<List<int>>();
                    font[row * CellColumns + column+CharacterStart] = cell;
                    for(int y =0;y<cellHeight;++y)
                    {
                        List<int> line = new List<int>();
                        cell.Add(line);
                        for(int x=0;x<cellWidth;++x)
                        {
                            var color = bmp.GetPixel(column * cellWidth + x, row * cellHeight + y);
                            if(color.R==0)
                            {
                                line.Add(x);
                            }
                        }
                    }
                }
            }
            CyFont wrapper = new CyFont()
            {
                Width = cellWidth,
                Height = cellHeight,
                Data = font
            };
            Utility.Save(wrapper,outFile);
        }
    }
}
