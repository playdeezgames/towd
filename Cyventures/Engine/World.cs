using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class World
    {
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public Dictionary<string, Terrain> Terrains { get; set; }
        public Dictionary<string, Bitmap<Tile>> Rooms { get; set; }
    }
}
