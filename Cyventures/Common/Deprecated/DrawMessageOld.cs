using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class DrawMessageOld : MessageBase
    {
        public static string Id = Guid.NewGuid().ToString();
        public DrawMessageOld() : base(Id)
        {
        }
    }
    public class DrawMessage<T> : DrawMessageOld
    {
        public IPixelWriter<T> PixelWriter { get; private set; }
        public DrawMessage(IPixelWriter<T> pixelWriter) : base()
        {
            PixelWriter = pixelWriter;
        }
    }
}
