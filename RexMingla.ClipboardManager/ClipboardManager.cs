using System.Windows.Forms;

namespace RexMingla.ClipboardManager
{
    public sealed class ClipboardManager : IClipboardManager
    {
        public ClipboardContent GetClipboardContent()
        {
            return Clipboard.GetDataObject().ToClipboardContent();
        }

        public void SetClipboardContent(ClipboardContent content)
        {
            Clipboard.SetDataObject(content.ToIDataObject());
        }
    }
}
