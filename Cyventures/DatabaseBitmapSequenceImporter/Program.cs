using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBitmapSequenceImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Starting....");
            foreach (var arg in args)
            {
                Console.WriteLine($"Starting file '{arg}'");
                var bitmapSequence = Utility.Load<Sequence<Bitmap<CyColor>>>(arg);
                StoreBitmapSequence(Path.GetFileNameWithoutExtension(arg), bitmapSequence);
                Console.WriteLine($"Done with file '{arg}'");
            }
            Console.WriteLine($"Done!");
        }

        private static void StoreBitmapSequence(string name, Sequence<Bitmap<CyColor>> bitmapSequence)
        {
            using (var db = new TOWDEntities())
            {
                if (db.BitmapSequences.Any(x => x.BitmapSequenceName == name))
                {
                    Console.WriteLine($"BitmapSequence with name {name} already exists!");
                }
                else
                {
                    Console.WriteLine($"Start creating bitmapSequence named {name}");
                    var dbBitmapSequence = new BitmapSequence()
                    {
                        BitmapSequenceName = name,
                        Bitmaps = ToBitmaps(bitmapSequence.Items)
                    };
                    db.BitmapSequences.Add(dbBitmapSequence);
                    db.SaveChanges();
                    Console.WriteLine($"Done creating bitmapSequence named {name}");
                }
            }
        }

        private static ICollection<Bitmap> ToBitmaps(List<Bitmap<CyColor>> items)
        {
            List<Bitmap> result = new List<Bitmap>();
            int index = 0;
            foreach(var item in items)
            {
                result.Add(new Bitmap
                {
                    BitmapIndex=index++,
                    BitmapHeight=item.Height,
                    BitmapWidth=item.Width,
                    BitmapPixels = ToBitmapPixels(item.Width, item.Pixels)
                });
            }
            return result;
        }

        private static ICollection<BitmapPixel> ToBitmapPixels(int width, CyColor[] pixels)
        {
            List<BitmapPixel> result = new List<BitmapPixel>();
            int index = 0;
            foreach(var pixel in pixels)
            {
                result.Add(new BitmapPixel
                {
                    X=index % width,
                    Y=index / width,
                    ColorId=(int)pixel
                });
                ++index;
            }
            return result;
        }
    }
}

