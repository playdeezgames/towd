using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class Terrains
    {
        public Terrains()
        {
            WorldTerrains = new HashSet<WorldTerrains>();
        }

        public int TerrainId { get; set; }
        public int BitmapId { get; set; }
        public string TerrainName { get; set; }

        public virtual Bitmaps Bitmap { get; set; }
        public virtual ICollection<WorldTerrains> WorldTerrains { get; set; }
    }
}
