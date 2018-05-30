using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CyBitmapSequence
    {
        public List<CyBitmap> Bitmaps { get; set; }
        public CyBitmapSequence()
        {
            Bitmaps = new List<CyBitmap>();
        }
        public void Append(CyBitmap bitmap)
        {
            Bitmaps.Add(bitmap);
        }
        public void Append(CyBitmapSequence bitmapSequence)
        {
            Bitmaps.AddRange(bitmapSequence.Bitmaps);
        }
    }
}
