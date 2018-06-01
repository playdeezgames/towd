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
        public abstract void DoPut(int x, int y, T color);
        public void Put(CyPoint pt, T color)
        {
            Put(pt.X, pt.Y, color);
        }

        public void Put(int x, int y, T color, CyRect? clipRect = null)
        {
            if (clipRect.HasValue)
            {
                if (x < clipRect.Value.Width && y < clipRect.Value.Height && x >= 0 && y >= 0)
                {
                    DoPut(x + clipRect.Value.X, y + clipRect.Value.Y, color);
                }
            }
            else
            {
                DoPut(x, y, color);
            }
        }

        public void Put(CyPoint pt, T color, CyRect? clipRect = null)
        {
            Put(pt.X, pt.Y, color, clipRect);
        }

        public void HLine(int x, int y, int w, T color, CyRect? clipRect = null)
        {
            while (w > 0)
            {
                Put(x, y, color, clipRect);
                x++;
                w--;
            }
        }

        public void HLine(CyPoint pt, int w, T color, CyRect? clipRect = null)
        {
            HLine(pt.X, pt.Y, w, color, clipRect);
        }

        public void VLine(int x, int y, int h, T color, CyRect? clipRect = null)
        {
            while (h > 0)
            {
                Put(x, y, color, clipRect);
                y++;
                h--;
            }
        }

        public void VLine(CyPoint pt, int h, T color, CyRect? clipRect = null)
        {
            VLine(pt.X, pt.Y, h, color, clipRect);
        }

        public void Box(int x, int y, int w, int h, T color, CyRect? clipRect = null)
        {
            while (h > 0)
            {
                HLine(x, y, w, color, clipRect);
                ++y;
                h--;
            }
        }

        public void Box(CyRect rect, T color, CyRect? clipRect = null)
        {
            Box(rect.X, rect.Y, rect.Width, rect.Height, color, clipRect);
        }

        public void Clear(T color, CyRect? clipRect = null)
        {
            Box(0, 0, clipRect?.Width ?? Width, clipRect?.Height ?? Height, color, clipRect);
        }
    }
}
