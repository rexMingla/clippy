using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RexMingla.Clippy.WpfApplication.translators;

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
                Component.For<PreferencesWindow>().Named("preferencesWindow").LifestyleSingleton(),
                Component.For<ClippyMenu>().Named("splash").LifestyleSingleton(),
                Component.For<IClipboardOrchestrator>().ImplementedBy<ClipboardOrchestrator>()
            );
        }
    }
}
