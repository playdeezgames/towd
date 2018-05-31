using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public struct CyPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public static CyPoint Create(int x, int y)
        {
            return new CyPoint() { X = x, Y = y };
        }
    }
}
