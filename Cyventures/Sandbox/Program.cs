﻿using Common;
using MonoGameCommon;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Sandbox
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Stage(SandboxConstants.ScreenWidth,SandboxConstants.ScreenHeight,SandboxConstants.Zoom,SandboxRoot.Create))
            {
                game.Run();
            }
        }
    }
}
