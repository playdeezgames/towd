using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Bitmap<T>
    {
        public int Width { get; set; }
        public T[] Pixels { get; set; }
        public int Height
        {
            get
            {
                return Pixels.Length / Width;
            }
        }
        public Bitmap(int width, int height)
        {
            Width = width;
            Pixels = new T[Width * height];
        }
        public void Set(int x, int y, T color)
        {
            if(x>=0 && y>=0 && x < Width && y<Height)
            {
                Pixels[y * Width + x] = color;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public T Get(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                return Pixels[y * Width + x];
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public void Draw(IPixelWriter<T> pixelWriter, int x, int y, Func<T,bool> filter, CyRect? clipRect=null)
        {
            for(int row=0;row<Height;++row)
            {
                for(int column=0;column<Width;++column)
                {
                    T color = Get(column, row);
                    if(filter(color))
                    {
                        pixelWriter.Put(x + column, y + row, color, clipRect);
                    }
                }
            }
        }
        public void Draw(IPixelWriter<T> pixelWriter, CyPoint pt, Func<T, bool> filter, CyRect? clipRect = null)
        {
            Draw(pixelWriter, pt.X, pt.Y, filter, clipRect);
        }
    }
}
