using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace RexMingla.Clippy.WpfApplication
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<MainWindow>().Named("window").LifestyleSingleton(),
                Component.For<IShell>().ImplementedBy<Shell>().LifestyleTransient()
            );
        }
    }
}
