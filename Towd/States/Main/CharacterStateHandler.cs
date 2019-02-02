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
    //TODO: have some content
    public class CharacterStateHandler : TowdStateHandler
    {
        private LabelControl _money;
        public CharacterStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Character", CyColor.White);
            _money = new LabelControl(this, true, CyPoint.Create(0, font.Height), font, "Money: ####", CyColor.Black);
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Red:
                    SetState(TowdState.Room);
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
            _money.Text = $"Money: {World.GetAvatarCreatureInstance().Money}";
        }

        protected override void OnStop()
        {
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
        }
    }
}
