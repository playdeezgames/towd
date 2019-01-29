using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public struct CyRect
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Right => X + Width;
        public int Bottom => Y + Height;
        public bool Contains(int x, int y)
        {
            return (x >= X && y >= Y && x < (X + Width) && y < (Y + Height));
        }
        public static CyRect Create(int x, int y, int width, int height)
        {
            return new CyRect() { X = x, Y = y, Width = width, Height = height };
        }
        public CyPoint Offset
        {
            get
            {
                return CyPoint.Create(X, Y);
            }
        }
        public CyRect OffsetBy(int x, int y)
        {
            return Create(X + x, Y + y, Width, Height);
        }
        public CyRect OffsetBy(CyPoint offset)
        {
            return OffsetBy(offset.X, offset.Y);
        }

        internal CyRect Intersect(CyRect other)
        {
            int x = Math.Max(X, other.X);
            int y = Math.Max(Y, other.Y);
            int right = Math.Min(Right, other.Right);
            int bottom = Math.Min(Bottom, other.Bottom);
            return Create(x, y, right - x, bottom - y);
        }
    }
}
