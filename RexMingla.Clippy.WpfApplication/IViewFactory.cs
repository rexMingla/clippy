using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexMingla.Clippy.WpfApplication
{
    interface IViewFactory
    {
        T CreateView<T>() where T : IView;
        T CreateView<T>(object argumentsAsAnonymousType) where T : IView;
    }

    public interface IView
    {
        bool? ShowDialog();
    }
}
