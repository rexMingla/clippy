using Castle.Windsor;
using System;
using System.Windows.Input;

namespace RexMingla.GlobalHotKey.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(new WindsorInstaller());

            var manager = container.Resolve<IHotKeyManager>();
            manager.Register(new Hotkey(1, ModifierKeys.Control | ModifierKeys.Shift, Key.C, OnCtrlShiftC));
            manager.Register(new Hotkey(2, ModifierKeys.Control, Key.B, OnCtrlShiftB));

            while (true)
            {
            }
        }

        private static void OnCtrlShiftC()
        {
            System.Console.WriteLine("OnCtrlShiftC");
        }

        private static void OnCtrlShiftB()
        {
            System.Console.WriteLine("OnCtrlShiftB");
        }
    }
}
