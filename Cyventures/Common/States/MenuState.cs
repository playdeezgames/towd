using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class MenuState<T,C>: State<T,C>
    {
        private HashSet<C> _nextItem;
        private HashSet<C> _previousItem;
        private HashSet<C> _selectItem;
        private int _itemCount;
        protected int CurrentIndex { get; private set; }
        public MenuState(StateManager<T,C> manager, int itemCount, HashSet<C> nextItem, HashSet<C> previousItem, HashSet<C> selectItem)
            :base (manager)
        {
            _nextItem = nextItem;
            _previousItem = previousItem;
            _selectItem = selectItem;
            _itemCount = itemCount;
        }
        public abstract void OnItemSelected(int itemIndex);
        public override void DoCommand(C command)
        {
            if(_nextItem.Contains(command))
            {
                CurrentIndex = (CurrentIndex + 1) % _itemCount;
            }
            else if (_previousItem.Contains(command))
            {
                CurrentIndex = (CurrentIndex + _itemCount - 1) % _itemCount;
            }
            else if (_selectItem.Contains(command))
            {
                OnItemSelected(CurrentIndex);
            }
        }
    }
}
