using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class Worlds
    {
        public Worlds()
        {
            Rooms = new HashSet<Rooms>();
        }

        public int WorldId { get; set; }
        public string WorldName { get; set; }

        public virtual ICollection<Rooms> Rooms { get; set; }
    }
}
