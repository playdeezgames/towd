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
        public QuitMessage():base(Id)
        {
        }
    }
}
