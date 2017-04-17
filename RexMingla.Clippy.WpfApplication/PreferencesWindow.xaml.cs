using RexMingla.Clippy.Config;
using System.Windows;
using System.ComponentModel;

namespace RexMingla.Clippy.WpfApplication
{
    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window, ISettingsListener
    {
        private readonly IConfigManager _configManager;
        private SettingsModel _settingsModel;

        public PreferencesWindow(IConfigManager configManager)
        {
            _configManager = configManager;
            _settingsModel = new SettingsModel();
            _configManager.RegisterSettingsListener(this);

            InitializeComponent();

            DataContext = _settingsModel;
        }

        public void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        public void OnSettingsChanged(Settings settings)
        {
            _settingsModel.Settings = settings;
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OnOk(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            _configManager.SetConfigSettings(_settingsModel.Settings);
        }

        public class SettingsModel : INotifyPropertyChanged
        {
            public Settings Settings;

            public event PropertyChangedEventHandler PropertyChanged;

            public int MaxDisplayedItems
            {
                get
                {
                    return Settings.MaxDisplayedItems;
                }
                set
                {
                    Settings.MaxDisplayedItems = value;
                    OnPropertyChanged("MaxDisplayedItems");
                }
            }

            public int ItemsPerMainGroup
            {
                get
                {
                    return Settings.ItemsPerMainGroup;
                }
                set
                {
                    Settings.ItemsPerMainGroup = value;
                    OnPropertyChanged("ItemsPerMainGroup");
                }
            }

            public int ItemsPerGroup
            {
                get
                {
                    return Settings.ItemsPerGroup;
                }
                set
                {
                    Settings.ItemsPerGroup = value;
                    OnPropertyChanged("ItemsPerGroup");
                }
            }

            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
