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
    public class ExitPlayStateHandler : TowdStateHandler
    {
        private ListBoxControl _listBox;
        public ExitPlayStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Save Game?", CyColor.White);
            _listBox = new ListBoxControl(
                this, 
                true, 
                CyRect.Create(0, font.Height, Width, Height-font.Height), 
                font, 
                new string[] 
                {
                    "Cancel",
                    "No",
                    "Yes"
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
                case 0://cancel
                    SetState(TowdState.Room);
                    break;
                case 1://no
                    SetState(TowdState.MainMenu);
                    break;
                case 2://yes
                    if (DoSaveGame())
                    {
                        SetState(TowdState.MainMenu);
                    }
                    break;
            }
        }

        private bool DoSaveGame()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            var result = dialog.ShowDialog();
            if(result== DialogResult.OK)
            {
                Utility.Save(World, dialog.FileName);
                return true;
            }
            return false;
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
            _listBox.Focus();
            _listBox.Selected = 0;
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
