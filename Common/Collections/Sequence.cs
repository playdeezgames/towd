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
        public int Count => Items.Count;
        public T this[int index]
        {
            get
            {
                if(index>=0 && index<Count)
                {
                    return Items[index];
                }
                else
                {
                    return default(T);
                }
            }
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
