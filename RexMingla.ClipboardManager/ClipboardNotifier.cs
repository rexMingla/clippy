using RexMingla.DataModel;
using System;
using System.Threading;
using System.Windows.Forms;

namespace RexMingla.ClipboardManager
{
    public sealed class ClipboardNotifier : IClipboardNotifier, IDisposable
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
            }
            return _instance;
        }

        public void Start()
        {
            ClipboardWatcher.Start();
            ClipboardWatcher.OnClipboardChange += (ClipboardContent content) =>
            {
                OnClipboardChange?.Invoke(content);
            };
        }

        public void Stop()
        {
            OnClipboardChange = null;
            ClipboardWatcher.Stop();
        }

        public void Dispose()
        {
            Stop();
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
                    WinApi.ChangeClipboardChain(_instance.Handle, nextClipboardViewer);
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

                nextClipboardViewer = WinApi.SetClipboardViewer(_instance.Handle);

                base.SetVisibleCore(false);
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case WinApi.WM_DRAWCLIPBOARD:
                        ClipChanged();
                        WinApi.SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                        break;

                    case WinApi.WM_CHANGECBCHAIN:
                        if (m.WParam == nextClipboardViewer)
                            nextClipboardViewer = m.LParam;
                        else
                            WinApi.SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
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
                OnClipboardChange?.Invoke(data.ToClipboardContent());
            }
        }
    }
}
