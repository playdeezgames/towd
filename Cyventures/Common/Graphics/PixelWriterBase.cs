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
        public CyRect? ClipRect { get; set; }
        public CyPoint Offset { get; set; }
        private PixelWriterBase() { }
        public PixelWriterBase(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public abstract void DoPut(int x, int y, T color);
        public void Put(int x, int y, T color)
        {
            x += Offset.X;
            y += Offset.Y;
            if(!ClipRect.HasValue || ClipRect.Value.Contains(x,y))
            {
                DoPut(x, y, color);
            }
        }
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

        public void Put(CyPoint pt, T color)
        {
            Put(pt.X, pt.Y, color);
        }

        public void HLine(CyPoint pt, int w, T color)
        {
            HLine(pt.X, pt.Y, w, color);
        }

        public void VLine(CyPoint pt, int h, T color)
        {
            VLine(pt.X, pt.Y, h, color);
        }

        public void Box(CyRect rect, T color)
        {
            Box(rect.X, rect.Y, rect.Width, rect.Height, color);
        }
    }
}
