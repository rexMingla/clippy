namespace RexMingla.GlobalHotKey
{
    public interface IHotKeyManager
    {
        void Register(HotKey key);
        void Unregister(HotKey key);
    }
}