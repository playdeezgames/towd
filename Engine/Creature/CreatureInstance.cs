using System;
using System.Collections.Generic;
using System.Linq;

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
        public HashSet<string> Equipped { get; set; }
        public string Dialog { get; set; }
        //COMBAT
        public int Body { get; set; }
        public int Wounds { get; set; }
        public int Mind { get; set; }
        public int Fatigue { get; set; }
        public string UnarmedAttack { get; set; }
        public string BaseDefense { get; set; }
        public string DeathEvent { get; set; }
        public int XP { get; set; }
        public string Speed { get; set; }


        public int GetCurrentBody()
        {
            return Math.Max(Body - Wounds, 0);
        }

        public int GetCurrentMind()
        {
            return Math.Max(Mind - Fatigue, 0);
        }

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

        public bool HasItem(string itemName)
        {
            if(Items!=null && Items.TryGetValue(itemName, out int quantity))
            {
                return quantity > 0;
            }
            return false;
        }

        public void RemoveItem(string itemName, int quantity)
        {
            if(HasItem(itemName))
            {
                if(quantity>=Items[itemName])
                {
                    Items.Remove(itemName);
                }
                else
                {
                    Items[itemName] -= quantity;
                }
            }
        }

        public void AddItem(string itemName, int quantity)
        {
            if(quantity>0)
            {
                if(Items==null)
                {
                    Items = new Dictionary<string, int>();
                }
                if(Items.ContainsKey(itemName))
                {
                    Items[itemName] += quantity;
                }
                else
                {
                    Items[itemName] = quantity;
                }
            }
        }

        public Dictionary<string, int> GetItems()
        {
            if(Items==null)
            {
                Items = new Dictionary<string, int>();
            }
            return Items;
        }

        public void Eat(string itemName, Item item)
        {
            if(HasItem(itemName) && item.ItemType == ItemType.Food)
            {
                RemoveItem(itemName, 1);
                Wounds = Math.Max(0, Wounds - item.Body);
            }
        }

        public HashSet<string> GetEquipped()
        {
            if(Equipped==null)
            {
                Equipped = new HashSet<string>();
            }
            return Equipped;
        }
        public bool HasEquipped(string itemName)
        {
            return GetEquipped().Contains(itemName);
        }

        public bool CanEquip(Item itemDescriptor, Dictionary<string,Item> items)
        {
            var slots = itemDescriptor.GetEquipSlots();
            if (slots.Any())
            {
                slots.IntersectWith(GetEquippedSlots(items));
                return !slots.Any();
            }
            else
            {
                return false;
            }
        }

        private HashSet<char> GetEquippedSlots(Dictionary<string, Item> items)
        {
            HashSet<char> slots = new HashSet<char>();
            foreach(var itemName in GetEquipped())
            {
                slots.UnionWith(items[itemName].GetEquipSlots());
            }
            return slots;
        }

        public void ChangeXP(int value)
        {
            XP += value;
        }

        public void ChangeBody(int value)
        {
            Body += value;
        }

        public void Rest()
        {
            Wounds = 0;
            Fatigue = 0;
        }

        public bool CanEat(Item itemDescriptor)
        {
            return itemDescriptor.ItemType == ItemType.Food;
        }

        public bool IsDead()
        {
            return Wounds >= Body;
        }
    }
}