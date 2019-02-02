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

        internal void AddTriggerFromTmxObject(TmxObject obj)
        {
            var trigger = GetTrigger(obj.Properties["ForTrigger"]);
            switch (obj.Type)
            {
                case "MakeRoomMessage":
                    trigger.Add(new TriggerEvent
                    {
                        Order = Convert.ToInt32(obj.Properties["Order"]),
                        EventType = TriggerEventType.MakeRoomMessage,
                        MakeRoomMessage = new MakeRoomMessage
                        {
                            Message = obj.Properties["Message"]
                        }
                    });
                    break;
                case "ClearSearch":
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
                    break;
                case "MakeSign":
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
                    break;
                case "GiveItem":
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
                    break;
                case "GiveMoney":
                    trigger.Add(new TriggerEvent
                    {
                        Order = Convert.ToInt32(obj.Properties["Order"]),
                        EventType = TriggerEventType.GiveMoney,
                        GiveMoney = new GiveMoney
                        {
                            Amount = Convert.ToInt32(obj.Properties["Amount"])
                        }
                    });
                    break;
            }
        }
    }
}
