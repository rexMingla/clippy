﻿namespace RexMingla.Clippy.WpfApplication
{
    public class Shell : IShell
    {
        private readonly MainWindow _window;

        public Shell(MainWindow window)
        {
            _window = window;
        }

        public void Run()
        {
            _window.Show();
        }
    }
}