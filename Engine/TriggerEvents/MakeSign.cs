using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class MakeSign: MakeRoomMessage
    {
        public int Column { get; set; }
        public int Row { get; set; }
    }
}
