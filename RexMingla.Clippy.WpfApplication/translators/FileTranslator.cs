using RexMingla.DataModel;
using System.Linq;
using System.Windows.Controls;

namespace RexMingla.Clippy.WpfApplication.translators
{
    public class FileTranslator : ITranslator
    {
        string ITranslator.PreferredFormat
        {
            get
            {
                return "FileDrop";
            }
        }

        public MenuItem CreateMenuItem(ClipboardData data, ClipboardContent content)
        {
            var names = data.Content as string[];
            if (names.Count() == 1)
            {
                var name = names.First();
                return new MenuItem
                {
                    Header = name.Length > 100 ? $"[1 File] ...{name.Substring(name.Length - 97, 97)}" : $"[1 File] {name}",
                    DataContext = content
                };
            }
            return new MenuItem
            {
                Header = $"[{names.Count()} Files]",
                DataContext = content
            };
        }
    }
}
