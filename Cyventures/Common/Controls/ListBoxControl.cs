using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ListBoxControl : ControlBase
    {
        private CyColor _foreground;
        private CyColor _background;
        private CyFont _font;
        private List<string> _items;
        public IEnumerable<string> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value.ToList();
                Selected = 0;
            }
        }
        private int _offsetY;
        private int _selected;
        private Action<int> _onActivate;
        public int Selected
        {
            get
            {
                return _selected;
            }
            private set
            {
                _selected = value;
                AdjustOffsetY();
            }
        }

        private void AdjustOffsetY()
        {
            int top = _offsetY + Selected * _font.Height;
            int bottom = top + _font.Height;
            if(top<0)
            {
                _offsetY -= top;
            }
            else if(bottom>Height)
            {
                _offsetY += (Height - bottom);
            }
        }

        public ListBoxControl(IMessageHandler<CyColor> parent, bool enabled, CyRect bounds, CyFont font, IEnumerable<string> items, int selected, CyColor foreground, CyColor background, Action<int> onActivate) : base(parent, enabled, bounds)
        {
            _font = font;
            _items = items.ToList();
            _offsetY = 0;
            _foreground = foreground;
            _background = background;
            _onActivate = onActivate;
            Selected = selected;
        }

        protected override void OnBlur()
        {
        }

        protected override void OnFocus()
        {
        }

        protected override bool OnFocusCommand(Command command)
        {
            switch(command)
            {
                case Command.Down:
                    NextItem();
                    return true;
                case Command.Up:
                    PreviousItem();
                    return true;
                case Command.Green:
                case Command.Blue:
                    if(_onActivate!=null)
                    {
                        _onActivate(Selected);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        private void PreviousItem()
        {
            if (_items.Any())
            {
                Selected = (Selected + _items.Count - 1) % _items.Count;
            }
            else
            {
                Selected = 0;
            }
        }

        private void NextItem()
        {
            if (_items.Any())
            {
                Selected = (Selected + 1) % _items.Count;
            }
            else
            {
                Selected = 0;
            }
        }

        protected override IResult OnMessage(IMessage message)
        {
            return null;
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
            pixelWriter.Clear(_background, clipRect);
            int y = _offsetY;
            for(int index=0;index<_items.Count();++index)
            {
                if(index==Selected)
                {
                    pixelWriter.Box(CyRect.Create(0, y, Width, _font.Height), _foreground, clipRect);
                    _font.Draw(pixelWriter, _background, 0, y, _items[index], clipRect);
                }
                else
                {
                    _font.Draw(pixelWriter, _foreground, 0, y, _items[index], clipRect);
                }
                y += _font.Height;
            }
        }
    }
}
