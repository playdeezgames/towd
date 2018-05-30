using System;

namespace MapEditor
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MapEditor())
                game.Run();
        }
    }
}
