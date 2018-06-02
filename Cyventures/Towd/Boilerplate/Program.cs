using Common;
using MonoGameCommon;
using System;
using System.Collections.Generic;
using System.Reflection;

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
