using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class MessageHandlerBase<T> : IMessageHandler<T>
    {
        private MessageHandlerBase() { }
        public MessageHandlerBase(IMessageHandler<T> parent, bool enabled, CyRect? bounds)
        {
            Parent = parent;
            Enabled = enabled;
            X = bounds?.X ?? Parent?.X ?? 0;
            Y = bounds?.Y ?? Parent?.Y ?? 0;
            Width = bounds?.Width ?? Parent?.Width ?? 0;
            Height = bounds?.Height ?? Parent?.Height ?? 0;
        }
        private IMessageHandler<T> _parent = null;
        public IMessageHandler<T> Parent
        {
            get
            {
                return _parent;
            }
            private set
            {
                if(_parent!=null)
                {
                    (_parent as MessageHandlerBase<T>)?.RemoveChild(this);
                }
                _parent = value;
                if(_parent!=null)
                {
                    (_parent as MessageHandlerBase<T>)?.AddChild(this);
                }
            }
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int GlobalX => (Parent?.X ?? 0) + X;

        public int GlobalY => (Parent?.Y ?? 0) + Y;
        public bool Enabled { get; set; }
        public bool GlobalEnabled => (Parent?.Enabled ?? true) && Enabled;

        private LinkedList<IMessageHandler<T>> _children = new LinkedList<IMessageHandler<T>>();
        public void AddChild(IMessageHandler<T> child)
        {
            RemoveChild(child);
            _children.AddLast(child);
        }
        public void RemoveChild(IMessageHandler<T> child)
        {
            _children.Remove(child);
        }

        protected abstract IResult OnMessage(IMessage message);

        public IResult HandleMessage(IMessage message)
        {
            return OnMessage(message) ?? Parent?.HandleMessage(message);
        }

        protected abstract void OnUpdate(IPixelWriter<T> pixelWriter, CyRect? clipRect);

        public void Update(IPixelWriter<T> pixelWriter)
        {
            if(GlobalEnabled)
            {
                OnUpdate(pixelWriter, CyRect.Create(GlobalX, GlobalY, Width, Height));
                var child = _children.First;
                while (child != null)
                {
                    child.Value.Update(pixelWriter);
                    child = child.Next;
                }
            }
        }

        protected abstract bool OnCommand(Command command);

        public bool HandleCommand(Command command)
        {
            if(!GlobalEnabled)
            {
                return false;
            }
            var child = _children.Last;
            while(child!=null)
            {
                if(child.Value.HandleCommand(command))
                {
                    return true;
                }
                child = child.Previous;
            }
            return OnCommand(command);
        }
    }
}
