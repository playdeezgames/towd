using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CyFont
    {
        public int Height { get; set; }
        public Dictionary<char, CyGlyph> Glyphs { get; set; }
        public CyRect GetBounds(string text)
        {
            int width = 0;
            foreach (var ch in text)
            {
                Glyphs.TryGetValue(ch, out CyGlyph glyph);
                if (glyph != null)
                {
                    width += glyph.Width;
                }
            }
            return CyRect.Create(0, 0, width, Height);
        }
        public void Draw<T>(IPixelWriter<T> pixelWriter, T color, int x, int y, string text, CyRect? clipRect=null)
        {
            foreach(var ch in text)
            {
                Glyphs.TryGetValue(ch, out CyGlyph glyph);
                if(glyph!=null)
                {
                    x = glyph.Draw(pixelWriter, color, x, y, clipRect);
                }
            }
        }
    }
}
