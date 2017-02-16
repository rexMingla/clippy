using Common.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Interop;

namespace RexMingla.GlobalHotKey
{
    public class HotKeyManager : IDisposable, IHotKeyManager
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static HotKeyManager _manager;

        private HotKeyManager()
        {
            HotKeyListener.Start();
        }

        public static HotKeyManager GetManager()
        {
            if (_manager == null)
            {
                _manager = new HotKeyManager();
            }
            return _manager;
        }

        public void Register(Hotkey key)
        {
            HotKeyListener.Add(key);
        }

        public void Unregister(Hotkey key)
        {
            HotKeyListener.Remove(key);
        }

        public void Dispose()
        {
            HotKeyListener.Stop();
        }

        private class OperationAndKey
        {
            public bool IsAdd { get; set; }
            public Hotkey Key { get; set; }
        }

        public class HotKeyListener : Form, IDisposable
        {
            private static readonly BlockingCollection<OperationAndKey> Queue = new BlockingCollection<OperationAndKey>();

            // static instance of this form
            private static HotKeyListener _instance;

            readonly List<Hotkey> _hotkeys = new List<Hotkey>();

            private readonly HwndSource _windowHandleSource;


            // start listening
            public static void Start()
            {
                var t = new Thread(new ParameterizedThreadStart(x => Application.Run(new HotKeyListener())));
                t.SetApartmentState(ApartmentState.STA); // give the [STAThread] attribute
                t.Start();
            }

            public static void Stop()
            {
                _instance.Invoke(new MethodInvoker(_instance.Close));

                _instance.Dispose();

                _instance = null;
            }


            // on load: (hide this window)
            protected override void SetVisibleCore(bool value)
            {
                _instance = this;
                base.SetVisibleCore(false);
            }

            public HotKeyListener()
            {
                _windowHandleSource = new HwndSource(new HwndSourceParameters());
                _windowHandleSource.AddHook(HwndHook);

                CreateHandle();
            }

            public IReadOnlyCollection<Hotkey> Items
            {
                get
                {
                    return _hotkeys.AsReadOnly();
                }
            }

            public static void Add(Hotkey hotkey)
            {
                Queue.Add(new OperationAndKey { Key = hotkey, IsAdd = true });
            }

            public static void Remove(Hotkey hotkey)
            {
                Queue.Add(new OperationAndKey { Key = hotkey, IsAdd = false });
            }

            public void Remove(params int[] hotkeyIds)
            {
                foreach (var hotkeyId in hotkeyIds)
                {
                    Remove(_instance._hotkeys.FirstOrDefault(h => h.Id == hotkeyId));
                }
            }

            void Register(Hotkey hotkey)
            {
                _log.Debug($"Registering hot key {hotkey}");           
                var existingHotKey = _hotkeys.FirstOrDefault(h => h.Id == hotkey.Id);
                if (existingHotKey != null)
                {
                    Remove(existingHotKey);
                }
                RegisterHotKey(Handle, hotkey.Id, (uint)hotkey.Modifiers, hotkey.VirtualKey);
                _hotkeys.Add(hotkey);
            }

            void Unregister(Hotkey hotkey)
            {
                _log.Debug($"Unregistering hot key {hotkey}");
                UnregisterHotKey(Handle, hotkey.Id);
                _hotkeys.Remove(hotkey);
            }

            enum WM_Case
            {
                Hotkey = 0x0312         //WM_HOTKEY
            }

            IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
            {
                while(Queue.Any())
                {
                    var item = Queue.Take();
                    if (item.IsAdd)
                    {
                        Register(item.Key);
                    } else
                    {
                        Unregister(item.Key);
                    }
                }

                switch (msg)
                {
                    case (int)WM_Case.Hotkey:
                        var hotkeyId = wParam.ToInt32();
                        var hotkey = _hotkeys.SingleOrDefault(h => h.Id == hotkeyId);
                        if (hotkey != null)
                            hotkey.Action();
                        break;
                }
                return IntPtr.Zero;
            }

            [DllImport("user32.dll")]
            static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, int vk);

            [DllImport("user32.dll")]
            static extern bool UnregisterHotKey(IntPtr hWnd, int id);

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                _hotkeys.ForEach(h => sb.Append(h).Append(Environment.NewLine));
                return sb.ToString();
            }

            //Cleanup and implement IDisposable
            void Unhook()
            {
                _windowHandleSource.RemoveHook(HwndHook);
                _hotkeys.ForEach(h => Unregister(h));
            }
            public void Dispose()
            {
                Unhook();
                GC.SuppressFinalize(this);
            }

            ~HotKeyListener()
            {
                Unhook();
            }
        }
    }
}
