using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class Glyphs
    {
        public Glyphs()
        {
            GlyphPixels = new HashSet<GlyphPixels>();
        }

        public int GlyphId { get; set; }
        public int GlyphCharacter { get; set; }
        public int FontId { get; set; }
        public int GlyphWidth { get; set; }

        public virtual Fonts Font { get; set; }
        public virtual ICollection<GlyphPixels> GlyphPixels { get; set; }
    }
}
