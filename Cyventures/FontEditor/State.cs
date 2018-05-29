using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontEditor
{
    public abstract class State<T,C>
    {
        protected StateManager<T,C> Manager { get; private set; }
        private State() { }
        public State(StateManager<T,C> manager)
        {
            Manager = manager;
        }
        public abstract void Update(TimeSpan elapsed);
        public abstract void DoCommand(C command);
    }
}
