using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class FetchMessage:MessageBase
    {
        public static string Id = Guid.NewGuid().ToString();
        public FetchMessage():base(Id)
        {
        }
    }
    public class FetchMessage<TResource>:FetchMessage
    {
        public TResource Resource { get; private set; }
        public FetchMessage(TResource resource)
        {
            Resource = resource;
        }
    }
}
