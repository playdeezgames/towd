using Common;
using MonoGameCommon;
using System;

namespace Sandbox
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new CommonGame(160,100,2,SandboxRoot.Create))
            {
                game.Run();
            }
        }
    }
}
