using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class Colors
    {
        public Colors()
        {
            BitmapPixels = new HashSet<BitmapPixels>();
        }

        public int ColorId { get; set; }

        public virtual ICollection<BitmapPixels> BitmapPixels { get; set; }
    }
}
