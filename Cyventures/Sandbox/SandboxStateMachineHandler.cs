using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class SandboxStateMachineHandler : StateMachineHandler<CyColor, SandboxState>
    {
        public SandboxStateMachineHandler(MessageHandlerBase<CyColor> parent) : base(parent)
        {
            this[SandboxState.Splash] = new SplashStateHandler(this, null);
            Current = SandboxState.Splash;
        }
    }
}
