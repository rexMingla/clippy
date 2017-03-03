using System.Windows.Controls;
using RexMingla.ClipboardManager;

namespace RexMingla.Clippy.WpfApplication
{
    public interface IMenuItemTranslator
    {
        MenuItem ToMenuItem(ClipboardContent content);
    }
}