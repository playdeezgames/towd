using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AckResult<T> : ResultBase
    {
        public IMessage Message { get; private set; }
        public IMessageHandler<T> Handler { get; private set; }
        public static readonly string Id = Guid.NewGuid().ToString();
        public AckResult(IMessage message, IMessageHandler<T> handler):base(Id)
        {
            Message = message;
            Handler = handler;
        }
    }
}
