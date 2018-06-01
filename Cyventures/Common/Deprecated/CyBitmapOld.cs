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
    public class CyBitmapOld : TileMap<CyColor>
    {
        public CyBitmapOld(int width, int height, CyColor color)
            :base(width,height, color)
        {
        }
    }
}
