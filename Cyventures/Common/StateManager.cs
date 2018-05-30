using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class StateManager<T,C>
    {
        private Dictionary<T, State<T,C>> _states = new Dictionary<T, State<T, C>>();
        public T Current { get; set; }
        public State<T, C> this[T index]
        {
            get
            {
                return _states[index];
            }
            set
            {
                _states[index] = value;
            }
        }
        public void Update(TimeSpan elapsed)
        {
            this[Current].Update(elapsed);
        }
        public void DoCommand(C command)
        {
            this[Current].DoCommand(command);
        }
    }
}
