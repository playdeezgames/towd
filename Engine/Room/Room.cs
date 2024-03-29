﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Engine
{
    public class Room : Bitmap<RoomTile>
    {
        public string Caption { get; set; }
        public string FileName { get; set; }
        public bool Loaded { get; set; }
        public Dictionary<string, List<TriggerEvent>> Triggers { get; set; }
        public Queue<string> RoomMessages { get; set; }
        public Dictionary<string, ShoppeInventory> ShoppeInventories { get; set; }
        public Dictionary<string, DialogState> DialogStates { get; set; }
        public Dictionary<string, bool> RoomFlags { get; set; }
        public Dictionary<string, List<CreatureDeathEvent>> DeathEvents { get; set; }

        public bool HasMessage()
        {
            return (RoomMessages?.Count() ?? 0) > 0;
        }
        public string GetNextMessage()
        {
            return RoomMessages?.Peek();
        }
        public string AcknowledgeNextMessage()
        {
            return RoomMessages?.Dequeue();
        }
        public void AddMessage(string message)
        {
            if (RoomMessages == null)
            {
                RoomMessages = new Queue<string>();
            }
            RoomMessages.Enqueue(message);
        }

        public Room(int width, int height) : base(width, height)
        {
        }

        public RoomTile TryGet(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                return Get(x, y);
            }
            else
            {
                return null;
            }
        }


        public List<TriggerEvent> GetTrigger(string name)
        {
            if (Triggers.TryGetValue(name, out var tiggerEvents))
            {
                return tiggerEvents;
            }
            else
            {
                var triggerEvents = new List<TriggerEvent>();
                Triggers[name] = triggerEvents;
                return triggerEvents;
            }
        }

        public RoomTileRole GetTileRole(int column, int row, Dictionary<string, Terrain> terrains)
        {
            if (column < 0 || column >= Width)
            {
                return RoomTileRole.Solid;
            }
            if (row < 0 || row >= Height)
            {
                return RoomTileRole.Solid;
            }
            var tile = Get(column, row);
            return tile.RoleOverride ?? terrains[tile.Terrain].Role;
        }

        internal bool IsTerrainInUse(string terrain)
        {
            return Pixels.Any(x => x.Terrain == terrain);
        }

        internal void AddEventFromTmxObject(TmxObject obj)
        {
            switch (obj.Type)
            {
                case "CreatureDeathEvent":
                    AddCreatureDeathEvent(obj);
                    break;
                case "DialogChoiceCondition":
                    AddDialogChoiceCondition(obj);
                    break;
                case "RoomFlag":
                    AddRoomFlag(obj);
                    break;
                case "DialogState":
                    AddDialogState(obj);
                    break;
                case "DialogNode":
                    AddDialogNode(obj);
                    break;
                case "DialogChoice":
                    AddDialogChoice(obj);
                    break;
                case "DialogChoiceEvent":
                    AddDialogChoiceEvent(obj);
                    break;
                case "Buying":
                    AddBuying(obj);
                    break;
                case "Selling":
                    AddSelling(obj);
                    break;
                case "MakeRoomMessage":
                    AddMakeRoomMessage(obj);
                    break;
                case "ClearSearch":
                    AddClearSearch(obj);
                    break;
                case "MakeSign":
                    AddMakeSign(obj);
                    break;
                case "GiveItem":
                    AddGiveItem(obj);
                    break;
                case "GiveMoney":
                    AddGiveMoney(obj);
                    break;
                default:
                    break;
            }
        }

        private void AddCreatureDeathEvent(TmxObject obj)
        {
            var deathEvent = new CreatureDeathEvent
            {
                Order = obj.GetProperty("Order",0),
                Counter = obj.GetProperty("Counter", string.Empty)
            };
            AddCreatureDeathEvent(obj.Properties["ForEvent"], deathEvent);
        }

        private void AddCreatureDeathEvent(string eventName, CreatureDeathEvent deathEvent)
        {
            GetDeathEvent(eventName).Add(deathEvent);
        }

        public void MakeTeleport(int column, int row, string prompt, string destinationRoom, int destinationColumn, int destinationRow)
        {
            var roomTile = Get(column, row);
            roomTile.RoleOverride = RoomTileRole.Teleport;
            roomTile.Teleport = new Teleport
            {
                Prompt = prompt,
                Room = destinationRoom,
                Column = destinationColumn,
                Row = destinationRow
            };
        }

        public void SetRoomFlag(string flag)
        {
            if (RoomFlags == null)
            {
                RoomFlags = new Dictionary<string, bool>();
            }
            RoomFlags[flag] = true;
        }

        public bool GetFlag(string flagName)
        {
            if (RoomFlags != null)
            {
                if (RoomFlags.ContainsKey(flagName))
                {
                    return RoomFlags[flagName];
                }
            }
            return false;
        }

        private void AddDialogChoiceCondition(TmxObject obj)
        {
            var choice = GetDialogState(obj.Properties["ForDialog"]).GetNode(obj.Properties["ForState"]).GetChoice(obj.Properties["ForOption"]);
            choice.AddCondition(new DialogChoiceCondition
            {
                ConditionType = (DialogConditionType)obj.GetProperty("ConditionType", 0),
                FlagName = obj.GetProperty("FlagName", string.Empty),
                CounterName = obj.GetProperty("CounterName", string.Empty),
                Value = obj.GetProperty("Value", 0)
            });
        }

        private void AddRoomFlag(TmxObject obj)
        {
            string flagName = obj.Properties["Flag"];
            bool value = obj.GetProperty("Value", false);
            if (RoomFlags == null)
            {
                RoomFlags = new Dictionary<string, bool>();
            }
            RoomFlags[flagName] = value;
        }

        private void AddDialogChoiceEvent(TmxObject obj)
        {
            var choice = GetDialogState(obj.Properties["ForDialog"]).GetNode(obj.Properties["ForState"]).GetChoice(obj.Properties["ForOption"]);
            var choiceEvent = new DialogChoiceEvent
            {
                Order = obj.GetProperty("Order", 0),
                EventType = (DialogEventType)obj.GetProperty("DialogEventType", 0),
                Shoppe = obj.GetProperty("Shoppe", string.Empty),
                Flag = obj.GetProperty("Flag", string.Empty),
                State = obj.GetProperty("State", string.Empty),
                Column = obj.GetProperty("Column", 0),
                Row = obj.GetProperty("Row", 0),
                DestinationColumn = obj.GetProperty("DestinationColumn", 0),
                DestinationRow = obj.GetProperty("DestinationRow", 0),
                DestinationRoom = obj.GetProperty("DestinationRoom", string.Empty),
                Prompt = obj.GetProperty("Prompt", string.Empty),
                Counter = obj.GetProperty("Counter", string.Empty),
                Value = obj.GetProperty("Value", 0)
            };
            choice.AddEvent(choiceEvent);
        }

        private void AddDialogChoice(TmxObject obj)
        {
            var choice = GetDialogState(obj.Properties["ForDialog"]).GetNode(obj.Properties["ForState"]).GetChoice(obj.Properties["Option"]);
            choice.Order = obj.GetProperty("Order", 0);
            choice.Option = obj.Properties["Option"];
            choice.OptionText = obj.Properties["OptionText"];
        }

        private void AddDialogNode(TmxObject obj)
        {
            var node = GetDialogState(obj.Properties["ForDialog"]).GetNode(obj.Properties["ForState"]);
            node.Caption = obj.GetProperty("Caption", "Dialog");
            node.SetPrompt(1, obj.GetProperty("Prompt1", string.Empty));
            node.SetPrompt(2, obj.GetProperty("Prompt2", string.Empty));
            node.SetPrompt(3, obj.GetProperty("Prompt3", string.Empty));
            node.SetPrompt(4, obj.GetProperty("Prompt4", string.Empty));
        }

        private void AddDialogState(TmxObject obj)
        {
            GetDialogState(obj.Properties["ForDialog"]).CurrentState = obj.Properties["CurrentState"];
        }

        private DialogState GetDialogState(string name)
        {
            if (DialogStates == null)
            {
                DialogStates = new Dictionary<string, DialogState>();
            }
            if (!DialogStates.ContainsKey(name))
            {
                DialogStates[name] = new DialogState();
            }
            return DialogStates[name];
        }

        private void AddGiveMoney(TmxObject obj)
        {
            var trigger = GetTrigger(obj.Properties["ForTrigger"]);
            trigger.Add(new TriggerEvent
            {
                Order = Convert.ToInt32(obj.Properties["Order"]),
                EventType = TriggerEventType.GiveMoney,
                GiveMoney = new GiveMoney
                {
                    Amount = Convert.ToInt32(obj.Properties["Amount"])
                }
            });
        }

        private void AddGiveItem(TmxObject obj)
        {
            var trigger = GetTrigger(obj.Properties["ForTrigger"]);
            trigger.Add(new TriggerEvent
            {
                Order = Convert.ToInt32(obj.Properties["Order"]),
                EventType = TriggerEventType.GiveItem,
                GiveItem = new GiveItem
                {
                    ItemIdentifier = obj.Properties["ItemIdentifier"],
                    Quantity = Convert.ToInt32(obj.Properties["Quantity"])
                }
            });
        }

        private void AddMakeSign(TmxObject obj)
        {
            var trigger = GetTrigger(obj.Properties["ForTrigger"]);
            trigger.Add(new TriggerEvent
            {
                Order = Convert.ToInt32(obj.Properties["Order"]),
                EventType = TriggerEventType.MakeSign,
                MakeSign = new MakeSign
                {
                    Column = Convert.ToInt32(obj.Properties["Column"]),
                    Row = Convert.ToInt32(obj.Properties["Row"]),
                    Message = obj.Properties["Message"]
                }
            });
        }

        internal void TriggerDeathEvent(CreatureInstance enemyInstance, World world)
        {
            if(!string.IsNullOrEmpty(enemyInstance.DeathEvent))
            {
                var deathEvents = GetDeathEvent(enemyInstance.DeathEvent);
                foreach(var deathEvent in deathEvents)
                {
                    switch(deathEvent.EventType)
                    {
                        case DeathEventType.DecrementWorldCounter:
                            world.DecrementCounter(deathEvent.Counter);
                            break;
                    }
                }
            }
        }

        private List<CreatureDeathEvent> GetDeathEvent(string eventName)
        {
            if (DeathEvents == null)
            {
                DeathEvents = new Dictionary<string, List<CreatureDeathEvent>>();
            }
            if (!DeathEvents.ContainsKey(eventName))
            {
                DeathEvents[eventName] = new List<CreatureDeathEvent>();
            }
            return DeathEvents[eventName];
        }

        private void AddClearSearch(TmxObject obj)
        {
            var trigger = GetTrigger(obj.Properties["ForTrigger"]);
            trigger.Add(new TriggerEvent
            {
                Order = Convert.ToInt32(obj.Properties["Order"]),
                EventType = TriggerEventType.ClearSearch,
                ClearSearch = new ClearSearch
                {
                    Column = Convert.ToInt32(obj.Properties["Column"]),
                    Row = Convert.ToInt32(obj.Properties["Row"])
                }
            });
        }

        private void AddMakeRoomMessage(TmxObject obj)
        {
            var trigger = GetTrigger(obj.Properties["ForTrigger"]);
            trigger.Add(new TriggerEvent
            {
                Order = Convert.ToInt32(obj.Properties["Order"]),
                EventType = TriggerEventType.MakeRoomMessage,
                MakeRoomMessage = new MakeRoomMessage
                {
                    Message = obj.Properties["Message"]
                }
            });
        }

        private void AddSelling(TmxObject obj)
        {
            ShoppeInventory inventory = GetShoppeInventory(obj.Properties["ForShoppe"]);
            inventory.AddSelling(obj.Properties["ItemName"]);
        }

        public ShoppeInventory GetShoppeInventory(string shoppeName)
        {
            if (ShoppeInventories == null)
            {
                ShoppeInventories = new Dictionary<string, ShoppeInventory>();
            }
            ShoppeInventory inventory;
            if (!ShoppeInventories.TryGetValue(shoppeName, out inventory))
            {
                inventory = new ShoppeInventory();
                inventory.Selling = new List<string>();
                inventory.Buying = new List<string>();
                ShoppeInventories.Add(shoppeName, inventory);
            }
            return inventory;
        }

        private void AddBuying(TmxObject obj)
        {
            ShoppeInventory inventory = GetShoppeInventory(obj.Properties["ForShoppe"]);
            inventory.AddBuying(obj.Properties["ItemName"]);
        }
    }
}
