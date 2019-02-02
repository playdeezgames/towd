using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class TriggerEvent
    {
        public int Order { get; set; }
        public TriggerEventType EventType { get; set; }
        public ClearSearch ClearSearch { get; set; }
        public MakeSign MakeSign { get; set; }
        public MakeRoomMessage MakeRoomMessage { get; set; }
        public GiveMoney GiveMoney { get; set; }
        public GiveItem GiveItem { get; set; }
    }
}
