﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Engine
{
    public class World
    {
        public string Avatar { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public Dictionary<string, Terrain> Terrains { get; set; }
        public Dictionary<string, Creature> Creatures { get; set; }
        public Dictionary<string, Room> Rooms { get; set; }
        public Dictionary<string, CreatureInstance> CreatureInstances { get; set; }

        public Room GetAvatarRoom()
        {
            return GetRoom(GetAvatarCreatureInstance().Room);
        }
        public Room GetRoom(string roomName)
        {
            var room = Rooms[roomName];
            if (!room.Loaded)
            {
                room = LoadRoomFromFile(room.FileName);
            }
            return room;
        }
        public void ActivateTrigger(string trigger)
        {
            var room = GetAvatarRoom();
            var triggerEvents = room.GetTrigger(trigger).OrderBy(x => x.Order);
            foreach (var triggerEvent in triggerEvents)
            {
                switch (triggerEvent.EventType)
                {
                    case TriggerEventType.ClearSearch:
                        DoClearSearch(triggerEvent.ClearSearch);
                        break;
                    case TriggerEventType.MakeSign:
                        DoMakeSign(triggerEvent.MakeSign);
                        break;
                }
            }
        }

        private void DoMakeSign(MakeSign makeSign)
        {
            var room = GetAvatarRoom();
            var roomTile = room.Get(makeSign.Column, makeSign.Row);
            roomTile.RoleOverride = RoomTileRole.Sign;
            roomTile.Sign = new Sign
            {
                Message = makeSign.Message
            };
        }

        private void DoClearSearch(ClearSearch clearSearch)
        {
            var room = GetAvatarRoom();
            var roomTile = room.Get(clearSearch.Column, clearSearch.Row);
            if (roomTile.RoleOverride == RoomTileRole.Search)
            {
                roomTile.RoleOverride = null;
                roomTile.Search = null;
            }
        }

        private Room LoadRoomFromFile(string fileName)
        {
            var map = new TmxMap(fileName);
            Room room = new Room(map.Width, map.Height);
            room.Caption = map.Properties["RoomCaption"];
            string roomIdentifier = map.Properties["RoomId"];
            room.Loaded = true;
            room.Triggers = new Dictionary<string, List<TriggerEvent>>();
            Dictionary<int, string> terrains = new Dictionary<int, string>();
            foreach(var tileset in map.Tilesets)
            {
                foreach(var entry in tileset.Tiles)
                {
                    var tile = entry.Value;
                    int gid = tile.Id + tileset.FirstGid;
                    switch(tile.Type)
                    {
                        case "Terrain":
                            AddTerrain(tile);
                            terrains[gid] = tile.Properties["Name"];
                            break;
                        case "Creature":
                            AddCreature(tile);
                            break;
                    }
                }
            }
            var tileLayer = map.Layers["RoomTiles"];
            foreach(var tile in tileLayer.Tiles)
            {
                var roomTile = new RoomTile();
                roomTile.Terrain = terrains[tile.Gid];
                room.Set(tile.X, tile.Y, roomTile);
            }
            var objectLayer = map.ObjectGroups["Events"];
            foreach(var obj in objectLayer.Objects)
            {
                var roomTile = room.TryGet((int)(obj.X / obj.Width), (int)(obj.Y / obj.Height));
                switch (obj.Type)
                {
                    case "ClearSearch":
                        room.GetTrigger(obj.Properties["ForTrigger"]).Add(new TriggerEvent
                        {
                            Order = Convert.ToInt32(obj.Properties["Order"]),
                            EventType= TriggerEventType.ClearSearch,
                            ClearSearch=new ClearSearch
                            {
                                Column = Convert.ToInt32(obj.Properties["Column"]),
                                Row = Convert.ToInt32(obj.Properties["Row"])
                            }
                        });
                        break;
                    case "MakeSign":
                        room.GetTrigger(obj.Properties["ForTrigger"]).Add(new TriggerEvent
                        {
                            Order = Convert.ToInt32(obj.Properties["Order"]),
                            EventType = TriggerEventType.MakeSign,
                            MakeSign = new MakeSign
                            {
                                Column = Convert.ToInt32(obj.Properties["Column"]),
                                Row = Convert.ToInt32(obj.Properties["Row"]),
                                Message=obj.Properties["Message"]
                            }
                        });
                        break;
                    case "Teleport":
                        Teleport teleport = new Teleport
                        {
                            Room = obj.Properties["Room"],
                            Prompt = obj.Properties["Prompt"],
                            Column = Convert.ToInt32(obj.Properties["Column"]),
                            Row = Convert.ToInt32(obj.Properties["Row"])
                        };
                        roomTile.RoleOverride = RoomTileRole.Teleport;
                        roomTile.Teleport = teleport;
                        break;
                    case "Sign":
                        Sign sign = new Sign
                        {
                            Message=obj.Properties["Message"]
                        };
                        roomTile.RoleOverride = RoomTileRole.Sign;
                        roomTile.Sign = sign;
                        break;
                    case "Search":
                        Search search = new Search
                        {
                            Prompt=obj.Properties["Prompt"],
                            Trigger=obj.Properties["Trigger"]
                        };
                        roomTile.Search = search;
                        roomTile.RoleOverride = RoomTileRole.Search;
                        break;
                }
            }
            objectLayer = map.ObjectGroups["CreatureInstances"];
            foreach (var obj in objectLayer.Objects)
            {
                int column = (int)(obj.X / obj.Width);
                int row = (int)(obj.Y / obj.Height);
                switch (obj.Type)
                {
                    case "CreatureInstance":
                        string identifier = obj.Properties["Identifier"];
                        CreatureInstance creatureInstance = new CreatureInstance
                        {
                            Column = column,
                            Room = roomIdentifier,
                            Row =row,
                            Creature=obj.Properties["Creature"]
                        };
                        CreatureInstances[identifier] = creatureInstance;
                        room.Get(column, row).CreatureInstance = identifier;
                        break;
                }
            }
            Rooms[roomIdentifier] = room;
            return room;
        }

        private void AddCreature(TmxTilesetTile tile)
        {
            if(!Creatures.ContainsKey(tile.Properties["Name"]))
            {
                Creature creature = new Creature
                {
                    ResourceIdentifier = tile.Properties["ResourceIdentifier"],
                    ResourceIndex = Convert.ToInt32(tile.Properties["ResourceIndex"])
                };
                Creatures[tile.Properties["Name"]] = creature;
            }

        }

        private void AddTerrain(TmxTilesetTile tile)
        {
            if(!Terrains.ContainsKey(tile.Properties["Name"]))
            {
                Terrain terrain = new Terrain
                {
                    ResourceIdentifier = tile.Properties["ResourceIdentifier"],
                    ResourceIndex = Convert.ToInt32(tile.Properties["ResourceIndex"]),
                    Role = (RoomTileRole)Convert.ToInt32(tile.Properties["Role"])
                };
                Terrains[tile.Properties["Name"]] = terrain;
            }
        }

        public CreatureInstance GetAvatarCreatureInstance()
        {
            return CreatureInstances[Avatar];
        }

        public bool IsTerrainInUse(string terrain)
        {
            return Rooms.Any(x => x.Value.IsTerrainInUse(terrain));
        }
    }
}
