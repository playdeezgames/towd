﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public struct CyRect
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Contains(int x, int y)
        {
            return (x >= X && y >= Y && x < (X + Width) && y < (Y + Height));
        }
        public static CyRect Create(int x, int y, int width, int height)
        {
            return new CyRect() { X = x, Y = y, Width = width, Height = height };
        }
        public CyPoint Offset
        {
            get
            {
                return CyPoint.Create(X, Y);
            }
        }
    }
}
