using Common.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace RexMingla.GlobalHotKey
{
    // based on https://github.com/kirmir/GlobalHotKey/blob/master/GlobalHotKey/HotKeyManager.cs
    public class HotKeyManager : IHotKeyManager, IDisposable
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly HwndSource _windowHandleSource;

        private readonly List<HotKey> _hotKeys;

        public HotKeyManager(Window window)
        {
            var windowHandle = new WindowInteropHelper(window).Handle;
            _windowHandleSource = HwndSource.FromHwnd(windowHandle);
            _windowHandleSource.AddHook(HwndHook);

            _hotKeys = new List<HotKey>();
        }

        public void Register(HotKey hotKey)
        {
            // Check if specified hot key is already registered.
            if (_hotKeys.SingleOrDefault(k => (k.Key == hotKey.Key && k.Modifiers == hotKey.Modifiers) || k.Id == hotKey.Id) != null)
            {
                throw new ArgumentException($"The specified hot key {hotKey} is already registered.");
            }
            
            // Register new hot key.
            if (!WinApi.RegisterHotKey(_windowHandleSource.Handle, hotKey.Id, hotKey.Key, hotKey.Modifiers))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), $"Can't register the hot key {hotKey}.");
            }
            _hotKeys.Add(hotKey);
        }

        /// <summary>
        /// Unregisters previously registered hot key.
        /// </summary>
        /// <param name="hotKey">The registered hot key.</param>
        public void Unregister(HotKey hotKey)
        {
            var matchedHotKey = _hotKeys.SingleOrDefault(k => k.Id == hotKey.Id);
            if (matchedHotKey == null)
            {
                throw new ArgumentException("The specified hot key is was not registered.");
            }
            WinApi.UnregisterHotKey(_windowHandleSource.Handle, matchedHotKey.Id);
            _hotKeys.Remove(matchedHotKey);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Unregister hot keys.
            foreach (var hotKey in _hotKeys)
            {
                WinApi.UnregisterHotKey(_windowHandleSource.Handle, hotKey.Id);
            }

            _windowHandleSource.RemoveHook(HwndHook);
            _windowHandleSource.Dispose();
        }

        private IntPtr HwndHook(IntPtr handle, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (message == WinApi.WmHotKey)
            {
                var key = KeyInterop.KeyFromVirtualKey(((int)lParam >> 16) & 0xFFFF);
                var modifiers = (ModifierKeys)((int)lParam & 0xFFFF);

                var hotKey = _hotKeys.SingleOrDefault(k => k.Key == key && k.Modifiers == modifiers);
                if (hotKey == null)
                {
                    _log.Trace($"Hot key with {key} keys and {modifiers} modifiers ignored as it is not being listened to.");
                    return IntPtr.Zero;
                }
                _log.Trace($"Calling action {hotKey.Action.Method.Name}");
                hotKey.Action();
                handled = true;
                return new IntPtr(1);
            }
            return IntPtr.Zero;
        }
    }
}
