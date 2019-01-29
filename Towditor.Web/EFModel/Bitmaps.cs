using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class Bitmaps
    {
        public Bitmaps()
        {
            BitmapPixels = new HashSet<BitmapPixels>();
            Creatures = new HashSet<Creatures>();
            Terrains = new HashSet<Terrains>();
        }

        public int BitmapId { get; set; }
        public int BitmapWidth { get; set; }
        public int BitmapHeight { get; set; }
        public int BitmapSequenceId { get; set; }
        public int BitmapIndex { get; set; }

        public virtual BitmapSequences BitmapSequence { get; set; }
        public virtual ICollection<BitmapPixels> BitmapPixels { get; set; }
        public virtual ICollection<Creatures> Creatures { get; set; }
        public virtual ICollection<Terrains> Terrains { get; set; }
    }
}
