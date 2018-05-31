using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class SandboxRoot : MessageHandlerBase<CyColor>
    {
        CyFont _font;
        public SandboxRoot(IMessageHandler<CyColor> parent)
            :base(parent, true, null)
        {
            _font = Utility.LoadEmbedded<CyFont>(Assembly.GetExecutingAssembly(), "Sandbox.CyFont5x7.json");
        }
        public static IMessageHandler<CyColor> Create(IMessageHandler<CyColor> parent)
        {
            return new SandboxRoot(parent);
        }

        protected override bool OnCommand(Command command)
        {
            switch(command)
            {
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
        }
    }
}
