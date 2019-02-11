using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Item: VisibleElement
    {
        public ItemType ItemType { get; set; }
        public string DisplayName { get; set; }
        public int BuyPrice { get; set; }//the price for tagon to buy the item from a shoppe that is selling it
        public int SellPrice { get; set; }//the price for tagon to sell the item to a shopped that is buying it
        public HashSet<char> EquipSlots { get; set; }
        //EquipSlots
        //w=weapon hand
        //s=shield hand
        //h=head(helmet)
        //t=torso("chainmail")
        //f=feet(boots)
        //b=back("cape")
        //n=neck(necklace)
        public string Attack { get; set; }
        public string Defense { get; set; }
        public int Body { get; set; }

        internal HashSet<char> GetEquipSlots()
        {
            if(EquipSlots==null)
            {
                EquipSlots = new HashSet<char>();
            }
            return EquipSlots;
        }
    }
}
