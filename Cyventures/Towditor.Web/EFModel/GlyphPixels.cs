using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class GlyphPixels
    {
        public int GlyphPixelId { get; set; }
        public int GlyphId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public virtual Glyphs Glyph { get; set; }
    }
}
