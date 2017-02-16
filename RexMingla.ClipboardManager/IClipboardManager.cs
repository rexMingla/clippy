namespace RexMingla.ClipboardManager
{
    public interface IClipboardManager
    {
        ClipboardContent GetClipboardContent();
        void SetClipboardContent(ClipboardContent content);
    }
}
