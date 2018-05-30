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
    public class CyBitmap : TileMap<CyColor>
    {
        public CyBitmap(int width, int height, CyColor color)
            :base(width,height, color)
        {
        }
    }
}
