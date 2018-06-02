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
    public class RoomStateHandler : TowdStateHandler
    {
        public RoomStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
        }

        protected override bool OnCommand(Command command)
        {
            switch (command)
            {
                case Command.Red:
                    SetState(TowdState.MainMenu);
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
        }

        protected override void OnStop()
        {
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
            var room = World.Rooms["home"];
            int cellWidth = World.TileWidth;
            int cellHeight = World.TileHeight;
            for(int column=0;column<room.Width;++column)
            {
                for(int row=0;row<room.Height;++row)
                {
                    var tile = room.Get(column, row);
                    var terrain = World.Terrains[tile.Terrain];
                    var bitmap = BitmapSequenceManager[terrain.Identifier][terrain.Index];
                    bitmap.Draw(pixelWriter, CyPoint.Create(column * cellWidth, row * cellHeight), x => true, clipRect);
                }
            }
        }
    }
}
