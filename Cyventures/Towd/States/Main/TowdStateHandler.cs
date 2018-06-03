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
    public abstract class TowdStateHandler : StateHandler<CyColor, TowdState>
    {
        public TowdStateHandler(StateMachineHandler<CyColor, TowdState> parent, CyRect? bounds) : base(parent, bounds)
        {
        }

        protected void SetState(TowdState state)
        {
            HandleMessage(SetStateMessage<TowdState>.Create(state));
        }

        protected Manager<TowdFont, CyFont> FontManager =>
                (HandleMessage(FetchMessage<TowdResource>.Create(TowdResource.FontManager)) as FetchResult<Manager<TowdFont, CyFont>>)?.Payload;

        protected World World =>
                (HandleMessage(FetchMessage<TowdResource>.Create(TowdResource.World)) as FetchResult<World>)?.Payload;

        protected EditorState EditorState =>
                (HandleMessage(FetchMessage<TowdResource>.Create(TowdResource.EditorState)) as FetchResult<EditorState>)?.Payload;

        protected Manager<string, Sequence<Bitmap<CyColor>>> BitmapSequenceManager =>
                (HandleMessage(FetchMessage<TowdResource>.Create(TowdResource.BitmapSequenceManager)) as FetchResult<Manager<string, Sequence<Bitmap<CyColor>>>>)?.Payload;
    }
}
