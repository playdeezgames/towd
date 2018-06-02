using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towd
{
    public class LoadWorldMessage: MessageBase
    {
        public static readonly string Id = Guid.NewGuid().ToString();
        public string FileName { get; private set; }
        protected LoadWorldMessage(string fileName) : base(Id)
        {
            FileName = fileName;
        }
        public static LoadWorldMessage Create(string fileName)
        {
            return new LoadWorldMessage(fileName);
        }
    }
}
