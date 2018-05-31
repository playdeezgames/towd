using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class MessageBase : IMessage
    {
        public string MessageId { get; private set; }
        private MessageBase() { }
        public MessageBase(string messageId)
        {
            MessageId = messageId;
        }
    }
}
