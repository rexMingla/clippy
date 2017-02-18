namespace RexMingla.WindowManager
{
    public interface IWindowManager
    {
        WindowProperties GetCurrentWindow();
        void SetCurrentWindow(WindowProperties properties);
    }
}