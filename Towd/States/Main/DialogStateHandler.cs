using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Engine;

namespace Towd
{
    internal class DialogStateHandler : TowdStateHandler
    {
        private ListBoxControl<string> _listBox;
        private LabelControl[] _promptLabels;
        private LabelControl _headerLabel;
        public DialogStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
            var font = FontManager[TowdFont.Large];
            new FilledBoxControl(this, true, CyRect.Create(0, 0, Width, font.Height), CyColor.DarkGray);
            new FilledBoxControl(this, true, CyRect.Create(0, Height - font.Height, Width, font.Height), CyColor.LightGray);
            _headerLabel = new LabelControl(this, true, CyPoint.Create(0, 0), font, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CyColor.White);
            _promptLabels = new LabelControl[4];
            _promptLabels[0] = new LabelControl(this, true, CyPoint.Create(0, font.Height), font, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CyColor.DarkGray);
            _promptLabels[1] = new LabelControl(this, true, CyPoint.Create(0, font.Height * 2), font, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CyColor.DarkGray);
            _promptLabels[2] = new LabelControl(this, true, CyPoint.Create(0, font.Height * 3), font, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CyColor.DarkGray);
            _promptLabels[3] = new LabelControl(this, true, CyPoint.Create(0, font.Height * 4), font, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CyColor.DarkGray);
            _listBox = new ListBoxControl<string>(
                this,
                true,
                CyRect.Create(0, font.Height * 5, Width, Height - font.Height * 5),
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
            foreach (var dialogEvent in dialogChoice.GetEvents().OrderBy(x => x.Order))
            {
                switch (dialogEvent.EventType)
                {
                    case DialogEventType.ExitDialog:
                        World.AvatarStatus.SetNormal();
                        SetState(TowdState.Room);
                        break;
                    case DialogEventType.EnterShoppe:
                        World.AvatarStatus.SetShopping(dialogEvent.Shoppe, Engine.ShoppeState.Initial);
                        SetState(TowdState.Shopping);
                        break;
                    case DialogEventType.SetDialogState:
                        dialogState.CurrentState = dialogEvent.State;
                        UpdateDialog();
                        break;
                    case DialogEventType.SetRoomFlag:
                        World.GetAvatarRoom().SetRoomFlag(dialogEvent.Flag);
                        break;
                    case DialogEventType.MakeTeleport:
                        World.GetAvatarRoom().MakeTeleport(dialogEvent.Column, dialogEvent.Row, dialogEvent.Prompt, dialogEvent.DestinationRoom, dialogEvent.DestinationColumn, dialogEvent.DestinationRow);
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
            _headerLabel.Text = dialogNode.Caption;
            _promptLabels[0].Text = dialogNode.GetPrompt(1);
            _promptLabels[1].Text = dialogNode.GetPrompt(2);
            _promptLabels[2].Text = dialogNode.GetPrompt(3);
            _promptLabels[3].Text = dialogNode.GetPrompt(4);
            _listBox.Items = dialogNode
                .Choices
                .Where(x => ShouldShowChoice(x.Value))
                .OrderBy(x => x.Value.Order)
                .Select(x => new ListBoxItem<string>
                {
                    Meta = x.Key,
                    Caption = x.Value.OptionText
                });
            _listBox.Selected = 0;
        }

        private bool ShouldShowChoice(DialogChoice choice)
        {
            var room = World.GetAvatarRoom();
            foreach (var condition in choice.GetConditions())
            {
                switch (condition.ConditionType)
                {
                    case DialogConditionType.WhenFlagClear:
                        if (room.GetFlag(condition.FlagName))
                        {
                            return false;
                        }
                        break;
                    case DialogConditionType.WhenFlagSet:
                        if (!room.GetFlag(condition.FlagName))
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
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