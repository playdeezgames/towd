using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Towditor.Web.Models
{
    public class TerrainModel
    {
        public int TerrainId { get; set; }
        public int BitmapId { get; set; }
        public string TerrainName { get; set; }
        public int TileRoleId { get; set; }
        public static TerrainModel FromTerrain(EFModel.Terrains terrain)
        {
            return new TerrainModel
            {
                TerrainId=terrain.TerrainId,
                TerrainName=terrain.TerrainName,
                BitmapId=terrain.BitmapId,
                TileRoleId=terrain.TileRoleId
            };
        }
    }
}
