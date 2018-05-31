using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CommandMessageOld:MessageBase
    {
        public static readonly string Id = Guid.NewGuid().ToString();
        public Command Command { get; private set; }
        public CommandMessageOld(Command command):base(Id)
        {
            Command = command;
        }
    }
}
