using System.Windows;

namespace RexMingla.Clippy.WpfApplication
{
    public sealed class Shell : IShell
    {
        private readonly Window _window;

        public Shell(Window window)
        {
            _window = window;
        }

        public void Run()
        {
            _window.Show();
        }
    }
}
