using Common;
using Engine;
using MonoGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Towd
{
    public class RoomStateHandler : TowdStateHandler
    {
        public RoomStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
        }

        protected override bool OnCommand(Command command)
        {
            var room = World.GetAvatarRoom();
            switch (command)
            {
                case Command.Back:
                    SetState(TowdState.ExitPlay);
                    return true;
                case Command.Red:
                    if (room.HasMessage())
                    {
                        room.AcknowledgeNextMessage();
                    }
                    else if(World.GetAvatarStatus().State!= AvatarState.Normal)
                    {
                        World.GetAvatarStatus().SetNormal();//TODO: might have a "cancel status"
                    }
                    else
                    {
                        SetState(TowdState.Inventory);
                    }
                    return true;
                case Command.Yellow:
                    SetState(TowdState.Character);
                    return true;
                case Command.Green:
                    if (room.HasMessage())
                    {
                        room.AcknowledgeNextMessage();
                    }
                    else if (World.GetPromptTile() != null)
                    {
                        ConfirmPrompt();
                    }
                    return true;
                case Command.Up:
                    DoMove(0, -1);
                    return true;
                case Command.Down:
                    DoMove(0, 1);
                    return true;
                case Command.Right:
                    DoMove(1, 0);
                    return true;
                case Command.Left:
                    DoMove(-1, 0);
                    return true;
                default:
                    return false;
            }
        }

        private void ConfirmPrompt()
        {
            switch (World.GetPromptTile().RoleOverride.Value)
            {
                case RoomTileRole.Teleport:
                    MoveCreature(World.Avatar, World.GetPromptTile().Teleport.Room, World.GetPromptTile().Teleport.Column, World.GetPromptTile().Teleport.Row);
                    World.GetAvatarStatus().SetNormal();
                    break;
                case RoomTileRole.Search:
                    World.ActivateTrigger(World.GetPromptTile().Search.Trigger);
                    World.GetAvatarStatus().SetNormal();
                    break;
                case RoomTileRole.Sign:
                    World.GetAvatarStatus().SetNormal();
                    break;
                case RoomTileRole.Shoppe:
                    World.AvatarStatus.SetShopping(World.GetPromptTile().Shoppe.ShoppeName, ShoppeState.Initial);
                    SetState(TowdState.Shopping);
                    break;
            }
        }


        private void MoveCreature(string creatureInstance, string room, int column, int row)
        {
            var instance = World.CreatureInstances[creatureInstance];
            var oldTile = World.GetRoom(instance.Room).Get(instance.Column, instance.Row);
            var newTile = World.GetRoom(room).Get(column, row);
            if(newTile.CreatureInstance!=null)
            {
                var otherCreature = World.CreatureInstances[newTile.CreatureInstance];
                if(creatureInstance==World.Avatar)
                {
                    if(!string.IsNullOrEmpty(otherCreature.Dialog))
                    {
                        World.AvatarStatus.SetDialog(otherCreature.Dialog);
                        SetState(TowdState.Dialog);
                    }
                    else
                    {
                        //its a fight!
                    }
                }
                return;
            }
            oldTile.CreatureInstance = null;
            newTile.CreatureInstance = creatureInstance;
            instance.Room = room;
            instance.Column = column;
            instance.Row = row;
        }

        private void DoMove(int deltaX, int deltaY)
        {
            if (World.CanAvatarMove())
            {
                var avatarCreature = World.GetAvatarCreatureInstance();
                var avatarTile = World.GetAvatarRoom().Get(avatarCreature.Column, avatarCreature.Row);
                var nextX = deltaX + avatarCreature.Column;
                var nextY = deltaY + avatarCreature.Row;
                switch (World.GetAvatarRoom().GetTileRole(nextX, nextY, World.Terrains))
                {
                    case RoomTileRole.Open:
                        MoveCreature(World.Avatar, avatarCreature.Room, nextX, nextY);
                        break;
                    case RoomTileRole.StartDialog:
                        World.AvatarStatus.SetDialog(World.GetAvatarRoom().Get(nextX, nextY).StartDialog.Dialog);
                        SetState(TowdState.Dialog);
                        break;
                    case RoomTileRole.Teleport:
                    case RoomTileRole.Sign:
                    case RoomTileRole.Search:
                    case RoomTileRole.Shoppe:
                        World.GetAvatarStatus().SetPrompted(nextX, nextY);
                        break;
                }
            }
        }

        protected override IResult OnMessage(IMessage message)
        {
            return null;
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
            pixelWriter.Clear(CyColor.LightGray, clipRect);
            var avatarCreature = World.GetAvatarCreatureInstance();
            var room = World.GetAvatarRoom();
            int cellWidth = World.TileWidth;
            int cellHeight = World.TileHeight;
            int offsetX = Width / 2 - avatarCreature.Column * cellWidth - cellWidth / 2;
            int offsetY = Height / 2 - avatarCreature.Row * cellHeight - cellHeight / 2;


            for (int column = avatarCreature.Column - 10; column <= avatarCreature.Column + 10; ++column)
            {
                if (column < 0 || column >= room.Width)
                {
                    continue;
                }
                int cellX = column * cellWidth + offsetX;
                for (int row = avatarCreature.Row - 6; row <= avatarCreature.Row + 6; ++row)
                {
                    if (row < 0 || row >= room.Height)
                    {
                        continue;
                    }
                    int cellY = row * cellHeight + offsetY;
                    var tile = room.Get(column, row);
                    var terrain = World.Terrains[tile.Terrain];
                    var bitmap = BitmapSequenceManager[terrain.ResourceIdentifier][terrain.ResourceIndex];
                    bitmap.Draw(pixelWriter, CyPoint.Create(cellX, cellY), x => true, clipRect);
                    if (!string.IsNullOrEmpty(tile.CreatureInstance))
                    {
                        var creatureInstance = World.CreatureInstances[tile.CreatureInstance];
                        var creature = World.Creatures[creatureInstance.Creature];
                        bitmap = BitmapSequenceManager[creature.ResourceIdentifier][creature.ResourceIndex];
                        bitmap.Draw(pixelWriter, CyPoint.Create(cellX, cellY), x => x != CyColor.White, clipRect);
                    }
                }
            }
            var font = FontManager[TowdFont.Large];
            DrawOutlinedText(pixelWriter,font,CyColor.Black, CyColor.LightGray, 1, 1, room.Caption, clipRect);
            if (room.HasMessage())
            {
                var message = room.GetNextMessage();
                DrawOutlinedText(pixelWriter, font, CyColor.Black, CyColor.White, 0, Height - font.Height, message, clipRect);
            }
            else if (World.GetPromptTile() != null)
            {
                switch (World.GetPromptTile().RoleOverride.Value)
                {
                    case RoomTileRole.Shoppe:
                        DrawOutlinedText(pixelWriter, font, CyColor.Black, CyColor.White, 1, Height - font.Height, World.GetPromptTile().Shoppe.Prompt, clipRect);
                        break;
                    case RoomTileRole.Search:
                        DrawOutlinedText(pixelWriter, font, CyColor.Black, CyColor.White, 1, Height - font.Height, World.GetPromptTile().Search.Prompt, clipRect);
                        break;
                    case RoomTileRole.Teleport:
                        DrawOutlinedText(pixelWriter, font, CyColor.Black, CyColor.White, 1, Height - font.Height, World.GetPromptTile().Teleport.Prompt, clipRect);
                        break;
                    case RoomTileRole.Sign:
                        DrawOutlinedText(pixelWriter, font, CyColor.Black, CyColor.White, 1, Height - font.Height, World.GetPromptTile().Sign.Message, clipRect);
                        break;
                }
            }
        }

        private void DrawOutlinedText(IPixelWriter<CyColor> pixelWriter, CyFont font, CyColor color, CyColor outline, int x, int y, string text, CyRect? clipRect)
        {
            font.Draw(pixelWriter, outline, x + 1, y - 1, text, clipRect);
            font.Draw(pixelWriter, outline, x + 1, y, text, clipRect);
            font.Draw(pixelWriter, outline, x + 1, y + 1, text, clipRect);
            font.Draw(pixelWriter, outline, x + 0, y - 1, text, clipRect);
            font.Draw(pixelWriter, outline, x + 0, y + 1, text, clipRect);
            font.Draw(pixelWriter, outline, x - 1, y - 1, text, clipRect);
            font.Draw(pixelWriter, outline, x - 1, y, text, clipRect);
            font.Draw(pixelWriter, outline, x - 1, y + 1, text, clipRect);
            font.Draw(pixelWriter, color, x, y, text, clipRect);
        }
    }
}
