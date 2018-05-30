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
    public class CyBitmap : PixelWriterBase<CyColor>
    {
        public List<List<CyColor>> Data { get; set; }
        public CyBitmap(): base(0,0)
        {
            Data = new List<List<CyColor>>();
        }
        public CyBitmap(int width, int height, CyColor color)
            :base(width,height)
        {
            for (int row = 0; row < height; ++row)
            {
                List<CyColor> line = new List<CyColor>();
                Data.Add(line);
                for (int column = 0; column < width; ++column)
                {
                    line.Add(color);
                }
            }
        }

        public static CyBitmap Load(string fileName)
        {
            using (var reader = File.OpenText(fileName))
            {
                return JsonConvert.DeserializeObject<CyBitmap>(reader.ReadToEnd());
            }
        }
        public static CyBitmap LoadEmbedded(Assembly assembly, string resourceName)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<CyBitmap>(reader.ReadToEnd());
            }
        }
        public override void Put(int x, int y, CyColor color)
        {
            if(x>=0 && x<Width && y>=0 && y<Height)
            {
                Data[y][x] = color;
            }
        }
        public void Draw(IPixelWriter<CyColor> buffer, int x, int y, CyColor? transparent)
        {
            for(int row=0;row<Height;++row)
            {
                for(int column=0;column<Width;++column)
                {
                    CyColor color = Data[row][column];
                    if(!transparent.HasValue || transparent.Value!=color)
                    {
                        buffer.Put(x + column, y + row, color);
                    }
                }
            }
        }
    }
}
