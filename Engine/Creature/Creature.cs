using System.Collections.Generic;

namespace Engine
{
    public class Creature: VisibleElement
    {
        public int Body { get; set; }
        public int Mind { get; set; }
        public int BaseXP { get; set; }
        public string UnarmedAttack { get; set; }
        public string BaseDefense { get; set; }
        public HashSet<char> EquipSlots { get; set; }
    }
}