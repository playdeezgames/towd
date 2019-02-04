using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class AvatarStatus
    {
        public AvatarState State { get; set; }
        public PromptedStatus Prompted { get; set; }
        public ShoppingStatus Shopping { get; set; }
        public DialogStatus Dialog { get; set; }

        public void SetNormal()
        {
            State = AvatarState.Normal;
            Prompted = null;
            Shopping = null;
        }

        public void SetPrompted(int column, int row)
        {
            State = AvatarState.Prompted;
            Prompted = new PromptedStatus
            {
                Column = column,
                Row = row
            };
            Shopping = null;
            Dialog = null;
        }

        public void SetShopping(string shoppeName, ShoppeState shoppeState)
        {
            State = AvatarState.Shopping;
            Prompted = null;
            Shopping = new ShoppingStatus
            {
                ShoppeName = shoppeName,
                State = shoppeState
            };
            Dialog = null;
        }

        public void SetDialog(string dialog, string state)
        {
            State = AvatarState.Dialog;
            Prompted = null;
            Shopping = null;
            Dialog = new DialogStatus
            {
                Dialog = dialog,
                State = state
            };
        }
    }
}
