using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RexMingla.Action.action;
using RexMingla.Action.factory;

namespace RexMingla.Action
{
    public sealed class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Register(
                Classes.FromAssemblyContaining<IAction>().BasedOn<IAction>().WithService.FromInterface(),
                Component.For<IActionFactory>().ImplementedBy<ActionFactory>()
            );
        }
    }
}
