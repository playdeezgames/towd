using Common;
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
            }
        }

        private void AddDialogChoiceEvent(TmxObject obj)
        {
            var choice = GetDialogState(obj.Properties["ForDialog"]).GetNode(obj.Properties["ForState"]).GetChoice(obj.Properties["ForOption"]);
            var choiceEvent = new DialogChoiceEvent
            {
                Order = obj.GetProperty("Order", 0),
                EventType = (DialogEventType)obj.GetProperty("DialogEventType", 0),
                Shoppe = obj.GetProperty("Shoppe", string.Empty)
            };
            choice.AddEvent(choiceEvent);
        }

        private void AddDialogChoice(TmxObject obj)
        {
            var choice = GetDialogState(obj.Properties["ForDialog"]).GetNode(obj.Properties["ForState"]).GetChoice(obj.Properties["Option"]);
            choice.Order = obj.GetProperty("Order", 0);
            choice.Option = obj.Properties["Option"];
        }

        private void AddDialogNode(TmxObject obj)
        {
            var node = GetDialogState(obj.Properties["ForDialog"]).GetNode(obj.Properties["ForState"]);
            node.Prompt = obj.Properties["Prompt"];
        }

        private void AddDialogState(TmxObject obj)
        {
            GetDialogState(obj.Properties["ForDialog"]).CurrentState = obj.Properties["CurrentState"];
        }

        private DialogState GetDialogState(string name)
        {
            if(DialogStates==null)
            {
                DialogStates = new Dictionary<string, DialogState>();
            }
            if(!DialogStates.ContainsKey(name))
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
            if(ShoppeInventories==null)
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
