using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class SandboxRoot : MessageHandlerBase
    {
        public SandboxRoot(IMessageHandler parent)
            :base(parent)
        {

        }
        protected override IResult OnMessage(IMessage message)
        {
            if (message.MessageId == CommandMessage.Id)
            {
                var command = (message as CommandMessage).Command;
                switch(command)
                {
                    case Command.Back:
                        HandleMessage(new QuitMessage());
                        break;
                }
            }
            return null;
        }
        public static IMessageHandler Create(IMessageHandler parent)
        {
            return new SandboxRoot(parent);
        }
    }
}
