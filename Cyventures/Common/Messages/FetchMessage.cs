using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FetchMessage:MessageBase
    {
        public static string Id = Guid.NewGuid().ToString();
        protected FetchMessage():base(Id)
        {
        }
    }
    public class FetchMessage<TResource>:FetchMessage
    {
        public TResource Resource { get; private set; }
        protected FetchMessage(TResource resource)
        {
            Resource = resource;
        }
        public static FetchMessage<TResource> Create(TResource resource)
        {
            return new FetchMessage<TResource>(resource);
        }
    }
}
