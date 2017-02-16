namespace RexMingla.GlobalHotKey
{
    public interface IHotKeyManager
    {
        void Register(Hotkey key);
        void Unregister(Hotkey key);
    }
}