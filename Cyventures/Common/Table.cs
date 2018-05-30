using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Table<K,V>
    {
        public Dictionary<K,V> LookUp { get; set; }
        public Table()
        {
            LookUp = new Dictionary<K, V>();
        }
    }
}
