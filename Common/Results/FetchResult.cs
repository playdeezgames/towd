using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FetchResult : ResultBase
    {
        public static readonly string Id = Guid.NewGuid().ToString();
        protected FetchResult() : base(Id)
        {
        }
    }
    public class FetchResult<TPayload> : FetchResult
    {
        public TPayload Payload { get; private set; }
        protected FetchResult(TPayload payload):base()
        {
            Payload = payload;
        }
        public static FetchResult<TPayload> Create(TPayload payload)
        {
            return new FetchResult<TPayload>(payload);
        }
    }
}
