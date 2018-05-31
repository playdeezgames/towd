using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IMessageHandler<T>
    {
        int X { get; set; }
        int Y { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        int GlobalX { get; }
        int GlobalY { get; }
        bool Enabled { get; set; }
        bool GlobalEnabled { get; }
        IMessageHandler<T> Parent { get; }
        IResult HandleMessage(IMessage message);
        void Update(IPixelWriter<T> pixelWriter);
        bool HandleCommand(Command command);
    }
}
