using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AckResult: ResultBase
    {
        public IMessage Message { get; private set; }
        public static readonly string Id = Guid.NewGuid().ToString();
        public AckResult(IMessage message):base(Id)
        {
            Message = message;
        }
    }
}
