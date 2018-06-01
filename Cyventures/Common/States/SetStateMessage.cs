using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class SetStateMessage:MessageBase
    {
        public static string Id = Guid.NewGuid().ToString();
        public SetStateMessage():base(Id)
        {
        }
    }
    public class SetStateMessage<TState>:SetStateMessage
    {
        public TState State { get; private set; }
        private SetStateMessage() { }
        public SetStateMessage(TState state):base()
        {
            State = state;
        }
    }
}
