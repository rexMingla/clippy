using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RexMingla.Clippy.WpfApplication.translators;

namespace RexMingla.Clippy.WpfApplication
{
    public sealed class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Register(
                Classes.FromAssemblyContaining<ITranslator>().BasedOn<ITranslator>().WithService.FromInterface(),
                Component.For<IMenuItemTranslator>().ImplementedBy<MenuItemTranslator>(),
                Component.For<PreferencesWindow>().Named("preferencesWindow").LifestyleSingleton(),
                Component.For<ClippyMenu>().Named("splash").LifestyleSingleton(),
                Component.For<IClipboardOrchestrator>().ImplementedBy<ClipboardOrchestrator>()
            );
        }
    }
}
