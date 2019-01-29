using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFontImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Starting....");
            foreach (var arg in args)
            {
                Console.WriteLine($"Starting file '{arg}'");
                var font = Utility.Load<CyFont>(arg);
                StoreFont(Path.GetFileNameWithoutExtension(arg),font);
                Console.WriteLine($"Done with file '{arg}'");
            }
            Console.WriteLine($"Done!");
        }

        private static void StoreFont(string name, CyFont font)
        {
            using (var db = new TOWDEntities())
            {
                if(db.Fonts.Any(x=>x.FontName==name))
                {
                    Console.WriteLine($"Font with name {name} already exists!");
                }
                else
                {
                    Console.WriteLine($"Start creating font named {name}");
                    var dbFont = new Font()
                    {
                        FontName = name,
                        FontHeight = font.Height,
                        Glyphs = font.Glyphs.Select(x => new Glyph()
                        {
                            GlyphCharacter = x.Key,
                            GlyphWidth = x.Value.Width,
                            GlyphPixels = ToGlyphPixels(x.Value.Lines)
                        }).ToList()
                    };
                    db.Fonts.Add(dbFont);
                    db.SaveChanges();
                    Console.WriteLine($"Done creating font named {name}");
                }
            }
        }

        private static ICollection<GlyphPixel> ToGlyphPixels(Dictionary<int, List<int>> lines)
        {
            List<GlyphPixel> result = new List<GlyphPixel>();
            foreach(var line in lines)
            {
                foreach(var column in line.Value)
                {
                    result.Add(new GlyphPixel() { X=column, Y=line.Key });
                }
            }
            return result;
        }
    }
}
