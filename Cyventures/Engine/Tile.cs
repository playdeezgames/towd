using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Tile
    {
        public string Terrain { get; set; }
        public TileRole Role { get; set; }
        public Teleport Teleport { get; set; }
    }
}
