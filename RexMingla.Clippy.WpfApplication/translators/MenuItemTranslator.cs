using Common.Logging;
using RexMingla.ClipboardManager;
using RexMingla.Clippy.WpfApplication.translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace RexMingla.Clippy.WpfApplication
{
    public class MenuItemTranslator : IMenuItemTranslator
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public List<ITranslator> _translators;

        public MenuItemTranslator(params ITranslator[] translators)
        {
            _translators = translators.ToList();
        }

        public MenuItem ToMenuItem(ClipboardContent content)
        {
            var translator = _translators.FirstOrDefault(t => content.Data.Any(d => d.DataFormat == t.PreferredFormat));
            if (translator == null)
            {
                var type = content.Data.First().DataFormat;
                _log.Warn($"Clipboard data type {content.Data.First().DataFormat} unknown for {content}.");
                return new MenuItem
                {
                    Header = $"[{type}]",
                    DataContext = content
                };
            }

            var data = content.Data.First(d => d.DataFormat == translator.PreferredFormat);
            try
            {
                return translator.CreateMenuItem(data, content);
            }
            catch (Exception ex)
            { 
                _log.Error($"Error creating menu item of type {data.DataFormat}.", ex);
            }
            return new MenuItem
            {
                Header = $"[{data.DataFormat}]",
                DataContext = content
            };
        }
    }
}
