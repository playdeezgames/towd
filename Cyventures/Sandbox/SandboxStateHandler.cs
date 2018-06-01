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

        protected Manager<SandboxFont, CyFont> FontManager =>
                (HandleMessage(FetchMessage<SandboxResource>.Create(SandboxResource.FontManager)) as FetchResult<Manager<SandboxFont, CyFont>>)?.Payload;

        protected Manager<SandboxBitmap, Bitmap<CyColor>> BitmapManager =>
                (HandleMessage(FetchMessage<SandboxResource>.Create(SandboxResource.BitmapManager)) as FetchResult<Manager<SandboxBitmap, Bitmap<CyColor>>>)?.Payload;
    }
}
