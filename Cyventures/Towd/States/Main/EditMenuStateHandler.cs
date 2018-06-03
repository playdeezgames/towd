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
    public class EditMenuStateHandler : TowdStateHandler
    {
        private ListBoxControl _listBox;
        public EditMenuStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Edit Menu:", CyColor.White);
            _listBox = new ListBoxControl(
                this, 
                true, 
                CyRect.Create(0, font.Height, Width, Height-font.Height), 
                font, 
                new string[] 
                {
                    "Play",
                    "Edit",
                    "Save",
                    "Load",
                    "Main Menu"
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
                case 0://play
                    SetState(TowdState.Room);
                    break;
                case 1://edit
                    SetState(TowdState.Editor);
                    break;
                case 2://save
                    DoSaveGame();
                    break;
                case 3://load
                    DoLoadGame();
                    break;
                case 4://main menu
                    SetState(TowdState.MainMenu);
                    break;
            }
        }

        private void DoLoadGame()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                HandleMessage(LoadWorldMessage.Create(dialog.FileName));
            }
        }

        private void DoSaveGame()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            var result = dialog.ShowDialog();
            if(result== DialogResult.OK)
            {
                Utility.Save(World, dialog.FileName);
            }
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
