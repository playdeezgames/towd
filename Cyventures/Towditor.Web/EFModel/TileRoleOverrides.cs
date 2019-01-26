using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class TileRoleOverrides
    {
        public int TileRoleOverrideId { get; set; }
        public int TileId { get; set; }
        public int TileRoleId { get; set; }

        public virtual Tiles Tile { get; set; }
        public virtual TileRoles TileRole { get; set; }
    }
}
