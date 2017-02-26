using Castle.MicroKernel.Registration;
using System;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace RexMingla.Clippy.Config
{
    public class WindsorInstaller : IWindsorInstaller
    {
        private readonly string _configFile;

        public WindsorInstaller(string configFile)
        {
            _configFile = configFile;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IConfigManager>().ImplementedBy<ConfigManager>().DependsOn(
                    Dependency.OnConfigValue("configFile", _configFile)
                ).LifestyleTransient());
        }
    }
}
