using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class ControlBase : MessageHandlerBase<CyColor>
    {
        private static ControlBase _focus = null;

        public ControlBase(IMessageHandler<CyColor> parent, bool enabled, CyRect bounds) 
            : base(parent, enabled, bounds)
        {
        }

        public bool HasFocus => _focus == this;
        public void Focus()
        {
            if(!HasFocus)
            {
                if(_focus!=null)
                {
                    _focus.Blur();
                }
                _focus = this;
                OnFocus();
            }
        }
        public void Blur()
        {
            if(HasFocus)
            {
                _focus = null;
                OnBlur();
            }
        }
        protected abstract void OnFocus();
        protected abstract void OnBlur();
        protected override bool OnCommand(Command command)
        {
            if(HasFocus)
            {
                return OnFocusCommand(command);
            }
            else
            {
                return false;
            }
        }

        protected abstract bool OnFocusCommand(Command command);
    }
}
