using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace RexMingla.ClipboardManager
{
    public sealed class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IClipboardManager>().ImplementedBy<ClipboardManager>().LifestyleTransient(),
                Component.For<IClipboardNotifier>().Instance(ClipboardNotifier.GetNotifier()).LifestyleSingleton(),
                Component.For<IClipboardStore>().ImplementedBy<ClipboardStore>().OnCreate(s =>
            {
                ClipboardNotifier.GetNotifier().OnClipboardChange += s.InsertItem;
            }).LifestyleTransient());
        }
    }
}
