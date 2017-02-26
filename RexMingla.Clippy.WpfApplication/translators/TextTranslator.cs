using RexMingla.ClipboardManager;
using System.Linq;
using System.Windows.Controls;
using System;

namespace RexMingla.Clippy.WpfApplication.translators
{
    public class TextTranslator : ITranslator
    {
        string ITranslator.PreferredFormat
        {
            get
            {
                return "Text";
            }
        }

        public MenuItem CreateMenuItem(ClipboardData data, ClipboardContent content)
        {
            var text = data.Content as string;
            if (string.IsNullOrWhiteSpace(text))
            {
                return new MenuItem
                {
                    Header = "[Text] white space only",
                    DataContext = content
                };
            }
            var trimmedText = text.Trim();
            return new MenuItem
            {
                Header = trimmedText.Length > 100 ? $"{trimmedText.Substring(0, 97)}..." : trimmedText,
                DataContext = content
            };
        }
    }
}
