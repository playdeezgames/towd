using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeBitmap
{
    class Program
    {
        static void Main(string[] args)
        {
            string inFile = args[1];
            string outFile = args[0];

            Bitmap bmp = (Bitmap)Image.FromFile(inFile);

            var result = new Bitmap<CyColor>(bmp.Width, bmp.Height);
            for(int x= 0;x<bmp.Width;++x)
            {
                for(int y = 0; y<bmp.Height;++y)
                {
                    var color = bmp.GetPixel(x, y);
                    var cyColor = CyColor.White;
                    switch(color.R/85)
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
                    result.Set(x, y, cyColor);
                }
            }

            Utility.Save(result, outFile);
        }
    }
}
