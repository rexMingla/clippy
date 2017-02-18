
using System;
using System.Windows.Input;

namespace RexMingla.WindowManager
{
    class WindowManager : IWindowManager
    {
        public WindowProperties GetCurrentWindow()
        {
            IntPtr handle = WinApi.GetForegroundWindow();
            var pos = new PointStruct();
            WinApi.GetCursorPos(out pos);
            return new WindowProperties
            {
                Handle = handle,
                Position = pos
            };
        }

        public void SetCurrentWindow(WindowProperties properties)
        {
            WinApi.SwitchToThisWindow(properties.Handle, false);
        }
    }
}
