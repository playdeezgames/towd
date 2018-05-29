using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontEditor
{
    public class ColorBuffer<T>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private Color[] _buffer;
        private Func<T, Color> _convertor;
        public ColorBuffer(int width, int height, Func<T,Color> convertor)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }
            if(height<=0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            Width = width;
            Height = height;
            _convertor = convertor ?? throw new ArgumentNullException(nameof(convertor));
            _buffer = new Color[Width * Height];
        }
        public void Put(int x, int y, T color)
        {
            if(x>=0 && x< Width && y>=0 && y<Height)
            {
                _buffer[y * Width + x] = _convertor(color);
            }
        }
        public void HLine(int x, int y, int w, T color)
        {
            while(w>0)
            {
                Put(x, y, color);
                x++;
                w--;
            }
        }
        public void VLine(int x, int y, int h, T color)
        {
            while(h>0)
            {
                Put(x, y, color);
                y++;
                h--;
            }
        }
        public void Box(int x, int y, int w, int h, T color)
        {
            while(h>0)
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
        public void Apply(Texture2D texture)
        {
            texture.SetData(_buffer);
        }
    }
}
