using RexMingla.ClipboardManager;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RexMingla.Clippy.WpfApplication
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        private readonly IClipboardStore _store;
        private readonly IClipboardManager _manager;
        private readonly MenuItemTranslator _translator;

        public Splash(IClipboardStore store, IClipboardManager manager, MenuItemTranslator translator)
        {
            InitializeComponent();

            _store = store;
            _manager = manager;
            _translator = translator;
        }

        private void OnClosed(object sender, EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        void CreateContentMenu(object sender, ContextMenuEventArgs e)
        {
            e.Handled = true; //need to suppress empty menu
            FrameworkElement fe = e.Source as FrameworkElement;
            fe.ContextMenu = BuildMenu();
            fe.ContextMenu.IsOpen = true;
        }

        private ContextMenu BuildMenu()
        {
            var ret = new ContextMenu();

            var items = _store.GetItems();
            if (!items.Any())
            {
                ret.Items.Add(new MenuItem { Header = "No items in clipboard", IsEnabled = false });
            }
            foreach (var content in items)
            {
                ret.Items.Add(CreateMenuItem(content));
            }
            return ret;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //clean up notifyicon (would otherwise stay open until application finishes)
            TrayIcon.Dispose();

            base.OnClosing(e);
        }

        private MenuItem CreateMenuItem(ClipboardContent content)
        {
            var item = _translator.ToMenuItem(content);
            item.Click += OnSetClipboardContent;
            return item;
        }

        private void OnSetClipboardContent(object sender, RoutedEventArgs e)
        {
            var item = e.Source as MenuItem;
            _manager.SetClipboardContent(item.DataContext as ClipboardContent);
        }

        private void TrayContextMenuOpen(object sender, RoutedEventArgs e)
        {
            //OpenEventCounter.Text = (int.Parse(OpenEventCounter.Text) + 1).ToString();
        }

        private void PreviewTrayContextMenuOpen(object sender, RoutedEventArgs e)
        {
            ////marking the event as handled suppresses the context menu
            //e.Handled = (bool)SuppressContextMenu.IsChecked;

            //PreviewOpenEventCounter.Text = (int.Parse(PreviewOpenEventCounter.Text) + 1).ToString();
        }
    }
}
