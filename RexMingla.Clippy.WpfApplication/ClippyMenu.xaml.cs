using RexMingla.ClipboardManager;
using RexMingla.Clippy.Config;
using RexMingla.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RexMingla.Clippy.WpfApplication
{
    /// <summary>
    /// Interaction logic for ClippyMenu.xaml
    /// </summary>
    /// Reference: http://mubox.codeplex.com/SourceControl/changeset/view/64743#1119066
    public partial class ClippyMenu : Window, ISettingsListener
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon = null;

        private readonly IClipboardStore _clipboardStore;
        private readonly IMenuItemTranslator _translator;
        private readonly IClipboardManager _clipboardManager;
        private readonly IConfigManager _configManager;

        private Settings _settings;

        public ClippyMenu(IClipboardStore clipboardStore,
            IConfigManager configManager,
            IMenuItemTranslator translator,
            IClipboardManager clipboardManager)
        {
            InitializeComponent();

            _clipboardStore = clipboardStore;
            _configManager =  configManager;
            _translator = translator;
            _clipboardManager = clipboardManager;

            _configManager.RegisterSettingsListener(this);
        }

        protected override void OnInitialized(EventArgs e)
        {
            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.Click += notifyIcon_Click;
            _notifyIcon.Icon = new System.Drawing.Icon("images\\clipboard_32x32.ico");

            Closing += OnClosing;
            Closed += OnClosed;
            StateChanged += OnStateChanged;
            Loaded += OnLoaded;

            base.OnInitialized(e);
        }

        public void OnSettingsChanged(Settings settings)
        {
            _settings = settings;
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Topmost = false;
                ShowInTaskbar = false;
                _notifyIcon.Visible = true;
            }
            else
            {
                _notifyIcon.Visible = true;
                ShowInTaskbar = true;
                Topmost = true;
            }
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.WindowState != WindowState.Minimized)
            {
                e.Cancel = true;
                this.WindowState = WindowState.Minimized;
            }
        }

        private void OnClosed(object sender, System.EventArgs e)
        {
            try
            {
                if (_notifyIcon != null)
                {
                    _notifyIcon.Visible = false;
                    _notifyIcon.Dispose();
                    _notifyIcon = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void ShowSysTrayMenu()
        {
            ContextMenu = BuildMenu();
            ContextMenu.IsOpen = true;
        }

        private ContextMenu BuildMenu()
        {
            var ret = new ContextMenu
            {
                StaysOpen = false
            };

            ret.Items.Add(CreateMenuItem("Exit Clippy", QuitApp));
            ret.Items.Add(CreateMenuItem("Preferences...", ShowPreferences));
            ret.Items.Add(CreateMenuItem("Clear History", ClearHistory));
            ret.Items.Add(new Separator());
            ret.Items.Add(new MenuItem { Header = "Snippets", IsEnabled = false });
            ret.Items.Add(new Separator());
            AddHistoryItems(ret, _clipboardStore.GetItems().ToList());
            ret.Items.Add(new Separator());
            ret.Items.Add(new MenuItem { Header = "History", IsEnabled = false });
            return ret;
        }



        private void AddHistoryItems(ContextMenu ret, List<ClipboardContent> items)
        {
            var index = ret.Items.Count;
            if (!items.Any())
            {
                ret.Items.Add(new MenuItem { Header = "No items in clipboard", IsEnabled = false });
            }
            MenuItem subMenu = null;
            for (int i = 0; i < Math.Min(items.Count, _settings.MaxDisplayedItems); i++)
            {
                var item = CreateHistoryMenuItem(items[i]);
                if (i < _settings.ItemsPerMainGroup)
                {
                    item.InputGestureText = $"Alt+{i}";
                    ret.Items.Insert(index, item);
                    continue;
                }
                if ((i - _settings.ItemsPerMainGroup) % _settings.ItemsPerGroup == 0)
                {
                    subMenu = new MenuItem { Header = $"{i} - {i + _settings.ItemsPerGroup - 1}" };
                    ret.Items.Insert(index, subMenu);
                }
                subMenu.Items.Insert(index, item);
            }
        }

        private MenuItem CreateMenuItem(string header, RoutedEventHandler handler)
        {
            var item = new MenuItem { Header = header };
            item.Click += handler;
            return item;
        }

        private MenuItem CreateHistoryMenuItem(ClipboardContent content)
        {
            var item = _translator.ToMenuItem(content);
            item.Click += SetClipboardContent;
            return item;
        }

        private void ShowPreferences(object sender, RoutedEventArgs e)
        {
            var preferencesWindow = new PreferencesWindow(_configManager);
            preferencesWindow.ShowDialog();
        }

        private void ClearHistory(object sender, RoutedEventArgs e)
        {
            _clipboardStore.ClearItems();
        }

        private void QuitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void SetClipboardContent(object sender, RoutedEventArgs e)
        {
            var item = e.Source as MenuItem;
            _clipboardManager.SetClipboardContent(item.DataContext as ClipboardContent);
        }

        public void OnLoaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            Hide();
            _notifyIcon.Visible = true;
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            ShowSysTrayMenu();
        }
    }
}
