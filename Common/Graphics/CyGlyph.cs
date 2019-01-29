using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CyGlyph
    {
        public int Width { get; set; }
        public Dictionary<int,List<int>> Lines { get; set; }
        public CyGlyph(int width)
        {
            Width = width;
            Lines = new Dictionary<int, List<int>>();
        }
        public void Set(int x, int y)
        {
            Lines = Lines ?? new Dictionary<int, List<int>>();
            Lines.TryGetValue(y, out List<int> line);
            line = line ?? new List<int>();
            line.Add(x);
            Lines[y] = line.Distinct().ToList();
        }
        public void Clear(int x, int y)
        {
            Lines = Lines ?? new Dictionary<int, List<int>>();
            Lines.TryGetValue(y, out List<int> line);
            if(line!=null)
            {
                if(line.Contains(x))
                {
                    line.Remove(x);
                    if(line.Any())
                    {
                        Lines[y] = line.Distinct().ToList();
                    }
                    else
                    {
                        Lines.Remove(y);
                    }
                }
            }
        }
        public bool Get(int x, int y)
        {
            Lines.TryGetValue(y, out List<int> line);
            return line?.Contains(x) ?? false;
        }
        public void Toggle(int x, int y)
        {
            if(Get(x,y))
            {
                Clear(x, y);
            }
            else
            {
                Set(x, y);
            }
        }
        public int Draw<T>(IPixelWriter<T> pixelWriter, T color, int x, int y, CyRect? clipRect=null)
        {
            foreach(var entry in Lines)
            {
                foreach(var column in entry.Value)
                {
                    pixelWriter.Put(column + x, y + entry.Key, color, clipRect);
                }
            }
            return x + Width;
        }
        public CyPoint Draw<T>(IPixelWriter<T> pixelWriter, T color, CyPoint pt, CyRect? clipRect = null)
        {
            return CyPoint.Create(Draw(pixelWriter, color, pt.X, pt.Y, clipRect), pt.Y);
        }
    }
}
