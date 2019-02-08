using Common;
using MonoGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Towd
{
    public class AboutStateHandler : TowdStateHandler
    {
        public AboutStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "About TOWD", CyColor.White);
            new LabelControl(this, true, CyPoint.Create(0, font.Height), font, "By TheGrumpyGameDev", CyColor.Black);
            new LabelControl(this, true, CyPoint.Create(0, font.Height * 2), font, "With \"help\" from:", CyColor.Black);
            new LabelControl(this, true, CyPoint.Create(0, font.Height * 3), font, " - domsson", CyColor.Black);
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Red:
                    SetState(TowdState.MainMenu);
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
