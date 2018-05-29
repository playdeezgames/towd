using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontEditor
{
    public abstract class CyMenuState<T> : MenuState<T, Command>
    {
        private ColorBuffer<CyColor> _screen;
        private CyFont _font;
        private readonly List<string> _items;
        private readonly string _title;

        public CyMenuState(StateManager<T, Command> manager, ColorBuffer<CyColor> screen, CyFont font, string title, List<string> items)
            : base(manager, items.Count(), new HashSet<Command>() { Command.Down }, new HashSet<Command>() { Command.Up }, new HashSet<Command>() { Command.Select, Command.Enter })
        {
            _screen = screen;
            _font = font;
            _items = items;
            _title = title;
        }


        public override void Update(TimeSpan elapsed)
        {
            _screen.Clear(CyColor.White);

            _screen.Box(0, _font.Height * 0, _screen.Width, _font.Height, CyColor.DarkGray);
            _font.WriteText(_screen, CyColor.LightGray, 0, _font.Height * 0, _title);

            _screen.Box(0, _font.Height * (1 + CurrentIndex), _screen.Width, _font.Height, CyColor.Black);
            for (var index = 0; index < _items.Count(); ++index)
            {
                _font.WriteText(_screen, (index == CurrentIndex) ? (CyColor.White) : (CyColor.Black), 0, _font.Height * (1 + index), _items[index]);
            }
        }
    }
}
