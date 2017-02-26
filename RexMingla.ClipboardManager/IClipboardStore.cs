using System;
using System.Collections.Generic;

namespace RexMingla.ClipboardManager
{
    public interface IClipboardStore
    {
        void SetItems(List<ClipboardContent> contents);
        void InsertItem(ClipboardContent content);
        List<ClipboardContent> GetItems();
    }
}
