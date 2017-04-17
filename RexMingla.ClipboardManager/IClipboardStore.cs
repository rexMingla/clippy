using RexMingla.DataModel;
using System.Collections.Generic;

namespace RexMingla.ClipboardManager
{
    public interface IClipboardStore
    {
        void SetItems(List<ClipboardContent> contents);
        void InsertItem(ClipboardContent content);
        void ClearItems();
        List<ClipboardContent> GetItems();
    }
}
