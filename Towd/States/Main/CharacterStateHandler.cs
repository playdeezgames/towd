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
    public class CharacterStateHandler : TowdStateHandler
    {
        private LabelControl _money;
        private LabelControl _body;
        private LabelControl _mind;
        private ListBoxControl<string> _equipped;
        public CharacterStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Character", CyColor.White);
            _money = new LabelControl(this, true, CyPoint.Create(0, font.Height), font, "Money: ####G", CyColor.Black);
            _body = new LabelControl(this, true, CyPoint.Create(0, font.Height * 2), font, "Body: ##/##", CyColor.Black);
            _mind = new LabelControl(this, true, CyPoint.Create(0, font.Height * 3), font, "Money: ##/##", CyColor.Black);
            _equipped = new ListBoxControl<string>(this, true, CyRect.Create(0, font.Height * 4, Width, Height - font.Height * 4), font, new ListBoxItem<string>[0], 0, CyColor.Black, CyColor.White, OnListBoxActivate);
        }

        private void OnListBoxActivate(int obj)
        {
            throw new NotImplementedException();
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Yellow:
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
            var creatureInstance = World.GetAvatarCreatureInstance();
            _money.Text = $"Money: {creatureInstance.Money}g";
            _body.Text = $"Body: {creatureInstance.GetCurrentBody()}/{creatureInstance.Body}";
            _mind.Text = $"Mind: {creatureInstance.GetCurrentMind()}/{creatureInstance.Mind}";
            var equippedItems = creatureInstance.GetEquipped();
            List<ListBoxItem<string>> listItems = new List<ListBoxItem<string>>();
            foreach(var equippedItem in equippedItems)
            {
                var item = World.Items[equippedItem];
                listItems.Add(new ListBoxItem<string>
                {
                    Meta = equippedItem,
                    Caption = item.DisplayName
                });
            }
            _equipped.Items = listItems;
        }

        protected override void OnStop()
        {
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
        }
    }
}
