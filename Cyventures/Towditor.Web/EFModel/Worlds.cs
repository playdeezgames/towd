using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class Worlds
    {
        public Worlds()
        {
            WorldCreatures = new HashSet<WorldCreatures>();
            WorldTerrains = new HashSet<WorldTerrains>();
        }

        public int WorldId { get; set; }
        public string WorldName { get; set; }

        public virtual ICollection<WorldCreatures> WorldCreatures { get; set; }
        public virtual ICollection<WorldTerrains> WorldTerrains { get; set; }
    }
}
