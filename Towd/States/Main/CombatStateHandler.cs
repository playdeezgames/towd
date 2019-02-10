using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Towd
{
    internal class CombatStateHandler : TowdStateHandler
    {
        private enum CombatState
        {
            Initial,//
            AvatarTurn,//
            EnemyTurn,//
            AvatarTurnResult,//
            AvatarItemSelect,
            AvatarDeath,//
            EnemyDeath//
        }

        private CombatState _state;
        private ListBoxControl<string> _listBox;
        private LabelControl[] _promptLabels;
        private LabelControl _headerLabel;
        public CombatStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
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
            switch(_listBox.Items.ToList()[selected].Meta)
            {
                case "EnemyTurn":
                    _state = CombatState.EnemyTurn;
                    UpdateCombat(string.Empty);
                    break;
                case "AvatarTurn":
                    _state = CombatState.AvatarTurn;
                    UpdateCombat(string.Empty);
                    break;
                case "AvatarAttack":
                    _state = CombatState.AvatarTurnResult;
                    UpdateCombat(string.Empty);
                    break;
                case "MainMenu":
                    SetState(TowdState.MainMenu);
                    break;
                case "AvatarItemSelect":
                    break;
                case "AvatarDeath":
                    _state = CombatState.AvatarDeath;
                    UpdateCombat(string.Empty);
                    break;
                case "EnemyDeath":
                    _state = CombatState.EnemyDeath;
                    UpdateCombat(string.Empty);
                    break;
                case "CombatComplete":
                    World.AvatarStatus.SetNormal();
                    SetState(TowdState.Room);
                    break;
                case "AvatarRun":
                    World.AvatarStatus.SetNormal();
                    SetState(TowdState.Room);
                    break;
            }
        }

        private void UpdateAvatarTurnResult(string lastSelected)
        {
            var avatarInstance = World.GetAvatarCreatureInstance();
            var weapons = avatarInstance.GetEquipped().Select(x => World.Items[x]).Where(x => x.ItemType == Engine.ItemType.Weapon).ToList();
            List<string> output = new List<string>();
            int attack = 0;
            if(weapons.Any())
            {
                foreach(var weapon in weapons)
                {
                    output.Add($"Attacking w/ {weapon.DisplayName}!");
                    attack += World.Roll(weapon.Attack);
                }
            }
            else
            {
                output.Add("You attack unarmed!");
                attack+=World.Roll(avatarInstance.UnarmedAttack);
            }
            var enemyInstance = World.CreatureInstances[World.GetAvatarStatus().Combat.EnemyInstance];
            if (attack>0)
            {
                output.Add($"A hit for {attack}");
                int defend = World.Roll(enemyInstance.BaseDefense);
                if (defend > 0)
                {
                    output.Add($"It defends {defend}!");
                    attack = Math.Max(attack - defend, 0);
                }
            }
            else
            {
                output.Add("A miss!");
            }
            ShowOutput(output);
            enemyInstance.Wounds += attack;
            List<ListBoxItem<string>> listBoxItems = new List<ListBoxItem<string>>();
            if(enemyInstance.IsDead())
            {
                listBoxItems.Add(new ListBoxItem<string>
                {
                    Meta = "EnemyDeath",
                    Caption = "Ok"
                });
            }
            else
            {
                listBoxItems.Add(new ListBoxItem<string>
                {
                    Meta = "EnemyTurn",
                    Caption = "Ok"
                });
            }
            _listBox.Items = listBoxItems;
            _listBox.Selected = 0;
        }

        private void ShowOutput(List<string> output)
        {
            int index = 0;
            foreach(var label in _promptLabels)
            {
                if (index < output.Count())
                {
                    label.Text = output[index++];
                }
                else
                {
                    label.Text = string.Empty;
                }
            }
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
            _state = CombatState.Initial;
            UpdateCombat(string.Empty);
            _listBox.Focus();
        }

        private void UpdateCombat(string lastSelected)
        {
            switch (_state)
            {
                case CombatState.Initial:
                    UpdateInitial(lastSelected);
                    break;
                case CombatState.AvatarTurn:
                    UpdateAvatarTurn(lastSelected);
                    break;
                case CombatState.AvatarTurnResult:
                    UpdateAvatarTurnResult(lastSelected);
                    break;
                case CombatState.AvatarItemSelect:
                    UpdateAvatarItemSelect(lastSelected);
                    break;
                case CombatState.EnemyTurn:
                    UpdateEnemyTurn(lastSelected);
                    break;
                case CombatState.AvatarDeath:
                    UpdateAvatarDeath(lastSelected);
                    break;
                case CombatState.EnemyDeath:
                    UpdateEnemyDeath(lastSelected);
                    break;
            }
        }

        private void UpdateAvatarItemSelect(string lastSelected)
        {
        }

        private void UpdateEnemyDeath(string lastSelected)
        {
            var enemyInstance = World.CreatureInstances[World.GetAvatarStatus().Combat.EnemyInstance];
            List<string> output = new List<string>();
            output.Add("It is dead!");
            //TODO: loot
            //TODO: xp
            //TODO: room event
            World.TriggerDeathEvent(enemyInstance);
            //TODO: quest tracking

            //remove enemy instance!
            var roomTile = World.GetRoom(enemyInstance.Room).Get(enemyInstance.Column, enemyInstance.Row);
            roomTile.CreatureInstance = null;
            World.CreatureInstances.Remove(World.GetAvatarStatus().Combat.EnemyInstance);

            ShowOutput(output);
            List<ListBoxItem<string>> listBoxItems = new List<ListBoxItem<string>>();
            listBoxItems.Add(new ListBoxItem<string>
            {
                Meta = "CombatComplete",
                Caption = "Victory!"
            });
            _listBox.Items = listBoxItems;
            _listBox.Selected = 0;
        }

        private void UpdateAvatarDeath(string lastSelected)
        {
            List<string> output = new List<string>();
            output.Add("You are dead!");
            ShowOutput(output);
            List<ListBoxItem<string>> listBoxItems = new List<ListBoxItem<string>>();
            listBoxItems.Add(new ListBoxItem<string>
            {
                Meta = "MainMenu",
                Caption = "Main menu"
            });
            _listBox.Items = listBoxItems;
            _listBox.Selected = 0;
        }

        private void UpdateEnemyTurn(string lastSelected)
        {
            var enemyInstance = World.CreatureInstances[World.GetAvatarStatus().Combat.EnemyInstance];
            List<string> output = new List<string>();
            output.Add("It attacks!");
            int attack = World.Roll(enemyInstance.UnarmedAttack);
            var avatarInstance = World.GetAvatarCreatureInstance();
            if (attack>0)
            {
                output.Add($"A hit for {attack}");
                int defend = World.Roll(avatarInstance.BaseDefense);//TODO: fix when armor more fully implemented!
                output.Add($"You defend {defend}");
                attack = Math.Max(attack - defend, 0);
            }
            else
            {
                output.Add("It misses!");
            }
            avatarInstance.Wounds += attack;
            if(attack>0)
            {
                output.Add($"Health {avatarInstance.GetCurrentBody()}/{avatarInstance.Body}");
            }
            ShowOutput(output);
            List<ListBoxItem<string>> listBoxItems = new List<ListBoxItem<string>>();
            if(avatarInstance.IsDead())
            {
                listBoxItems.Add(new ListBoxItem<string>
                {
                    Meta= "AvatarDeath",
                    Caption="Ok"
                });
            }
            else
            {
                listBoxItems.Add(new ListBoxItem<string>
                {
                    Meta = "AvatarTurn",
                    Caption = "Ok"
                });
            }
            _listBox.Items = listBoxItems;
            _listBox.Selected = 0;
        }

        private void UpdateAvatarTurn(string lastSelected)
        {
            _promptLabels[0].Text = "It is your turn!";
            _promptLabels[1].Text = "What will you do?";
            _promptLabels[2].Text = "";
            _promptLabels[3].Text = "";
            List<ListBoxItem<string>> listBoxItems = new List<ListBoxItem<string>>();
            listBoxItems.Add(new ListBoxItem<string>
            {
                Meta = "AvatarAttack",
                Caption = "Attack!"
            });
            listBoxItems.Add(new ListBoxItem<string>
            {
                Meta = "AvatarItemSelect",
                Caption = "Use..."
            });
            listBoxItems.Add(new ListBoxItem<string>
            {
                Meta = "AvatarRun",
                Caption = "Run!"
            });
            _listBox.Items = listBoxItems;
            _listBox.Selected = 0;
        }

        private void UpdateInitial(string lastSelected)
        {
            var enemyInstance = World.CreatureInstances[World.GetAvatarStatus().Combat.EnemyInstance];
            _headerLabel.Text = $"Fighting {enemyInstance.Name}";
            _promptLabels[0].Text = "You are in combat!";
            _promptLabels[1].Text = "You will attack first.";
            _promptLabels[2].Text = "";
            _promptLabels[3].Text = "";
            List<ListBoxItem<string>> listBoxItems = new List<ListBoxItem<string>>();
            listBoxItems.Add(new ListBoxItem<string>
            {
                Meta = "AvatarTurn",
                Caption = "Ok"
            });
            _listBox.Items = listBoxItems;
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