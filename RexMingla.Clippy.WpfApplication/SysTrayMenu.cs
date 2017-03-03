using RexMingla.ClipboardManager;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace RexMingla.Clippy.WpfApplication
{
    class SysTrayMenu : System.Windows.Controls.ContextMenu
    {
        //[DllImport("user32.dll")]
        //public static extern bool GetCursorPos(out System.Drawing.Point pt);

        //private System.Windows.Forms.MenuItem CreateMenuItem(ClipboardContent content, MenuItemTranslator translator)
        //{
        //    var item = translator.ToMenuItem(content);
        //    item.Text = "Test";
        //    //item.Click += OnSetClipboardContent;
        //    return item;
        //}

        //public SysTrayMenu(IClipboardStore clipboardStore, MenuItemTranslator translator, Action helpCallback, Action exitApplicationCallback)
        //{
        //    try
        //    {
        //        var mousePosition = new System.Drawing.Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right - 16, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Bottom - 24);
        //        GetCursorPos(out mousePosition);

        //        //List<object> quickLaunchMenuItems = new List<object>();
        //        //ItemsSource = quickLaunchMenuItems;

                
                
        //        var items = clipboardStore.GetItems();
        //        if (!items.Any())
        //        {
        //            Items.Add(new MenuItem { Header = "No items in clipboard", IsEnabled = false });
        //        }
        //        foreach (var content in items)
        //        {
        //            Items.Add(CreateMenuItem(content, translator));
        //        }

        //        //foreach (var team in Mubox.Configuration.MuboxConfigSection.Default.Teams.OfType<Mubox.Configuration.TeamSettings>())
        //        //{
        //        //    menuItem = CreateTeamShortcutMenu(team);
        //        //    if (menuItem != null)
        //        //    {
        //        //        quickLaunchMenuItems.Add(menuItem);
        //        //    }
        //        //}
        //        //quickLaunchMenuItems.Add(new Separator());

        //        // New Mubox Client
        //        //menuItem = new MenuItem();
        //        //menuItem.Click += (sender, e) =>
        //        //{
        //        //    try
        //        //    {
        //        //        string clientName = Mubox.View.PromptForClientNameDialog.PromptForClientName();
        //        //        // TODO try and enforce "unique" client names, e.g. if we already have a ClientX running, don't allow a second ClientX without warning.

        //        //        var clientSettings = Mubox.Configuration.MuboxConfigSection.Default.Teams.ActiveTeam.Clients.GetOrCreateNew(clientName);
        //        //        clientSettings.CanLaunch = true;
        //        //        Mubox.Configuration.MuboxConfigSection.Default.Save();

        //        //        ClientState clientState = new ClientState(clientSettings);
        //        //        Mubox.View.Client.ClientWindow clientWindow = new Mubox.View.Client.ClientWindow(clientState);
        //        //        clientWindow.Show();
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        Debug.WriteLine(ex.Message);
        //        //        Debug.WriteLine(ex.StackTrace);
        //        //    }
        //        //};
        //        //quickLaunchMenuItems.Add(menuItem);

        //        //// "Disable 'Client Switching' Feature"
        //        //menuItem = new MenuItem();
        //        //menuItem.IsCheckable = true;
        //        //menuItem.IsChecked = Mubox.Configuration.MuboxConfigSection.Default.DisableAltTabHook;
        //        //menuItem.Click += (sender, e) =>
        //        //{
        //        //    Mubox.Configuration.MuboxConfigSection.Default.DisableAltTabHook = !Mubox.Configuration.MuboxConfigSection.Default.DisableAltTabHook;
        //        //    Mubox.Configuration.MuboxConfigSection.Default.Save();
        //        //};
        //        //menuItem.Header = "Disable \"Client Switching\" Feature";
        //        //menuItem.ToolTip = "Enable this option to use the default Windows Task Switcher instead of the Mubox Server UI, this only affects Client Switching.";
        //        //quickLaunchMenuItems.Add(menuItem);

        //        //// "Reverse Client Switching"
        //        //menuItem = new MenuItem();
        //        //menuItem.IsCheckable = true;
        //        //menuItem.IsChecked = Mubox.Configuration.MuboxConfigSection.Default.ReverseClientSwitching;
        //        //menuItem.Click += (sender, e) =>
        //        //{
        //        //    Mubox.Configuration.MuboxConfigSection.Default.ReverseClientSwitching = !Mubox.Configuration.MuboxConfigSection.Default.ReverseClientSwitching;
        //        //    Mubox.Configuration.MuboxConfigSection.Default.Save();
        //        //};
        //        //menuItem.Header = "Reverse Client Switching";
        //        //menuItem.ToolTip = "Enable this option to reverse the order that Client Switcher will switch between clients.";
        //        //quickLaunchMenuItems.Add(menuItem);

        //        //// "Toggle Server UI"
        //        //menuItem = new MenuItem();
        //        //menuItem.Click += (sender, e) =>
        //        //{
        //        //    if (Mubox.View.Server.ServerWindow.Instance != null)
        //        //    {
        //        //        Mubox.View.Server.ServerWindow.Instance.SetInputCapture((Mubox.View.Server.ServerWindow.Instance.Visibility == Visibility.Visible), (Mubox.View.Server.ServerWindow.Instance.Visibility != Visibility.Visible));
        //        //    }
        //        //};
        //        //menuItem.Header = "Toggle Server UI";
        //        //menuItem.ToolTip = "Show/Hide the Server UI";
        //        //quickLaunchMenuItems.Add(menuItem);

        //        //quickLaunchMenuItems.Add(new Separator());

        //        //// "Enable Input Capture"
        //        //menuItem = new MenuItem();
        //        //menuItem.IsCheckable = true;
        //        //menuItem.IsChecked = Mubox.Configuration.MuboxConfigSection.Default.IsCaptureEnabled;
        //        //menuItem.Click += (sender, e) =>
        //        //{
        //        //    if (Mubox.View.Server.ServerWindow.Instance != null)
        //        //    {
        //        //        Mubox.View.Server.ServerWindow.Instance.ToggleInputCapture(false);
        //        //    }
        //        //};
        //        //menuItem.Header = "Enable Input Capture";
        //        //menuItem.ToolTip = "'Input Capture' includes both Mouse and Keyboard Input";
        //        //quickLaunchMenuItems.Add(menuItem);

        //        //// "Configure Keyboard"
        //        //menuItem = new MenuItem();
        //        //menuItem.Click += (sender, e) =>
        //        //{
        //        //    Mubox.View.Server.MulticastConfigDialog.ShowStaticDialog();
        //        //};
        //        //menuItem.Header = "Configure Keyboard..";
        //        //quickLaunchMenuItems.Add(menuItem);

        //        //if (Mubox.Configuration.MuboxConfigSection.Default.IsCaptureEnabled)
        //        //{
        //        //    // "Enable Multicast"
        //        //    menuItem = new MenuItem();
        //        //    menuItem.IsCheckable = true;
        //        //    menuItem.IsChecked = Mubox.Configuration.MuboxConfigSection.Default.EnableMulticast;
        //        //    menuItem.Click += (sender, e) =>
        //        //    {
        //        //        Mubox.Configuration.MuboxConfigSection.Default.EnableMulticast = !Mubox.Configuration.MuboxConfigSection.Default.EnableMulticast;
        //        //        Mubox.Configuration.MuboxConfigSection.Default.Save();
        //        //    };
        //        //    menuItem.Header = "Enable Multicast";
        //        //    menuItem.ToolTip = "'Keyboard Multicast' replicates your Key Presses to all Clients.";
        //        //    quickLaunchMenuItems.Add(menuItem);

        //        //    // "Enable Mouse Capture"
        //        //    quickLaunchMenuItems.Add(new Separator());
        //        //    menuItem = new MenuItem();
        //        //    menuItem.IsCheckable = true;
        //        //    menuItem.IsChecked = Mubox.Configuration.MuboxConfigSection.Default.EnableMouseCapture;
        //        //    menuItem.Click += (sender, e) =>
        //        //    {
        //        //        Mubox.Configuration.MuboxConfigSection.Default.EnableMouseCapture = !Mubox.Configuration.MuboxConfigSection.Default.EnableMouseCapture;
        //        //        Mubox.Configuration.MuboxConfigSection.Default.Save();
        //        //    };
        //        //    menuItem.Header = "Enable Mouse Capture";
        //        //    menuItem.ToolTip = "Disable Mouse Capture if you use a third-party application for the Mouse.";
        //        //    quickLaunchMenuItems.Add(menuItem);

        //        //    if (Mubox.Configuration.MuboxConfigSection.Default.EnableMouseCapture)
        //        //    {
        //        //        {
        //        //            // "Mouse Clone" Menu
        //        //            List<MenuItem> mouseCloneModeMenu = new List<MenuItem>();

        //        //            // "Disabled"
        //        //            menuItem = new MenuItem();
        //        //            menuItem.IsCheckable = true;
        //        //            menuItem.IsChecked = Mubox.Configuration.MuboxConfigSection.Default.MouseCloneMode == MouseCloneModeType.Disabled;
        //        //            menuItem.Click += (sender, e) =>
        //        //            {
        //        //                Mubox.Configuration.MuboxConfigSection.Default.MouseCloneMode = MouseCloneModeType.Disabled;
        //        //                Mubox.Configuration.MuboxConfigSection.Default.Save();
        //        //            };
        //        //            menuItem.Header = "Disabled";
        //        //            menuItem.ToolTip = "Use this option to Disable the Mouse Clone feature.";
        //        //            mouseCloneModeMenu.Add(menuItem);

        //        //            // "Toggled"
        //        //            menuItem = new MenuItem();
        //        //            menuItem.IsCheckable = true;
        //        //            menuItem.IsChecked = (Mubox.Configuration.MuboxConfigSection.Default.MouseCloneMode == Mubox.Model.MouseCloneModeType.Toggled);
        //        //            menuItem.Click += (sender, e) =>
        //        //            {
        //        //                Mubox.Configuration.MuboxConfigSection.Default.MouseCloneMode = Mubox.Model.MouseCloneModeType.Toggled;
        //        //                Mubox.Configuration.MuboxConfigSection.Default.Save();
        //        //            };
        //        //            menuItem.Header = "Toggled";
        //        //            menuItem.ToolTip = "Mouse Clone is Active while CAPS LOCK is ON, and Inactive while CAPS LOCK is OFF.";
        //        //            mouseCloneModeMenu.Add(menuItem);

        //        //            // "Pressed"
        //        //            menuItem = new MenuItem();
        //        //            menuItem.IsCheckable = true;
        //        //            menuItem.IsChecked = (Mubox.Configuration.MuboxConfigSection.Default.MouseCloneMode == MouseCloneModeType.Pressed);
        //        //            menuItem.Click += (sender, e) =>
        //        //            {
        //        //                Mubox.Configuration.MuboxConfigSection.Default.MouseCloneMode = Mubox.Model.MouseCloneModeType.Pressed;
        //        //                Mubox.Configuration.MuboxConfigSection.Default.Save();
        //        //            };
        //        //            menuItem.Header = "Pressed";
        //        //            menuItem.ToolTip = "Mouse Clone is Active while CAPS LOCK Key is pressed, and Inactive while CAPS LOCK Key is released.";
        //        //            mouseCloneModeMenu.Add(menuItem);

        //        //            menuItem = new MenuItem();
        //        //            menuItem.Header = "Mouse Clone";
        //        //            menuItem.ItemsSource = mouseCloneModeMenu;
        //        //            quickLaunchMenuItems.Add(menuItem);
        //        //        }
        //        //        {
        //        //            // "Mouse Buffer" Option
        //        //            List<MenuItem> mouseClickBufferMenu = new List<MenuItem>();

        //        //            foreach (double time in new double[] { 0.0, 100.0, 150.0, 200.0, 250.0, 500.0, 750.0, 1000.0 })
        //        //            {
        //        //                // "Disabled"
        //        //                CreateMouseBufferMenuItem(menuItem, mouseClickBufferMenu, time);
        //        //            }

        //        //            menuItem = new MenuItem();
        //        //            menuItem.Header = "Mouse Buffer";
        //        //            menuItem.ToolTip = "Mouse Buffer prevents Mouse Movement from interrupting a Click gesture.";
        //        //            menuItem.ItemsSource = mouseClickBufferMenu;
        //        //            quickLaunchMenuItems.Add(menuItem);
        //        //        }
        //        //    }
        //        //}

        //        //quickLaunchMenuItems.Add(new Separator());

        //        //// "Auto-Start Server"
        //        //menuItem = new MenuItem();
        //        //menuItem.IsCheckable = true;
        //        //menuItem.IsChecked = Mubox.Configuration.MuboxConfigSection.Default.AutoStartServer;
        //        //menuItem.Click += (sender, e) =>
        //        //{
        //        //    Mubox.Configuration.MuboxConfigSection.Default.AutoStartServer = !Mubox.Configuration.MuboxConfigSection.Default.AutoStartServer;
        //        //    Mubox.Configuration.MuboxConfigSection.Default.Save();
        //        //};
        //        //menuItem.Header = "Auto Start Server";
        //        //quickLaunchMenuItems.Add(menuItem);
        //        //}

        //        //// Show Help
        //        //quickLaunchMenuItems.Add(new Separator());
        //        //menuItem = new MenuItem();
        //        //menuItem.Click += (sender, e) =>
        //        //{
        //        //    helpCallback();
        //        //};
        //        //menuItem.Icon = Resources["imageMenuHelpIcon"];
        //        //menuItem.Header = "Help...";
        //        //quickLaunchMenuItems.Add(menuItem);

        //        // Exit QuickLaunch Application
        //        //menuItem = new MenuItem();
        //        //menuItem.Click += (sender, e) =>
        //        //{
        //        //    exitApplicationCallback();
        //        //    foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
        //        //    {
        //        //        try
        //        //        {
        //        //            window.Close();
        //        //        }
        //        //        catch (Exception ex)
        //        //        {
        //        //            //Debug.WriteLine(ex.Message);
        //        //            //Debug.WriteLine(ex.StackTrace);
        //        //        }
        //        //    }
        //        //    //Mubox.View.Server.ServerWindow.Instance = null;
        //        //    exitApplicationCallback();
        //        //};
        //        //menuItem.Header = "E_xit";

        //        //quickLaunchMenuItems.Add(menuItem);
        //        Placement = System.Windows.Controls.Primitives.PlacementMode.AbsolutePoint;
        //        VerticalOffset = mousePosition.Y - 2;
        //        HorizontalOffset = mousePosition.X - 8;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Debug.WriteLine(ex.Message);
        //        //Debug.WriteLine(ex.StackTrace);
        //    }
        //}

    }
}
