using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class WorldTerrains
    {
        public int WorldTerrainId { get; set; }
        public int WorldId { get; set; }
        public int TerrainId { get; set; }

        public virtual Terrains Terrain { get; set; }
        public virtual Worlds World { get; set; }
    }
}
