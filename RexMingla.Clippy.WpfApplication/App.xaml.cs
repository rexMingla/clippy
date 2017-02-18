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
            _container.Install(new ClipboardManager.WindsorInstaller());
            _container.Install(new WindowManager.WindsorInstaller());

            var shell = _container.Resolve<IShell>();
            shell.Run();

            var orchestrator = _container.Resolve<IClipboardOrchestrator>();
            orchestrator.Start();

            _container.Release(shell);
            _container.Release(orchestrator);
        }
    }
}
