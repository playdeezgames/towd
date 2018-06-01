using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FilledBoxControl : MessageHandlerBase<CyColor>
    {
        public CyColor Color { get; set; }
        public FilledBoxControl(IMessageHandler<CyColor> parent, bool enabled, CyRect bounds, CyColor color) : base(parent, enabled, bounds)
        {
            Color = color;
        }

        protected override bool OnCommand(Command command)
        {
            return false;
        }

        protected override IResult OnMessage(IMessage message)
        {
            return null;
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
            pixelWriter.Clear(Color, clipRect);
        }
    }
}
