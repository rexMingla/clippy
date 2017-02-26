using RexMingla.ClipboardManager;
using System.Windows.Controls;

namespace RexMingla.Clippy.WpfApplication.translators
{
    public interface ITranslator
    {
        string PreferredFormat { get; }

        MenuItem CreateMenuItem(ClipboardData data, ClipboardContent content);
    }
}
