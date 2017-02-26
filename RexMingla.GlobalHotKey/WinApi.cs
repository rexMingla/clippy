using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace RexMingla.GlobalHotKey
{
    /// <summary>
    /// used from https://github.com/kirmir/GlobalHotKey/blob/master/GlobalHotKey/Internal/WinApi.cs
    /// </summary>
    public sealed class WinApi
    {
        /// <summary>
        /// Registers a system-wide hot key.
        /// </summary>
        /// <param name="handle">The handle of the window that will process hot key messages.</param>
        /// <param name="id">The hot key ID.</param>
        /// <param name="key">The key.</param>
        /// <param name="modifiers">The key modifiers.</param>
        /// <returns><c>true</c> if hot key was registered; otherwise, <c>false</c>.</returns>
        public static bool RegisterHotKey(IntPtr handle, int id, Key key, ModifierKeys modifiers)
        {
            var virtualCode = KeyInterop.VirtualKeyFromKey(key);
            return RegisterHotKey(handle, id, (uint)modifiers, (uint)virtualCode);
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public const int WmHotKey = 0x0312;
    }
}
