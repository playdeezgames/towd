using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towd
{
    public class TowdStateMachineHandler : StateMachineHandler<CyColor, TowdState>
    {
        public TowdStateMachineHandler(MessageHandlerBase<CyColor> parent) : base(parent)
        {
            this[TowdState.MainMenu] = new MainMenuStateHandler(this, null);
            this[TowdState.ConfirmQuit] = new ConfirmQuitStateHandler(this, null);
            this[TowdState.About] = new AboutStateHandler(this, null);
            this[TowdState.Options] = new OptionsStateHandler(this, null);
            this[TowdState.Help] = new HelpStateHandler(this, null);
            this[TowdState.Room] = new RoomStateHandler(this, null);
            this[TowdState.ExitPlay] = new ExitPlayStateHandler(this, null);
            this[TowdState.EditMenu] = new EditMenuStateHandler(this, null);
            Current = TowdState.MainMenu;
        }
    }
}
