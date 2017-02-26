using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Windows;

namespace RexMingla.GlobalHotKey
{
    public sealed class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IHotKeyManager>()
                    .ImplementedBy<HotKeyManager>()
                    .DependsOn(
                        Dependency.OnComponent(typeof(Window), "splash")).LifestyleSingleton()
            );
        }
    }
}
