using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class LabelControl : MessageHandlerBase<CyColor>
    {
        public CyFont Font { get; set; }
        public string Text { get; set; }
        public CyColor Color { get; set; }
        public LabelControl(IMessageHandler<CyColor> parent, bool enabled, CyPoint offset, CyFont font, string text, CyColor color) 
            : this(parent, enabled, font.GetBounds(text).OffsetBy(offset), font, text, color)
        {

        }
        public LabelControl(IMessageHandler<CyColor> parent, bool enabled, CyRect bounds, CyFont font, string text, CyColor color) 
            : base(parent, enabled, bounds)
        {
            Font = font;
            Text = text;
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
            Font.Draw(pixelWriter, Color, 0, 0, Text, clipRect);
        }
    }
}
