using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Towditor.Web.Models
{
    public class RoomDuplicationModel
    {
        public int TemplateRoomId { get; set; }
        public string TemplateRoomName { get; set; }
        public string DuplicateRoomName { get; set; }
    }
}
