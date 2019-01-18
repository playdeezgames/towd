using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEditor.Models
{
    public class MetaModel<TMeta, TPayload>
    {
        public TMeta Meta { get; set; }
        public TPayload Payload { get; set; }
    }
}