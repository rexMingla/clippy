using Common.Logging;
using Newtonsoft.Json;
using RexMingla.ClipboardManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace RexMingla.Clippy.Config
{
    public sealed class ConfigManager : IConfigManager
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _configFile;
        private readonly JsonConverter[] _converters;
        private readonly Config _config;

        public event OnClipboardHistoryChangedHandler OnClipboardHistoryChanged;
        public event OnConfigSettingsChangedHandler OnClipboardSettingsChanged;

        public ConfigManager(string configFile, params JsonConverter[] converters)
        {
            _configFile = configFile;
            _converters = converters;
            _config = Config.DefaultConfig;
        }

        public void SaveConfig()
        {
            _log.Debug($"Saving to config file {_configFile}");
            try
            {
                var json = JsonConvert.SerializeObject(_config, Formatting.Indented, _converters);
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
                var config = JsonConvert.DeserializeObject<Config>(json, _converters);
                SetConfig(config);
            }
            catch (Exception ex)
            {
                _log.Warn($"An error occurred loading config", ex);
            }
        }

        public void SetClipboardHistory(List<ClipboardContent> content)
        {
            if (_config.ClipboardHistory != content)
            {
                _config.ClipboardHistory = content;
                OnClipboardHistoryChanged?.Invoke(content);
            }
        }

        public void SetConfigSettings(Settings settings)
        {
            if (_config.Settings != settings)
            {
                _config.Settings = settings;
                OnClipboardSettingsChanged?.Invoke(settings);
            }
        }

        private void SetConfig(Config config) {
            if (config == null)
            {
                return;
            }
            SetConfigSettings(config.Settings);
            SetClipboardHistory(config.ClipboardHistory);
        }
    }
}
