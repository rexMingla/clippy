using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace RexMingla.WindowManager
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PointStruct
    {
        public int X;
        public int Y;

        public static implicit operator Point(PointStruct point)
        {
            return new Point(point.X, point.Y);
        }
    }

    public sealed class WinApi
    {
        public const uint WM_KEYDOWN = 0x100;

        // virtual keys: https://msdn.microsoft.com/en-us/library/dd375731(v=vs.85).aspx
        public const int VK_CONTROL = 0x11;
        public const int VK_V = 0x56;

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633548(v=vs.85).aspx
        public const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(HandleRef hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool isAltTab);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out PointStruct lpPoint);
    }
}
