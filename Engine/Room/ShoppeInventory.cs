using System;
using System.Collections.Generic;

namespace Engine
{
    public class ShoppeInventory
    {
        public List<string> Selling { get; set; }
        public List<string> Buying { get; set; }
        internal void AddSelling(string itemName)
        {
            Selling.Add(itemName);
        }

        internal void AddBuying(string itemName)
        {
            Buying.Add(itemName);
        }
    }
}