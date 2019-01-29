using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Towditor.Web.Models
{
    public class RoomEditorTileModel
    {
        public int TileId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int TerrainId { get; set; }
        public static RoomEditorTileModel FromTile(EFModel.Tiles tile)
        {
            return new RoomEditorTileModel
            {
                TileId=tile.TileId,
                X = tile.X,
                Y = tile.Y,
                TerrainId=tile.TerrainId
            };
        }
    }
}
