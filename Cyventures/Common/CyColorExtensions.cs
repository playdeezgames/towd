using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class CyColorExtensions
    {
        public static Color ToColor(CyColor cyColor)
        {
            switch(cyColor)
            {
                case CyColor.Black:
                    return new Color(0, 0, 0,255);
                case CyColor.DarkGray:
                    return new Color(85, 85, 85,255);
                case CyColor.LightGray:
                    return new Color(170, 170, 170,255);
                case CyColor.White:
                    return new Color(255, 255, 255,255);
                default:
                    throw new ArgumentOutOfRangeException(nameof(cyColor));
            }
        }
    }
}
