using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ColorBuffer<T>: PixelWriterBase<T>
    {
        private Color[] _buffer;
        private Func<T, Color> _convertor;
        public ColorBuffer(int width, int height, Func<T,Color> convertor)
            :base(width,height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }
            if(height<=0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }
            _convertor = convertor ?? throw new ArgumentNullException(nameof(convertor));
            _buffer = new Color[Width * Height];
        }
        public override void Put(int x, int y, T color)
        {
            if(x>=0 && x< Width && y>=0 && y<Height)
            {
                _buffer[y * Width + x] = _convertor(color);
            }
        }
        public void Apply(Texture2D texture)
        {
            texture.SetData(_buffer);
        }
    }
}
