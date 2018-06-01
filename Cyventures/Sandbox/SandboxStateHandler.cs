using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public abstract class SandboxStateHandler : StateHandler<CyColor, SandboxState>
    {
        public SandboxStateHandler(StateMachineHandler<CyColor, SandboxState> parent, CyRect? bounds) : base(parent, bounds)
        {
        }

        protected Manager<SandboxFont,CyFont> FontManager =>
                (HandleMessage(new FetchMessage<SandboxResource>(SandboxResource.FontManager)) as FetchResult<Manager<SandboxFont, CyFont>>)?.Payload;
    }
}
