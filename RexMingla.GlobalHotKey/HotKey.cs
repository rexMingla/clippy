using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace RexMingla.GlobalHotKey
{
    // adapted from https://gitlab.com/jbjurstam/GlobalHotkeysWPF/blob/master/GlobalHotkeys/GlobalHotkeys/GlobalHotkeys.cs
    public class HotKey
    {
        private static readonly ISet<int> _usedIds = new HashSet<int>();

        private int _key;
        public int Id { get; set; }
        public ModifierKeys Modifiers { get; private set; }
        public Action Action { get; private set; }
        public int VirtualKey { get { return _key; } }
        public Key Key
        {
            get
            {
                return KeyInterop.KeyFromVirtualKey(_key);
            }
            private set
            {
                _key = KeyInterop.VirtualKeyFromKey(value);
            }
        }

        public HotKey(ModifierKeys modifiers, Key key, Action action) : this(modifiers, key, action, GetNextId())
        {
        }

        public HotKey(ModifierKeys modifiers, Key key, Action action, int id)
        {
            Id = id;
            Modifiers = modifiers;
            Key = key;
            Action = action;

            _usedIds.Add(id);
        }

        public override string ToString()
        {
            return string.Format($"<HotKey Id:{Id} Modifiers:{Modifiers} Key:{Key} Action: {Action.Method.Name}>");
        }

        // don't really want this here. 
        private static int GetNextId()
        {
            return _usedIds.Any() ? _usedIds.Max() + 1 : 0;
        }
    }
}
