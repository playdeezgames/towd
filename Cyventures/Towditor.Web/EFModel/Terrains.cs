using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class Terrains
    {
        public Terrains()
        {
            Tiles = new HashSet<Tiles>();
        }

        public int TerrainId { get; set; }
        public int BitmapId { get; set; }
        public string TerrainName { get; set; }
        public int TileRoleId { get; set; }

        public virtual Bitmaps Bitmap { get; set; }
        public virtual TileRoles TileRole { get; set; }
        public virtual ICollection<Tiles> Tiles { get; set; }
    }
}
