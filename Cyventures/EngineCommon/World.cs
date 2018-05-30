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
        public Table<string, Dictionary<int, int>> Generators { get; set; } = new Table<string, Dictionary<int, int>>();
        public Table<string, TileMap<Tile>> Rooms { get; set; } = new Table<string, TileMap<Tile>>();

        public World()
        {
        }
    }
}
