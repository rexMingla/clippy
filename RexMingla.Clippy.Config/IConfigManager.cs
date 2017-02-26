namespace RexMingla.Clippy.Config
{
    public interface IConfigManager
    {
        Config Config { get; set; }

        void LoadConfig();
        void SaveConfig();
    }
}