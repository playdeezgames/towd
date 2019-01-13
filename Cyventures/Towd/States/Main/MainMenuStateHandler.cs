using Common;
using MonoGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Towd
{
    public class MainMenuStateHandler : TowdStateHandler
    {
        private ListBoxControl _listBox;
        public MainMenuStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Tombs of Woeful Doom!", CyColor.White);
            _listBox = new ListBoxControl(
                this, 
                true, 
                CyRect.Create(0, font.Height, Width, Height-font.Height), 
                font, 
                new string[] 
                {
                    "New",
                    "Load",
                    "Help",
                    "Options",
                    "About",
                    "Quit",
                }, 
                0, 
                CyColor.Black, 
                CyColor.White, 
                OnListBoxActivate);
        }

        private void OnListBoxActivate(int selected)
        {
            switch(selected)
            {
                case 0://new
                    HandleMessage(NewWorldMessage.Create());
                    SetState(TowdState.Room);
                    break;
                case 1://load
                    if(DoLoadGame())
                    {
                        SetState(TowdState.Room);
                    }
                    break;
                case 2://help
                    SetState(TowdState.Help);
                    break;
                case 3://options
                    SetState(TowdState.Options);
                    break;
                case 4://about
                    SetState(TowdState.About);
                    break;
                case 5://quit
                    SetState(TowdState.ConfirmQuit);
                    break;
            }
        }

        private bool DoLoadGame()
        {
            //TODO: make loading fail gracefully when loading a bad file
            OpenFileDialog dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();
            if(result== DialogResult.OK)
            {
                HandleMessage(LoadWorldMessage.Create(dialog.FileName));
                return true;
            }
            return false;
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
