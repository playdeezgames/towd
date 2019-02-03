using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Item: VisibleElement
    {
        public string DisplayName { get; set; }
        public int BuyPrice { get; set; }//the price for tagon to buy the item from a shoppe that is selling it
        public int SellPrice { get; set; }//the price for tagon to sell the item to a shopped that is buying it
    }
}
