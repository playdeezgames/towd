using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class StateOld<T,C>
    {
        protected StateManagerOld<T,C> Manager { get; private set; }
        private StateOld() { }
        public StateOld(StateManagerOld<T,C> manager)
        {
            Manager = manager;
        }
        public abstract void Update(TimeSpan elapsed);
        public abstract void DoCommand(C command);
    }
}
