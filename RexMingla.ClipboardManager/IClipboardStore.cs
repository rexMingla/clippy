using System;
using System.Collections.Generic;

namespace RexMingla.ClipboardManager
{
    public interface IClipboardStore
    {
        void InsertItem(ClipboardContent content);
        List<ClipboardContent> GetItems();
    }
}
