using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Terrain
    {
        public string ResourceIdentifier { get; set; }//references an exernal bitmapsequence manager
        public int ResourceIndex { get; set; }//references an exernal bitmapsequence manager
        public TileRole Role { get; set; }
        public string EventName { get; set; }
    }
}
