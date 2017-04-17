using Castle.Windsor;
using System.Windows;

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
            _container.Install(new Config.WindsorInstaller(WpfApplication.Properties.Settings.Default.SettingsFile));

            var splash = _container.Resolve<ClippyMenu>();
            splash.Show();

            var orchestrator = _container.Resolve<IClipboardOrchestrator>();
            orchestrator.Start();
            _container.Release(orchestrator);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var orchestrator = _container.Resolve<IClipboardOrchestrator>();
            orchestrator.Stop();
        }

    }
}
