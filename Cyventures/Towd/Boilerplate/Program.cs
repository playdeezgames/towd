using MonoGameCommon;
using System;

namespace Towd
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Stage(TowdConstants.ScreenWidth, TowdConstants.ScreenHeight, TowdConstants.Zoom,Root.Create))
            {
                game.Run();
            }
        }
    }
}
