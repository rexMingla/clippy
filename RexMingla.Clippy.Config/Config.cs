using RexMingla.DataModel;
using System.Collections.Generic;

namespace RexMingla.Clippy.Config
{
    public class Config
    {
        public Settings Settings { get; set; }
        public List<ClipboardContent> ClipboardHistory { get; set; }

        private static Config _config = DefaultConfig;

        public static Config DefaultConfig = new Config
        {
            Settings = new Settings
            {
                MaxDisplayedItems = 100,
                ItemsPerMainGroup = 3,
                ItemsPerGroup = 4,
            },
            ClipboardHistory = new List<ClipboardContent>()
        };
    }

    public class Settings
    {
        public int MaxDisplayedItems;
        public int ItemsPerMainGroup;
        public int ItemsPerGroup;
    }
}
