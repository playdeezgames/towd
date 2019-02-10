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
        public CombatStatus Combat { get; set; }

        public void SetNormal()
        {
            State = AvatarState.Normal;
            Prompted = null;
            Shopping = null;
            Dialog = null;
            Combat = null;
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
            Combat = null;
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
            Combat = null;
        }

        public void SetDialog(string dialog)
        {
            State = AvatarState.Dialog;
            Prompted = null;
            Shopping = null;
            Dialog = new DialogStatus
            {
                Dialog = dialog
            };
            Combat = null;
        }

        public void SetCombat(string enemyInstance)
        {
            State = AvatarState.Combat;
            Prompted = null;
            Shopping = null;
            Dialog = null;
            Combat = new CombatStatus
            {
                EnemyInstance = enemyInstance
            };
        }
    }
}
