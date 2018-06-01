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
        private Manager<SandboxFont, CyFont> _fontManager;
        private Manager<SandboxBitmap, Bitmap<CyColor>> _bitmapManager;
        public SandboxRoot(IMessageHandler<CyColor> parent)
            :base(parent, true, null)
        {
            _fontManager = new Manager<SandboxFont, CyFont>
                (k => Utility.LoadEmbedded<CyFont>(Assembly.GetExecutingAssembly(),
                    (k == SandboxFont.Largest) ? ("Sandbox.CyFont8x8.json") :
                    (k == SandboxFont.Large) ? ("Sandbox.CyFont5x7.json") :
                    (k == SandboxFont.Medium) ? ("Sandbox.CyFont4x6.json") : 
                    ("Sandbox.CyFont3x5.json")));

            _bitmapManager = new Manager<SandboxBitmap, Bitmap<CyColor>>
                (k => Utility.LoadEmbedded<Bitmap<CyColor>>(Assembly.GetExecutingAssembly(), "Sandbox.TestBitmap.json"));

            new SandboxStateMachineHandler(this);
        }
        public static IMessageHandler<CyColor> Create(IMessageHandler<CyColor> parent)
        {
            return new SandboxRoot(parent);
        }

        protected override bool OnCommand(Command command)
        {
            return false;
        }

        protected override IResult OnMessage(IMessage message)
        {
            if(message.MessageId == FetchMessage.Id)
            {
                switch((message as FetchMessage<SandboxResource>)?.Resource ?? SandboxResource.None)
                {

                    case SandboxResource.BitmapManager:
                        return FetchResult<Manager<SandboxBitmap, Bitmap<CyColor>>>.Create(_bitmapManager);

                    case SandboxResource.FontManager:
                        return FetchResult<Manager<SandboxFont, CyFont>>.Create(_fontManager);
                    default:
                        return AckResult<CyColor>.Create(message, this);
                }
            }
            return null;
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
        }
    }
}
