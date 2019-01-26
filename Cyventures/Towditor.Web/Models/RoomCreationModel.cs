using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Towditor.Web.Models
{
    public class RoomCreationModel
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int WorldId { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public int TerrainId { get; set; }
    }
}
