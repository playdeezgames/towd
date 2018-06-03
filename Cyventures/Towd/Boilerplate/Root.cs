using Common;
using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Towd
{
    public class Root : MessageHandlerBase<CyColor>
    {
        private static readonly Dictionary<TowdFont, string> _fontMap = new Dictionary<TowdFont, string>()
        {
            [TowdFont.Largest] = "Towd.Resources.Fonts.CyFont8x8.json",
            [TowdFont.Large] = "Towd.Resources.Fonts.CyFont5x7.json",
            [TowdFont.Medium] = "Towd.Resources.Fonts.CyFont4x6.json",
            [TowdFont.Small] = "Towd.Resources.Fonts.CyFont3x5.json",
        };
        private static readonly Dictionary<string, string> _bitmapSequenceMap = new Dictionary<string, string>()
        {
            [TowdBitmapSequence.DungeonTiles] = "Towd.Resources.BitmapSequences.DungeonTiles.json",
            [TowdBitmapSequence.DungeonCreatures] = "Towd.Resources.BitmapSequences.DungeonCreatures.json"
        };

        private Manager<TowdFont, CyFont> _fontManager;
        private Manager<string, Sequence<Bitmap<CyColor>>> _bitmapSequenceManager;
        private World _world;
        private EditorState _editorState= new EditorState();


        public Root(IMessageHandler<CyColor> parent)
            :base(parent, true, null)
        {
            _fontManager = new Manager<TowdFont, CyFont>
                (k => Utility.LoadEmbedded<CyFont>(Assembly.GetExecutingAssembly(),_fontMap[k]));

            _bitmapSequenceManager = new Manager<string, Sequence<Bitmap<CyColor>>>
                (k => Utility.LoadEmbedded<Sequence<Bitmap<CyColor>>>(Assembly.GetExecutingAssembly(), _bitmapSequenceMap[k]));

            _world = Utility.LoadEmbedded<World>(Assembly.GetExecutingAssembly(), "Towd.Resources.World.json");

            new TowdStateMachineHandler(this);
        }
        public static IMessageHandler<CyColor> Create(IMessageHandler<CyColor> parent)
        {
            return new Root(parent);
        }

        protected override bool OnCommand(Command command)
        {
            return false;
        }

        protected override IResult OnMessage(IMessage message)
        {
            if (message.MessageId == FetchMessage.Id)
            {
                switch ((message as FetchMessage<TowdResource>)?.Resource ?? TowdResource.None)
                {
                    case TowdResource.EditorState:
                        return FetchResult< EditorState >.Create(_editorState);

                    case TowdResource.World:
                        return FetchResult<World>.Create(_world);

                    case TowdResource.BitmapSequenceManager:
                        return FetchResult<Manager<string, Sequence<Bitmap<CyColor>>>>.Create(_bitmapSequenceManager);

                    case TowdResource.FontManager:
                        return FetchResult<Manager<TowdFont, CyFont>>.Create(_fontManager);

                    default:
                        return AckResult<CyColor>.Create(message, this);
                }
            }
            else if (message.MessageId == NewWorldMessage.Id)
            {
                _world = Utility.LoadEmbedded<World>(Assembly.GetExecutingAssembly(), "Towd.Resources.World.json");
            }
            else if (message.MessageId == LoadWorldMessage.Id)
            {
                _world = Utility.Load<World>((message as LoadWorldMessage).FileName);
            }
            return null;
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
        }
    }
}
