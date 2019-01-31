using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Room : Bitmap<RoomTile>
    {
        public string Caption { get; set; }
        public string FileName { get; set; }
        public bool Loaded { get; set; }

        public Room(int width, int height) : base(width, height)
        {
        }

        public RoomTileRole GetTileRole(int column, int row, Dictionary<string, Terrain> terrains)
        {
            if(column<0 || column>=Width)
            {
                return RoomTileRole.Solid;
            }
            if(row<0 || row>=Height)
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
    }
}
