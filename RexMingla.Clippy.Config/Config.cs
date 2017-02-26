using RexMingla.ClipboardManager;
using System.Collections.Generic;

namespace RexMingla.Clippy.Config
{
    public class Config
    {
        public Metadata Metadata;
        public List<ClipboardContent> RecentlyUsed;

        public static Config DefaultConfig = new Config
        {
            Metadata = new Metadata
            {
                MaxDisplayedItems = 100,
                ItemsPerGroup = 10
            },
            RecentlyUsed = new List<ClipboardContent>()
        };
    }

    public class Metadata
    {
        public int MaxDisplayedItems;
        public int ItemsPerGroup;
    }
}
