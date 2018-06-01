using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class StateHandler<TPixel, TState> : MessageHandlerBase<TPixel>
    {
        public StateHandler(StateMachineHandler<TPixel,TState> parent, CyRect? bounds)
            :base(parent,false,bounds)
        {

        }
        protected abstract void OnStart();
        protected abstract void OnStop();
        public void Start()
        {
            Enabled = true;
            OnStart();
        }
        public void Stop()
        {
            OnStop();
            Enabled = false;
        }

    }
}
