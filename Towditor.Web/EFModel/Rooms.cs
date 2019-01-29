using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class Rooms
    {
        public Rooms()
        {
            Tiles = new HashSet<Tiles>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int WorldId { get; set; }

        public virtual Worlds World { get; set; }
        public virtual ICollection<Tiles> Tiles { get; set; }
    }
}
