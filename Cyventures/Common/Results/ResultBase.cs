using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class ResultBase : IResult
    {
        public string ResultId { get; private set; }
        private ResultBase() { }
        public ResultBase(string resultId)
        {
            ResultId = resultId;
        }
    }
}
