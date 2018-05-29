using System;

namespace FontEditor
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Sandbox())
                game.Run();
        }
    }
#endif
}
