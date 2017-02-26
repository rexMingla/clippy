using Common.Logging;
using System.Collections.Generic;
using System.Reflection;

namespace RexMingla.ClipboardManager
{
    public sealed class ClipboardStore : IClipboardStore
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly List<ClipboardContent> _data;

        public ClipboardStore()
        {
            _data = new List<ClipboardContent>();
        }

        public void SetItems(List<ClipboardContent> contents)
        {
            _data.Clear();
            contents.ForEach(InsertItem);
        }

        public void InsertItem(ClipboardContent content)
        {
            var sanitizedContent = content;
            sanitizedContent.Data.RemoveAll(d => d.Content as System.IO.MemoryStream != null); // remove for purposes of serialization
            _log.Info($"Adding item {sanitizedContent}. item count {_data.Count}");
            if (!sanitizedContent.HasData())
            {
                _log.Warn($"No data found. Item {content} ignored.");
                return;
            }
            _data.Remove(sanitizedContent);
            _data.Insert(0, sanitizedContent);
        }

        public List<ClipboardContent> GetItems()
        {
            return _data;
        }
    }
}
