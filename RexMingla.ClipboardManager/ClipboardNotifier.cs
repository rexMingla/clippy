using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace RexMingla.ClipboardManager
{
    public class ClipboardNotifier : IClipboardNotifier, IDisposable
    {
        public event OnClipboardChangeEventHandler OnClipboardChange;

        private static ClipboardNotifier _instance;
        private ClipboardWatcher _clipboardWatcher;

        private ClipboardNotifier()
        {
            _clipboardWatcher = new ClipboardWatcher();
        }

        public static ClipboardNotifier GetNotifier()
        {
            if (_instance == null)
            {
                _instance = new ClipboardNotifier();
                _instance.Start();
            }
            return _instance;
        }

        private void Start()
        {
            ClipboardWatcher.Start();
            ClipboardWatcher.OnClipboardChange += (ClipboardContent content) =>
            {
                OnClipboardChange?.Invoke(content);
            };
        }

        public void Dispose()
        {
            OnClipboardChange = null;
            ClipboardWatcher.Stop();
        }

        class ClipboardWatcher : Form
        {
            // static instance of this form
            private static ClipboardWatcher _instance;

            // needed to dispose this form
            static IntPtr nextClipboardViewer;

            public delegate void OnClipboardChangeEventHandler(ClipboardContent content);
            public static event OnClipboardChangeEventHandler OnClipboardChange;

            // start listening
            public static void Start()
            {
                var t = new Thread(new ParameterizedThreadStart(x => Application.Run(new ClipboardWatcher())));
                t.SetApartmentState(ApartmentState.STA); // give the [STAThread] attribute
                t.Start();
            }

            // stop listening (dispose form)
            public static void Stop()
            {
                _instance.Invoke(new MethodInvoker(() =>
                {
                    ChangeClipboardChain(_instance.Handle, nextClipboardViewer);
                }));
                _instance.Invoke(new MethodInvoker(_instance.Close));

                _instance.Dispose();

                _instance = null;
            }

            // on load: (hide this window)
            protected override void SetVisibleCore(bool value)
            {
                CreateHandle();

                _instance = this;

                nextClipboardViewer = SetClipboardViewer(_instance.Handle);

                base.SetVisibleCore(false);
            }

            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case WM_DRAWCLIPBOARD:
                        ClipChanged();
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                        break;

                    case WM_CHANGECBCHAIN:
                        if (m.WParam == nextClipboardViewer)
                            nextClipboardViewer = m.LParam;
                        else
                            SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                        break;

                    default:
                        base.WndProc(ref m);
                        break;
                }
            }

            private void ClipChanged()
            {
                IDataObject data = Clipboard.GetDataObject();
                if (data == null)
                {
                    return;
                }
                OnClipboardChange(data.ToClipboardContent());
            }
        }
    }
}
