using Common.Logging;
using RexMingla.ClipboardManager;
using RexMingla.Clippy.Config;
using RexMingla.GlobalHotKey;
using RexMingla.WindowManager;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Input;

namespace RexMingla.Clippy.WpfApplication
{
    /// <summary>
    ///  co-ordinates the click event right through to paste of selected content
    /// </summary>
    public sealed class ClipboardOrchestrator : IClipboardOrchestrator, IDisposable
    {
        private readonly IHotKeyManager _hotKeyManager;
        private readonly IClipboardManager _clipboardManager;
        private readonly IWindowManager _windowManager;
        private readonly IKeySender _keySender;
        private readonly IClipboardStore _clipboardStore;
        private readonly IConfigManager _configManager;
        private readonly IClipboardNotifier _clipboardNotifier;

        private readonly HotKey _showMenuHotKey;
        private WindowProperties _previousWindowProperties;

        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ClipboardOrchestrator(
            IHotKeyManager hotKeyManager,
            IClipboardManager clipboardManager,
            IWindowManager windowManager,
            IKeySender keySender,
            IClipboardStore clipboardStore,
            IConfigManager configManager,
            IClipboardNotifier clipboardNotifier)
        {
            _hotKeyManager = hotKeyManager;
            _clipboardManager = clipboardManager;
            _windowManager = windowManager;
            _keySender = keySender;
            _configManager = configManager;
            _clipboardStore = clipboardStore;
            _clipboardNotifier = clipboardNotifier;

            _showMenuHotKey = new HotKey(ModifierKeys.Control | ModifierKeys.Shift, Key.V, OnShowContextMenu);

            _configManager.OnClipboardHistoryChanged += _clipboardStore.SetItems;
            _configManager.LoadConfig();
        }

        public void Start()
        {
            _hotKeyManager.Register(_showMenuHotKey);
            _clipboardNotifier.Start();
        }

        public void Stop()
        {
            _clipboardNotifier.Stop();
            Dispose();
        }

        private void OnShowContextMenu()
        {
            _log.Info("Getting focus window");
            _previousWindowProperties = _windowManager.GetCurrentWindow();
            _log.Debug($"focus window handle is {_previousWindowProperties.Handle}");

            _log.Debug("Showing menu");
 
            //_menu.Show();

            // TODO: remove this!
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
            _log.Info($"Clipboard selection made {content}");
            if (content == null)
            {
                return;
            }
            _log.Debug($"Setting content");
            _clipboardManager.SetClipboardContent(content);
            _log.Debug($"Setting current window");
            _windowManager.SetCurrentWindow(_previousWindowProperties);
            _log.Debug($"Performing paste");
            _keySender.SendPasteCommand(_previousWindowProperties);

            _previousWindowProperties = null;
        }

        public void Dispose()
        {
            _configManager.OnClipboardHistoryChanged -= _clipboardStore.SetItems;
            _configManager.SetClipboardHistory(_clipboardStore.GetItems());
            _configManager.SaveConfig();
        }
    }
}
