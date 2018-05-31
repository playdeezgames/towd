using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Sequence<T>
    {
        public List<T> Items { get; set; }
        public Sequence()
        {
            Items = new List<T>();
        }
        public void Append(T item)
        {
            Items.Add(item);
        }
        public void Append(Sequence<T> sequence)
        {
            Items.AddRange(sequence.Items);
        }
    }
}
