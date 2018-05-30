using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineCommon
{
    public class Tile
    {
        public int Terrain { get; set; }
        public TileRole Role { get; set; }
        public Teleport Teleport { get; set; }
        public int? CreatureInstance { get; set; }
    }
}
