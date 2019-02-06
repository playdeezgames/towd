using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Towd
{
    internal class DialogStateHandler : TowdStateHandler
    {
        private ListBoxControl<string> _listBox;
        private LabelControl _promptLabel;
        public DialogStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new FilledBoxControl(this, true, CyRect.Create(0, Height - font.Height, Width, font.Height), CyColor.LightGray);
            new LabelControl(this, true, CyPoint.Create(0, 0), font, "Dialog", CyColor.White);
            _promptLabel = new LabelControl(this, true, CyPoint.Create(0, font.Height), font, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CyColor.DarkGray);
            _listBox = new ListBoxControl<string>(
                this,
                true,
                CyRect.Create(0, font.Height * 2, Width, Height - font.Height * 2),
                font,
                new ListBoxItem<string>[]
                {
                },
                0,
                CyColor.Black,
                CyColor.White,
                OnListBoxActivate);
        }

        private void OnListBoxActivate(int selected)
        {
            var dialogState = World.GetAvatarRoom().DialogStates[World.AvatarStatus.Dialog.Dialog];
            var dialogNode = dialogState.Nodes[dialogState.CurrentState];
            var dialogChoice = dialogNode.Choices[_listBox.Items.ToList()[selected].Meta];
            foreach(var dialogEvent in dialogChoice.Events.OrderBy(x=>x.Order))
            {
                switch(dialogEvent.EventType)
                {
                    case Engine.DialogEventType.ExitDialog:
                        World.AvatarStatus.SetNormal();
                        SetState(TowdState.Room);
                        break;
                    case Engine.DialogEventType.EnterShoppe:
                        World.AvatarStatus.SetShopping(dialogEvent.Shoppe, Engine.ShoppeState.Initial);
                        SetState(TowdState.Shopping);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Red:
                    World.AvatarStatus.SetNormal();
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
            UpdateDialog();
            _listBox.Focus();
        }

        private void UpdateDialog()
        {
            var dialogState = World.GetAvatarRoom().DialogStates[World.AvatarStatus.Dialog.Dialog];
            var dialogNode = dialogState.Nodes[dialogState.CurrentState];
            _promptLabel.Text = dialogNode.Prompt;
            _listBox.Items = dialogNode
                .Choices
                .OrderBy(x => x.Value.Order)
                .Select(x => new ListBoxItem<string>
                {
                    Meta = x.Key,
                    Caption= x.Value.Option
                });
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