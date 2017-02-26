using Common.Logging;
using System.Collections.Generic;
using System.Reflection;

namespace RexMingla.ClipboardManager
{
    public sealed class ClipboardStore : IClipboardStore
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly List<ClipboardContent> _data;

        public ClipboardStore(int maxSize = 100)
        {
            _data = new List<ClipboardContent>(maxSize);
        }

        public void SetItems(List<ClipboardContent> contents)
        {
            if (contents == null)
            {
                return;
            }
            _data.Clear();
            contents.ForEach(InsertItem);
        }

        public void InsertItem(ClipboardContent content)
        {
            _log.Info($"Adding item {content}. item count {_data.Count}");
            if (!content.HasData())
            {
                _log.Warn($"No data found. Item {content} ignored.");
                return;
            }
            _data.Remove(content);
            if (_data.Capacity == _data.Count)
            {
                _log.Warn($"At capacity {_data.Capacity} items, removing last item");
                _data.RemoveAt(_data.Count - 1);
            }
            _data.Insert(0, content);
        }

        public List<ClipboardContent> GetItems()
        {
            return _data;
        }
    }
}
