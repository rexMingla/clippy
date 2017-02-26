namespace RexMingla.WindowManager
{
    public sealed class KeySender : IKeySender
    {
        public void SendPasteCommand(WindowProperties properties)
        {
            // it would be nice if this wasn't hard coded :(
            WinApi.PostMessage(properties.Handle, WinApi.WM_KEYDOWN, WinApi.VK_V, WinApi.VK_CONTROL);
        }
    }
}
