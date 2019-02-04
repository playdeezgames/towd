﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Engine
{
    public static class TmxObjectExtensions
    {
        public static string GetProperty(this TmxObject obj, string propertyName, string defaultValue)
        {
            if (obj == null || obj.Properties == null || !obj.Properties.ContainsKey(propertyName))
            {
                return defaultValue;
            }
            else
            {
                return obj.Properties[propertyName];
            }
        }
        public static int GetProperty(this TmxObject obj, string propertyName, int defaultValue)
        {
            var result = obj.GetProperty(propertyName, defaultValue.ToString());
            if(int.TryParse(result, out int value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
