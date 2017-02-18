using RexMingla.ClipboardManager;
using RexMingla.GlobalHotKey;
using RexMingla.WindowManager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;

namespace RexMingla.Clippy.WpfApplication
{
    /// <summary>
    ///  co-ordinates the click event right through to paste of selected content
    /// </summary>
    public class ClipboardOrchestrator : IClipboardOrchestrator
    {
        private readonly IHotKeyManager _hotKeyManager;
        private readonly HotKey _showMenuHotKey;
        private readonly IClipboardManager _clipboardManager;
        private readonly IWindowManager _windowManager;
        private readonly IKeySender _keySender;

        private WindowProperties _previousWindowProperties;

        public ClipboardOrchestrator(
            IHotKeyManager hotKeyManager,
            IClipboardManager clipboardManager,
            IWindowManager windowManager,
            IKeySender keySender)
        {
            _hotKeyManager = hotKeyManager;
            _clipboardManager = clipboardManager;
            _windowManager = windowManager;
            _keySender = keySender;

            _showMenuHotKey = new HotKey(ModifierKeys.Control | ModifierKeys.Shift, Key.V, OnShowContextMenu);
        }

        public void Start()
        {
            _hotKeyManager.Register(_showMenuHotKey);
        }

        private void OnShowContextMenu()
        {
            // get focus window (for pasting later)
            _previousWindowProperties = _windowManager.GetCurrentWindow();
            // show menu
            OnActionSelected(new ClipboardContent
            {
                Data = new List<ClipboardData>()
                {
                    new ClipboardData() {
                        DataFormat = "Text",
                        Content = DateTime.Now.ToLongTimeString()
                    }
                }
            });
        }

        private void OnActionSelected(ClipboardContent content)
        {
            if (content == null)
            {
                return;
            }
            _clipboardManager.SetClipboardContent(content);
            _windowManager.SetCurrentWindow(_previousWindowProperties);
            _keySender.SendPasteCommand(_previousWindowProperties);

            _previousWindowProperties = null;
        }
    }
}
