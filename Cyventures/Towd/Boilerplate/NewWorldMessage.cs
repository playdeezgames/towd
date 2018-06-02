using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towd
{
    public class NewWorldMessage: MessageBase
    {
        public static readonly string Id = Guid.NewGuid().ToString();
        protected NewWorldMessage() : base(Id)
        {
        }
        public static NewWorldMessage Create()
        {
            return new NewWorldMessage();
        }
    }
}
