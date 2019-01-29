using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class CreatureInstances
    {
        public int CreatureInstanceId { get; set; }
        public int CreatureId { get; set; }
        public int TileId { get; set; }

        public virtual Creatures Creature { get; set; }
        public virtual Tiles Tile { get; set; }
    }
}
