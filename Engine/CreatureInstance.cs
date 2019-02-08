﻿using System;
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
        public HashSet<string> Equipped { get; set; }
        public string Dialog { get; set; }
        //COMBAT
        public int Body { get; set; }
        public int Wounds { get; set; }
        public int Mind { get; set; }
        public string UnarmedAttack { get; set; }
        public string BaseDefense { get; set; }

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
    }
}