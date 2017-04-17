using Castle.Windsor;
using RexMingla.DataModel;
using System.Linq;

namespace RexMingla.ClipboardManager.Console
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(new WindsorInstaller());
            var store = container.Resolve<IClipboardStore>();
            var notifier = container.Resolve<IClipboardNotifier>();
            notifier.OnClipboardChange += OnChange;
            var manager = container.Resolve<IClipboardManager>();

            while (true)
            {
            }
        }

        static void OnChange(ClipboardContent content)
        {
            var item = content.Data.First();
            System.Console.WriteLine($"clipboard changed: {item.DataFormat} data: {item.Content}");
        }
    }
}
