using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class StateManagerOld<T,C>
    {
        private Dictionary<T, StateOld<T,C>> _states = new Dictionary<T, StateOld<T, C>>();
        public T Current { get; private set; }
        private Stack<T> _stack = new Stack<T>();
        public void Push(T state)
        {
            _stack.Push(Current);
            Set(state);
        }
        public void Pop()
        {
            Set(_stack.Pop());
        }
        public void Set(T state)
        {
            Current = state;
        }
        public StateOld<T, C> this[T index]
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
