using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class PixelWriterBase<T> : IPixelWriter<T>
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private PixelWriterBase() { }
        public PixelWriterBase(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public abstract void Put(int x, int y, T color);
        public void HLine(int x, int y, int w, T color)
        {
            while (w > 0)
            {
                Put(x, y, color);
                x++;
                w--;
            }
        }
        public void VLine(int x, int y, int h, T color)
        {
            while (h > 0)
            {
                Put(x, y, color);
                y++;
                h--;
            }
        }
        public void Box(int x, int y, int w, int h, T color)
        {
            while (h > 0)
            {
                HLine(x, y, w, color);
                ++y;
                h--;
            }
        }
        public void Clear(T color)
        {
            Box(0, 0, Width, Height, color);
        }
    }
}
