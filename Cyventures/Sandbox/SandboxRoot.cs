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
        public static IMessageHandler Create(IMessageHandler parent)
        {
            return new SandboxRoot(parent);
        }

        protected override bool OnCommand(CommandMessage message)
        {
            switch(message.Command)
            {
                case Command.Back:
                    HandleMessage(new QuitMessage());
                    return true;
                default:
                    return false;
            }
        }

        protected override void OnDraw(DrawMessage message)
        {
            IPixelWriter<CyColor> pixelWriter = (message as DrawMessage<CyColor>).PixelWriter;
            if(pixelWriter!=null)
            {
                pixelWriter.Box(CyRect.Create(0, 0, 10, 10), CyColor.LightGray);
            }
        }

        protected override void OnInitialize(InitializeMessage message)
        {
            
        }
    }
}
