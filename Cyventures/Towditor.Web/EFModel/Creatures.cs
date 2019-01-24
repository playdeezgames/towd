using System;
using System.Collections.Generic;

namespace Towditor.Web.EFModel
{
    public partial class Creatures
    {
        public Creatures()
        {
            WorldCreatures = new HashSet<WorldCreatures>();
        }

        public int CreatureId { get; set; }
        public int BitmapId { get; set; }
        public string CreatureName { get; set; }

        public virtual Bitmaps Bitmap { get; set; }
        public virtual ICollection<WorldCreatures> WorldCreatures { get; set; }
    }
}
