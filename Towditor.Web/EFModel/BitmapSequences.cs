using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class BitmapSequences
    {
        public BitmapSequences()
        {
            Bitmaps = new HashSet<Bitmaps>();
        }

        public int BitmapSequenceId { get; set; }
        public string BitmapSequenceName { get; set; }

        public virtual ICollection<Bitmaps> Bitmaps { get; set; }
    }
}
