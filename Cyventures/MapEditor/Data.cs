using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    public static class Data
    {
        public static TileMap<int> Map { get; set; }
        public static CyBitmapSequence TileSet { get; set; }
    }
}
