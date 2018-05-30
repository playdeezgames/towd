using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeBitmapSequence
{
    class Program
    {
        static void Main(string[] args)
        {
            string inFile = args[3];
            int cellWidth = int.Parse(args[1]);
            int cellHeight = int.Parse(args[2]);
            string outFile = args[0];

            Bitmap bmp = (Bitmap)Image.FromFile(inFile);
            int columns = bmp.Width / cellWidth;
            int rows = bmp.Height / cellHeight;

            var result = new CyBitmapSequence();

            for(int row=0;row<rows;++row)
            {
                for(int column=0;column<columns;++column)
                {
                    CyBitmap cyBitmap = new CyBitmap(cellWidth, cellHeight, CyColor.White);
                    for(int x=0;x<cellWidth;++x)
                    {
                        for(int y=0;y<cellHeight;++y)
                        {
                            var color = bmp.GetPixel(column * cellWidth + x, row * cellHeight + y);
                            var cyColor = CyColor.White;
                            switch (color.R / 85)
                            {
                                case 0:
                                    cyColor = CyColor.Black;
                                    break;
                                case 1:
                                    cyColor = CyColor.DarkGray;
                                    break;
                                case 2:
                                    cyColor = CyColor.LightGray;
                                    break;
                                case 3:
                                    cyColor = CyColor.White;
                                    break;
                            }
                            cyBitmap.Put(x, y, cyColor);
                        }
                    }
                    result.Append(cyBitmap);
                }
            }

            Utility.Save(result, outFile);

        }
    }
}
