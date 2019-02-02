using System;
using System.Collections.Generic;

namespace Engine
{
    public class CreatureInstance
    {
        //WHAT
        public string Creature { get; set; }
        //WHERE
        public string Room { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        //WHO
        public string Name { get; set; }
        //INVENTORY
        public int Money { get; set; }
        public Dictionary<string, int> Items { get; set; }

        internal void GiveItem(string itemIdentifier, int quantity)
        {
            if (Items.TryGetValue(itemIdentifier, out int previous))
            {
                Items[itemIdentifier] = previous + quantity;
            }
            else
            {
                Items[itemIdentifier] = quantity;
            }
        }
    }
}