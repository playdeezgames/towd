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
    public class TileMap<T> : PixelWriterBase<T>
    {
        public List<List<T>> Data { get; set; }
        public TileMap(): base(0,0)
        {
            Data = new List<List<T>>();
        }
        public TileMap(int width, int height, T color)
            :base(width,height)
        {
            Data = new List<List<T>>();
            for (int row = 0; row < height; ++row)
            {
                List<T> line = new List<T>();
                Data.Add(line);
                for (int column = 0; column < width; ++column)
                {
                    line.Add(color);
                }
            }
        }
        public override void DoPut(int x, int y, T color)
        {
            if(x>=0 && x<Width && y>=0 && y<Height)
            {
                Data[y][x] = color;
            }
        }
        public void Draw(IPixelWriter<T> buffer, int x, int y, Func<T, bool> test)
        {
            for (int row = 0; row < Height; ++row)
            {
                for (int column = 0; column < Width; ++column)
                {
                    T color = Data[row][column];
                    if (test(color))
                    {
                        buffer.Put(x + column, y + row, color);
                    }
                }
            }
        }
    }
}
