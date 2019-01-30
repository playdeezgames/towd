using MonoGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace TiledSharpSandbox
{
    public static class TowdConstants
    {
        public const int ScreenWidth = 160;
        public const int ScreenHeight = 100;
        public const int Zoom = 6;
    }
    class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Stage(TowdConstants.ScreenWidth, TowdConstants.ScreenHeight, TowdConstants.Zoom, Root.Create))
            {
                game.Run();
            }
        }
    //    static void Main(string[] args)
    //    {
    //        var map = new TmxMap(@"E:\TheGrumpyGameDev\tombofwoefuldoom\Resources\Maps\hometown.tmx");
    //        Console.WriteLine("Done!");
    //        Console.ReadLine();
    //    }
    }
}
