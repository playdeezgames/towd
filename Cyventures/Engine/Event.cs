using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Event
    {
        public EventType EventType { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public List<string> Branches { get; set; }
    }
}
