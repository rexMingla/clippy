using RexMingla.ClipboardManager;
using RexMingla.DataModel;
using System.Collections.Generic;

namespace RexMingla.Clippy.Config
{
    public interface ISettingsListener
    {
        void OnSettingsChanged(Settings settings);
    }

    public interface IConfigManager
    {
        void SetClipboardHistory(List<ClipboardContent> content);
        void SetConfigSettings(Settings settings);

        void LoadConfig();
        void SaveConfig();

        void RegisterSettingsListener(ISettingsListener listener);
    }
}