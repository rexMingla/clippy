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

        public void InsertItem(ClipboardContent content)
        {
            _log.Info($"Adding item {content}. item count {_data.Count}");
            _data.Remove(content);
            _data.Insert(0, content);
        }

        public List<ClipboardContent> GetItems()
        {
            return _data;
        }
    }
}
