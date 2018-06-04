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
        public bool EditMode { get; set; }
        public int Avatar { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public Dictionary<string, Terrain> Terrains { get; set; }
        public Dictionary<string, Creature> Creatures { get; set; }
        public Dictionary<string, Room> Rooms { get; set; }
        public Dictionary<int, CreatureInstance> CreatureInstances { get; set; }
        public Dictionary<string, Event> Events { get; set; }

        public Room GetAvatarRoom()
        {
            return Rooms[GetAvatarCreatureInstance().Room];
        }

        public CreatureInstance GetAvatarCreatureInstance()
        {
            return CreatureInstances[Avatar];
        }

        public bool IsTerrainInUse(string terrain)
        {
            return Rooms.Any(x => x.Value.IsTerrainInUse(terrain));
        }
    }
}
