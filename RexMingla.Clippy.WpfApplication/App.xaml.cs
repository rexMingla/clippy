using Castle.Windsor;
using System.Windows;
using System.Windows.Input;

namespace RexMingla.Clippy.WpfApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IWindsorContainer _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _container = new WindsorContainer();
            _container.Install(new WindsorInstaller());
            _container.Install(new GlobalHotKey.WindsorInstaller());

            var shell = _container.Resolve<IShell>();
            shell.Run();
            var hotKeyManager = _container.Resolve<GlobalHotKey.IHotKeyManager>();
            var hotKey = new GlobalHotKey.HotKey(ModifierKeys.Control | ModifierKeys.Shift, Key.A, OnKeyPress);
            hotKeyManager.Register(hotKey);
            hotKeyManager.Register(new GlobalHotKey.HotKey(ModifierKeys.Control | ModifierKeys.Shift, Key.B, OnKeyPress));
            hotKeyManager.Unregister(hotKey);
            hotKeyManager.Register(new GlobalHotKey.HotKey(ModifierKeys.Control | ModifierKeys.Shift, Key.C, OnKeyPress));
            
            _container.Release(shell);
        }

        private static void OnKeyPress()
        {
            System.Console.WriteLine("OnKeyPress()");
        }
    }
}
