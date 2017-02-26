using Common.Logging;
using Newtonsoft.Json;
using RexMingla.ClipboardManager;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RexMingla.Clippy.Config
{
    public sealed class ConfigManager : IConfigManager
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _configFile;

        public Config Config { get; set; }

        public ConfigManager(string configFile)
        {
            _configFile = configFile;
            Config = Config.DefaultConfig;
        }

        public void SaveConfig()
        {
            _log.Debug($"Saving to config file {_configFile}");
            try
            {
                var json = JsonConvert.SerializeObject(Config, Formatting.Indented, new ImageConverter());
                File.WriteAllText(_configFile, json);
            }
            catch (Exception ex)
            {
                _log.Warn($"An error occurred saving config", ex);
            }
        }

        public void LoadConfig()
        {
            _log.Debug($"Loading from config file {_configFile}");
            try
            {
                var json = File.ReadAllText(_configFile);
                Config = JsonConvert.DeserializeObject<Config>(json);
                Config.RecentlyUsed.ForEach(UpdateBitmapFromBase64);
            }
            catch (Exception ex)
            {
                _log.Warn($"An error occurred loading config", ex);
            }
        }

        private void UpdateBitmapFromBase64(ClipboardContent content)
        {
            var bitmap = content.Data.FirstOrDefault(d => d.DataFormat == "Bitmap");
            if (bitmap == null)
            {
                return;
            }
            var m = new MemoryStream(Convert.FromBase64String((string)bitmap.Content));
            bitmap.Content = Bitmap.FromStream(m);
        }

    }
}
