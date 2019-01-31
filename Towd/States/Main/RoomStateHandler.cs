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
        private RoomTile _promptTile = null;
        public RoomStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Red:
                    if(_promptTile!=null)
                    {
                        _promptTile = null;
                    }
                    else
                    {
                        SetState(TowdState.ExitPlay);
                    }
                    return true;
                case Command.Green:
                    if(_promptTile!=null)
                    {
                        ConfirmPrompt();
                    }
                    return true;
                case Command.Up:
                    _promptTile = null;
                    DoMove(0, -1);
                    return true;
                case Command.Down:
                    _promptTile = null;
                    DoMove(0, 1);
                    return true;
                case Command.Right:
                    _promptTile = null;
                    DoMove(1, 0);
                    return true;
                case Command.Left:
                    _promptTile = null;
                    DoMove(-1, 0);
                    return true;
                default:
                    return false;
            }
        }

        private void ConfirmPrompt()
        {
            switch(_promptTile.RoleOverride.Value)
            {
                case RoomTileRole.Teleport:
                    MoveCreature(World.Avatar, _promptTile.Teleport.Room, _promptTile.Teleport.Column, _promptTile.Teleport.Row);
                    break;
            }
            _promptTile = null;
        }

        private void MoveCreature(string creatureInstance, string room, int column, int row)
        {
            var instance = World.CreatureInstances[creatureInstance];
            var oldTile = World.GetRoom(instance.Room).Get(instance.Column, instance.Row);
            var newTile = World.GetRoom(room).Get(column, row);
            oldTile.CreatureInstance = null;
            newTile.CreatureInstance = creatureInstance;
            instance.Room = room;
            instance.Column = column;
            instance.Row = row;
        }

        private void DoMove(int deltaX, int deltaY)
        {
            var avatarCreature = World.GetAvatarCreatureInstance();
            var avatarTile = World.GetAvatarRoom().Get(avatarCreature.Column, avatarCreature.Row);
            var nextX = deltaX + avatarCreature.Column;
            var nextY = deltaY + avatarCreature.Row;
            switch(World.GetAvatarRoom().GetTileRole(nextX, nextY, World.Terrains))
            {
                case RoomTileRole.Open:
                    MoveCreature(World.Avatar, avatarCreature.Room, nextX, nextY);
                    break;
                case RoomTileRole.Teleport:
                    _promptTile = World.GetAvatarRoom().Get(nextX, nextY);
                    break;
            }
        }

        protected override IResult OnMessage(IMessage message)
        {
            return null;
        }

        protected override void OnStart()
        {
            _promptTile = null;
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
            int offsetX = Width/2-avatarCreature.Column*cellWidth-cellWidth/2;
            int offsetY = Height/2-avatarCreature.Row*cellHeight-cellHeight/2;
            for(int column=0;column<room.Width;++column)
            {
                int cellX = column * cellWidth + offsetX;
                for (int row=0;row<room.Height;++row)
                {
                    int cellY = row * cellHeight + offsetY;
                    var tile = room.Get(column, row);
                    var terrain = World.Terrains[tile.Terrain];
                    var bitmap = BitmapSequenceManager[terrain.ResourceIdentifier][terrain.ResourceIndex];
                    bitmap.Draw(pixelWriter, CyPoint.Create(cellX, cellY), x => true, clipRect);
                    if(!string.IsNullOrEmpty(tile.CreatureInstance))
                    {
                        var creatureInstance = World.CreatureInstances[tile.CreatureInstance];
                        var creature = World.Creatures[creatureInstance.Creature];
                        bitmap = BitmapSequenceManager[creature.ResourceIdentifier][creature.ResourceIndex];
                        bitmap.Draw(pixelWriter, CyPoint.Create(cellX, cellY), x => x!= CyColor.White, clipRect);
                    }
                }
            }
            var font = FontManager[TowdFont.Large];
            font.Draw(pixelWriter, CyColor.Black, 0, 0, room.Caption, clipRect);
            if(_promptTile!=null)
            {
                switch(_promptTile.RoleOverride.Value)
                {
                    case RoomTileRole.Teleport:
                        font.Draw(pixelWriter, CyColor.White, 0, Height - font.Height + 1, _promptTile.Teleport.Prompt, clipRect);
                        font.Draw(pixelWriter, CyColor.White, 1, Height - font.Height, _promptTile.Teleport.Prompt, clipRect);
                        font.Draw(pixelWriter, CyColor.Black, 0, Height - font.Height, _promptTile.Teleport.Prompt, clipRect);
                        break;
                }
            }
        }
    }
}
