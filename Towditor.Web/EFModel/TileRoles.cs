using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class TileRoles
    {
        public TileRoles()
        {
            Terrains = new HashSet<Terrains>();
            TileRoleOverrides = new HashSet<TileRoleOverrides>();
        }

        public int TileRoleId { get; set; }
        public string TileRoleName { get; set; }

        public virtual ICollection<Terrains> Terrains { get; set; }
        public virtual ICollection<TileRoleOverrides> TileRoleOverrides { get; set; }
    }
}
