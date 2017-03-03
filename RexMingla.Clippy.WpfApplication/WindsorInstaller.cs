using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Hardcodet.Wpf.TaskbarNotification;
using RexMingla.Clippy.WpfApplication.translators;
using System.Windows;

namespace RexMingla.Clippy.WpfApplication
{
    public sealed class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                // TODO: change to use windsor factory..
                Component.For<ITranslator>().ImplementedBy<TextTranslator>().Named("textTranslator"),
                Component.For<ITranslator>().ImplementedBy<FileTranslator>().Named("fileTranslator"),
                Component.For<ITranslator>().ImplementedBy<ImageTranslator>().Named("imageTranslator"),
                Component.For<IMenuItemTranslator>().ImplementedBy<MenuItemTranslator>().Named("menuItemTranslator")
                    .DependsOn(
                        Dependency.OnComponentCollection("translators", "textTranslator", "fileTranslator", "imageTranslator")
                    ),
                Component.For<Splash>().Named("splash2")
                    .DependsOn(
                        Dependency.OnComponent("translator", "menuItemTranslator")
                    ).LifestyleSingleton(),
                Component.For<ClippyMenu>().Named("splash").LifestyleSingleton(),
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
