using System.Windows.Forms;

namespace RexMingla.ClipboardManager
{
    public sealed class ClipboardManager : IClipboardManager
    {
        // TODO: this doesn't work..
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
