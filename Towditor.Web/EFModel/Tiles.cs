using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class Tiles
    {
        public int TileId { get; set; }
        public int RoomId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int TerrainId { get; set; }

        [JsonIgnore]
        public virtual Rooms Room { get; set; }
        public virtual Terrains Terrain { get; set; }
        public virtual CreatureInstances CreatureInstances { get; set; }
        public virtual TileRoleOverrides TileRoleOverrides { get; set; }
    }
}
