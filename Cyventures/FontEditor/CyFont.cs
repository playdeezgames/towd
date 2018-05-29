using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontEditor
{
    public class CyFont
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Dictionary<int, List<List<int>>> Data { get; set; }
        public static CyFont Load(string fileName)
        {
            using (var reader = File.OpenText(fileName))
            {
                return JsonConvert.DeserializeObject<CyFont>(reader.ReadToEnd());
            }
        }
        public void Save(string fileName)
        {
            using (var writer = File.CreateText(fileName))
            {
                writer.Write(JsonConvert.SerializeObject(this));
            }
        }
        public static CyFont Create(int width, int height)
        {
            CyFont result = new CyFont
            {
                Width = width,
                Height = height,
                Data = new Dictionary<int, List<List<int>>>()
            };
            for (int index=32;index<128;++index)
            {
                result.Data[index] = new List<List<int>>();
                while(result.Data[index].Count()<height)
                {
                    result.Data[index].Add(new List<int>());
                }
            }
            return result;
        }
        public void WriteCharacter(ColorBuffer<CyColor> buffer, CyColor color, int startX, int startY, int character)
        {
            if(buffer==null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (Data.ContainsKey(character))
            {
                for (int y = 0; y < Height; ++y)
                {
                    foreach (var x in Data[character][y])
                    {
                        buffer.Put(startX + x, startY + y, color);
                    }
                }
            }
        }
        public void WriteText(ColorBuffer<CyColor> buffer, CyColor color, int startX, int startY, string text)
        {
            var bytes = Encoding.ASCII.GetBytes(text);
            foreach(var b in bytes)
            {
                WriteCharacter(buffer, color, startX, startY, b);
                startX += Width;
            }
        }
    }
}
