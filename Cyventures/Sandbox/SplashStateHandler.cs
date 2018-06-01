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
        private ListBoxControl _listBox;
        public SplashStateHandler(StateMachineHandler<CyColor, SandboxState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[SandboxFont.Largest];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Main Menu:", CyColor.White);
            _listBox = new ListBoxControl(this, true, CyRect.Create(0, font.Height, Width, Height-font.Height), font, new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen" }, 0, CyColor.Black, CyColor.White, OnListBoxActivate);
        }

        private void OnListBoxActivate(int selected)
        {
            if(selected==12)
            {
                HandleMessage(QuitMessage.Create());
            }
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
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
            _listBox.Focus();
        }

        protected override void OnStop()
        {
            _listBox.Blur();
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
        }
    }
}
