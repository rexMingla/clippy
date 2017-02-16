using System;
using System.Windows.Input;

namespace RexMingla.GlobalHotKey
{
    // adapted from https://gitlab.com/jbjurstam/GlobalHotkeysWPF/blob/master/GlobalHotkeys/GlobalHotkeys/GlobalHotkeys.cs
    public class Hotkey
    {
        private int _key;
        public int Id { get; private set; }
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
        public Hotkey(int id, ModifierKeys modifiers, Key key, Action action)
        {
            Id = id;
            Modifiers = modifiers;
            Key = key;
            Action = action;
        }
        public override string ToString()
        {
            return string.Format("Id: '{1}'{0}Modifiers: '{2}'{0}Key: '{3}'{0}Action: '{4}'{0}", Environment.NewLine, Id, Modifiers, Key, Action.Method.Name);
        }
    }
}
