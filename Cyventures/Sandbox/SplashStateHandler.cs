using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class SplashStateHandler : SandboxStateHandler
    {
        public SplashStateHandler(StateMachineHandler<CyColor, SandboxState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var smallBounds = FontManager[SandboxFont.Small].GetBounds("Splash!");
            var mediumBounds = FontManager[SandboxFont.Medium].GetBounds("Splash!").OffsetBy(0,smallBounds.Bottom);
            var largeBounds = FontManager[SandboxFont.Large].GetBounds("Splash!").OffsetBy(0,mediumBounds.Bottom);

            new FilledBoxControl(this, true, smallBounds, CyColor.LightGray);
            new FilledBoxControl(this, true, mediumBounds, CyColor.DarkGray);
            new FilledBoxControl(this, true, largeBounds, CyColor.Black);

            new LabelControl(this, true, smallBounds, FontManager[SandboxFont.Small], "Splash!", CyColor.Black);
            new LabelControl(this, true, mediumBounds, FontManager[SandboxFont.Medium], "Splash!", CyColor.White);
            new LabelControl(this, true, largeBounds, FontManager[SandboxFont.Large], "Splash!", CyColor.LightGray);
            new LabelControl(this, true, CyPoint.Create(SandboxConstants.ScreenWidth/2, SandboxConstants.ScreenHeight/2), FontManager[SandboxFont.Large], "Test", CyColor.Black);
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Back:
                    HandleMessage(new QuitMessage());
                    return true;
                default:
                    return false;
            }
        }

        protected override IResult OnMessage(IMessage message)
        {
            return null;
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
            
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
        }
    }
}
