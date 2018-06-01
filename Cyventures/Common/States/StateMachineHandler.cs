using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class StateMachineHandler<TPixel, TState> : MessageHandlerBase<TPixel>
    {
        private Dictionary<TState, StateHandler<TPixel, TState>> _stateHandlers = new Dictionary<TState, StateHandler<TPixel, TState>>();
        private TState _current;
        protected TState Current
        {
            get
            {
                return _current;
            }
            set
            {
                this[Current]?.Stop();
                _current = value;
                this[Current]?.Start();
            }
        }
        protected StateHandler<TPixel,TState> this[TState index]
        {
            get
            {
                _stateHandlers.TryGetValue(index, out StateHandler<TPixel, TState> stateHandler);
                return stateHandler;
            }
            set
            {
                _stateHandlers[index] = value;
            }
        }
        public StateMachineHandler(MessageHandlerBase<TPixel> parent)
            : base(parent, true, null)
        {

        }
        protected override bool OnCommand(Command command)
        {
            return false;
        }

        protected override IResult OnMessage(IMessage message)
        {
            if(message.MessageId== SetStateMessage.Id)
            {
                var specific = (message as SetStateMessage<TState>);
                if(specific!=null)
                {
                    Current = specific.State;
                    return AckResult<TPixel>.Create(message, this);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        protected override void OnUpdate(IPixelWriter<TPixel> pixelWriter, CyRect? clipRect)
        {
            //do nothing
        }
    }
}
