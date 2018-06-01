using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class QuitMessage:MessageBase
    {
        public static string Id = Guid.NewGuid().ToString();
        protected QuitMessage():base(Id)
        {
        }
        public static QuitMessage Create()
        {
            return new QuitMessage();
        }
    }
}
