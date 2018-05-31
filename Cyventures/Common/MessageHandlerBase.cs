using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class MessageHandlerBase : IMessageHandler
    {
        private MessageHandlerBase() { }
        public MessageHandlerBase(IMessageHandler parent)
        {
            Parent = parent;
        }
        private IMessageHandler _parent = null;
        public IMessageHandler Parent
        {
            get
            {
                return _parent;
            }
            private set
            {
                if(_parent!=null)
                {
                    (_parent as MessageHandlerBase)?.RemoveChild(this);
                }
                _parent = value;
                if(_parent!=null)
                {
                    (_parent as MessageHandlerBase)?.AddChild(this);
                }
            }
        }
        private LinkedList<IMessageHandler> _children = new LinkedList<IMessageHandler>();
        public void AddChild(IMessageHandler child)
        {
            RemoveChild(child);
            _children.AddLast(child);
        }
        public void RemoveChild(IMessageHandler child)
        {
            _children.Remove(child);
        }

        protected abstract IResult OnMessage(IMessage message);

        public void Broadcast(IMessage message, bool reverseOrder = false)
        {
            if (reverseOrder)
            {
                var item = _children.Last;
                while (item != null)
                {
                    item.Value.HandleBroadcast(message, reverseOrder);
                    item = item.Previous;
                }
                OnMessage(message);
            }
            else
            {
                OnMessage(message);
                var item = _children.First;
                while (item != null)
                {
                    item.Value.HandleBroadcast(message, reverseOrder);
                    item = item.Next;
                }
            }
        }

        public IResult HandleBroadcast(IMessage message, bool reverseOrder = false)
        {
            if(reverseOrder)
            {
                var item = _children.Last;
                while (item != null)
                {
                    var result = item.Value.HandleBroadcast(message, reverseOrder);
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        item = item.Previous;
                    }
                }
                return OnMessage(message);
            }
            else
            {
                var result = OnMessage(message);
                if (result == null)
                {
                    var item = _children.First;
                    while (item != null)
                    {
                        result = item.Value.HandleBroadcast(message, reverseOrder);
                        if (result != null)
                        {
                            return result;
                        }
                        else
                        {
                            item = item.Next;
                        }
                    }
                }
                else
                {
                    return result;
                }
            }
            return null;
        }

        public IResult HandleMessage(IMessage message)
        {
            return OnMessage(message) ?? Parent?.HandleMessage(message);
        }
    }
}
