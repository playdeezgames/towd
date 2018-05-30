using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineCommon
{
    public class World
    {
        public Dictionary<string, Dictionary<int, int>> Generators { get; set; } = new Dictionary<string, Dictionary<int, int>>();
        public Dictionary<string, TileMap<Tile>> Rooms { get; set; } = new Dictionary<string, TileMap<Tile>>();

        public World()
        {
        }
    }
}
