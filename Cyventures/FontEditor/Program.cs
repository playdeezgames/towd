﻿using System;

namespace FontEditor
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Sandbox())
                game.Run();
        }
    }
}
