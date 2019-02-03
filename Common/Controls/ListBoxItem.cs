using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ListBoxItem<TMeta>
    {
        public string Caption { get; set; }
        public TMeta Meta { get; set; }
        public static ListBoxItem<TMeta> Create(TMeta meta, string caption)
        {
            return new ListBoxItem<TMeta>
            {
                Caption = caption,
                Meta = meta
            };
        }
    }
}
