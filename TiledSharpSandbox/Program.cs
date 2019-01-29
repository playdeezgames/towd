using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace TiledSharpSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = new TmxMap(@"E:\TheGrumpyGameDev\tombofwoefuldoom\Resources\Maps\hometown.tmx");
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
