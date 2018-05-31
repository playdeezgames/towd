using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IPixelWriter<T>
    {
        CyRect? ClipRect { get; set; }
        CyPoint Offset { get; set; }
        void Put(int x, int y, T color);
        void Put(CyPoint pt, T color);
        void HLine(int x, int y, int w, T color);
        void HLine(CyPoint pt, int w, T color);
        void VLine(int x, int y, int h, T color);
        void VLine(CyPoint pt, int h, T color);
        void Box(int x, int y, int w, int h, T color);
        void Box(CyRect rect, T color);
        void Clear(T color);
    }
}
