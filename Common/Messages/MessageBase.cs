using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MessageBase : IMessage
    {
        public string MessageId { get; private set; }
        private MessageBase() { }
        protected MessageBase(string messageId)
        {
            MessageId = messageId;
        }
    }
}
