using System;

namespace TombsPlayer
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new TombsPlayer())
                game.Run();
        }
    }
}
