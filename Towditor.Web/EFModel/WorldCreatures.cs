using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class WorldCreatures
    {
        public int WorldCreatureId { get; set; }
        public int WorldId { get; set; }
        public int CreatureId { get; set; }

        public virtual Creatures Creature { get; set; }
        public virtual Worlds World { get; set; }
    }
}
