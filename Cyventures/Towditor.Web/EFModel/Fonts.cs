using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class Fonts
    {
        public Fonts()
        {
            Glyphs = new HashSet<Glyphs>();
        }

        public int FontId { get; set; }
        public string FontName { get; set; }
        public int FontHeight { get; set; }

        public virtual ICollection<Glyphs> Glyphs { get; set; }
    }
}
