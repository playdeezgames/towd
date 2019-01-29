using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Towditor.Web.Models
{
    public class RoomEditorRoomModel
    {
        public int RoomId { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public ICollection<RoomEditorTileModel> Tiles { get; set; }
        public static RoomEditorRoomModel FromRoom(EFModel.Rooms room)
        {
            return new RoomEditorRoomModel
            {
                RoomId=room.RoomId,
                Columns=room.Tiles.Max(x=>x.X)+1,
                Rows=room.Tiles.Max(x=>x.Y)+1,
                Tiles = room.Tiles.Select(RoomEditorTileModel.FromTile).ToList()
            };
        }
    }
}
