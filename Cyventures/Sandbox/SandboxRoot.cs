using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class SandboxRoot : MessageHandlerBase<CyColor>
    {
        public SandboxRoot(IMessageHandler<CyColor> parent)
            :base(parent, true, CyRect.Create(parent.Width/2-parent.Width/4,parent.Height/2-parent.Height/4,parent.Width/2,parent.Height/2))
        {

        }
        public static IMessageHandler<CyColor> Create(IMessageHandler<CyColor> parent)
        {
            return new SandboxRoot(parent);
        }

        protected override bool OnCommand(Command command)
        {
            switch(command)
            {
                case Command.Up:
                    Y-=10;
                    return true;
                case Command.Down:
                    Y+=10;
                    return true;
                case Command.Back:
                    HandleMessage(new QuitMessage());
                    return true;
                default:
                    return false;
            }
        }

        protected override IResult OnMessage(IMessage message)
        {
            return null;
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
            pixelWriter.Clear(CyColor.DarkGray, clipRect);
        }
    }
}
