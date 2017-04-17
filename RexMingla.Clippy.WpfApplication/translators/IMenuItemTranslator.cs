using System.Windows.Controls;
using RexMingla.DataModel;

namespace RexMingla.Clippy.WpfApplication
{
    public interface IMenuItemTranslator
    {
        MenuItem ToMenuItem(ClipboardContent content);
    }
}