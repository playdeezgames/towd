using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class RoomTile
    {
        public string Terrain { get; set; }
        public RoomTileRole? RoleOverride { get; set; }
        public Teleport Teleport { get; set; }
        public Sign Sign { get; set; }
        public Search Search { get; set; }
        public Shoppe Shoppe { get; set; }
        public string CreatureInstance { get; set; }
    }
}
