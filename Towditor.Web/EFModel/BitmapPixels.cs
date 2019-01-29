using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class BitmapPixels
    {
        public int BitmapPixelId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int ColorId { get; set; }
        public int BitmapId { get; set; }

        public virtual Bitmaps Bitmap { get; set; }
        public virtual Colors Color { get; set; }
    }
}
