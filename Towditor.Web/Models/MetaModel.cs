using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Towditor.Web.Models
{
    public class MetaModel<TMeta, TPayload>
    {
        public TMeta Meta { get; set; }
        public TPayload Payload { get; set; }
    }
}
