using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;

namespace RexMingla.Clippy.WpfApplication
{
    public sealed class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Splash>().Named("splash").LifestyleSingleton(),
                //Component.For<MainWindow>().Named("window").LifestyleSingleton(),
                Component.For<TaskbarIcon>().LifestyleSingleton(),
                Component.For<IClipboardOrchestrator>().ImplementedBy<ClipboardOrchestrator>(),
                Component.For<IShell>()
                    .ImplementedBy<Shell>()
                    .DependsOn(
                        Dependency.OnComponent(typeof(Window), "splash"))
            );
        }
    }
}
