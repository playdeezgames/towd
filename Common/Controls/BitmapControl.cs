using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BitmapControl<T> : MessageHandlerBase<T>
    {
        public Bitmap<T> Bitmap { get; set; }
        public Func<T,bool> Filter { get; set; }
        public BitmapControl(IMessageHandler<T> parent, bool enabled, CyPoint offset, Bitmap<T> bitmap, Func<T,bool> filter) 
            : base(parent, enabled, CyRect.Create(offset.X,offset.Y, bitmap.Width, bitmap.Height ))
        {
            Bitmap = bitmap;
            Filter = filter;
        }

        protected override bool OnCommand(Command command)
        {
            return false;
        }

        protected override IResult OnMessage(IMessage message)
        {
            return null;
        }

        protected override void OnUpdate(IPixelWriter<T> pixelWriter, CyRect? clipRect)
        {
            Bitmap.Draw(pixelWriter, CyPoint.Create(0, 0), Filter, clipRect);
        }
    }
}
