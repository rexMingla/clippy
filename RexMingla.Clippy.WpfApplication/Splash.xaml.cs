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

        public Splash(IClipboardStore store, IClipboardManager manager)
        {
            InitializeComponent();

            _store = store;
            _manager = manager;
        }

        protected override void OnClosed(EventArgs e)
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

        private MenuItem CreateMenuItem(ClipboardContent content)
        {
            var item = content.ToMenuItem();
            item.Click += OnSetClipboardContent;
            return item;
        }

        private void OnSetClipboardContent(object sender, RoutedEventArgs e)
        {
            var item = e.Source as MenuItem;
            _manager.SetClipboardContent(item.DataContext as ClipboardContent);
        }
    }
}
