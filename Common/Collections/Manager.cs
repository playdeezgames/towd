using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Manager<TKey, TManaged>
    {
        private Dictionary<TKey, TManaged> _table = new Dictionary<TKey, TManaged>();
        private Func<TKey, TManaged> _loader;
        public IEnumerable<TKey> Keys => _table.Keys;
        public Manager(Func<TKey, TManaged> loader)
        {
            _loader = loader;
        }
        public TManaged this[TKey key]
        {
            get
            {
                if(_table.TryGetValue(key, out TManaged managed))
                {
                    return managed;
                }
                else
                {
                    var created = _loader(key);
                    _table[key] = created;
                    return created;
                }
            }
            set
            {
                _table[key] = value;
            }
        }

    }
}
