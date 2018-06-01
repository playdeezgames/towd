using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class FetchResult : ResultBase
    {
        public static readonly string Id = Guid.NewGuid().ToString();
        public FetchResult() : base(Id)
        {
        }
    }
    public class FetchResult<TPayload> : FetchResult
    {
        public TPayload Payload { get; private set; }
        public FetchResult(TPayload payload):base()
        {
            Payload = payload;
        }
    }
}
