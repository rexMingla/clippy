using Common.Logging;
using Newtonsoft.Json;
using RexMingla.DataModel;
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

        private readonly List<ISettingsListener> _settingsListeners = new List<ISettingsListener>();
        private readonly List<IClipboardHistoryListener> _historyListeners = new List<IClipboardHistoryListener>();

        public ConfigManager(string configFile, params JsonConverter[] converters)
        {
            _configFile = configFile;
            _converters = converters;
            _config = Config.DefaultConfig;
        }

        public void SaveConfig()
        {
            try
            {
                _log.Debug($"Saving to config file. {_config.ClipboardHistory.Count}(s) history items");
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
                SetConfig(Config.DefaultConfig);
            }
        }

        public void SetClipboardHistory(List<ClipboardContent> content)
        {
            if (_config.ClipboardHistory != content)
            {
                _config.ClipboardHistory = content;
                _historyListeners.ForEach(l => l.OnClipboardHistoryChanged(content));
            }
        }

        public void SetConfigSettings(Settings settings)
        {
            var sanitizedSettings = SanitizeSettings(settings);
            if (_config.Settings != sanitizedSettings)
            {
                _config.Settings = sanitizedSettings;
                _settingsListeners.ForEach(l => l.OnSettingsChanged(settings));
            }
        }

        private Settings SanitizeSettings(Settings settings)
        {
            if (settings == null)
            {
                return Config.DefaultConfig.Settings;
            }
            return new Settings
            {
                ItemsPerGroup = Math.Max(5, Math.Min(100, settings.ItemsPerGroup)),
                ItemsPerMainGroup = Math.Max(0, Math.Min(100, settings.ItemsPerMainGroup)),
                MaxDisplayedItems = Math.Max(10, Math.Min(100, settings.MaxDisplayedItems))
            };
        }

        private void SetConfig(Config config) {
            if (config == null)
            {
                return;
            }
            SetConfigSettings(config.Settings);
            SetClipboardHistory(config.ClipboardHistory);
        }

        public void RegisterSettingsListener(ISettingsListener listener)
        {
            _settingsListeners.Add(listener);
            listener.OnSettingsChanged(_config.Settings);
        }

        public void RegisterClipboardHistoryListener(IClipboardHistoryListener listener)
        {
            _historyListeners.Add(listener);
            listener.OnClipboardHistoryChanged(_config.ClipboardHistory);
        }
    }
}
