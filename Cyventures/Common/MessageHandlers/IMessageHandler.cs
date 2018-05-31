using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IMessageHandler
    {
        IMessageHandler Parent { get; }
        IResult HandleMessage(IMessage message);
        void Broadcast(IMessage message, bool reverseOrder = false);
        IResult HandleBroadcast(IMessage message, bool reverseOrder = false);
    }
}
