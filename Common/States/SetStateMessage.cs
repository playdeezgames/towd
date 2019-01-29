using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SetStateMessage:MessageBase
    {
        public static string Id = Guid.NewGuid().ToString();
        protected SetStateMessage():base(Id)
        {
        }
    }
    public class SetStateMessage<TState>:SetStateMessage
    {
        public TState State { get; private set; }
        private SetStateMessage() { }
        protected SetStateMessage(TState state):base()
        {
            State = state;
        }
        public static SetStateMessage<TState> Create(TState state)
        {
            return new SetStateMessage<TState>(state);
        }
    }
}
