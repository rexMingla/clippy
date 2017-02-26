using RexMingla.ClipboardManager;
using System.Collections.Generic;

namespace RexMingla.Clippy.Config
{
    public delegate void OnClipboardHistoryChangedHandler(List<ClipboardContent> content);
    public delegate void OnConfigSettingsChangedHandler(Settings settings);

    public interface IConfigManager
    {
        event OnClipboardHistoryChangedHandler OnClipboardHistoryChanged;
        event OnConfigSettingsChangedHandler OnClipboardSettingsChanged;

        void SetClipboardHistory(List<ClipboardContent> content);
        void SetConfigSettings(Settings settings);

        void LoadConfig();
        void SaveConfig();
    }
}