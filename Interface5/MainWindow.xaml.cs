using System.Windows;
using Interface5.Views;
using Interface5.Models;

namespace Interface5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Settings _settings;
        private SettingsWindow _settingsWindow;
        private StatsWindow _statsWindow;
        private GameWindow _gameWindow;
        public MainWindow()
        {
            InitializeComponent();
            _settings = new Settings();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            _settingsWindow = new SettingsWindow(_settings);
            _settingsWindow.ShowDialog();
        }

        private void btnStats_Click(object sender, RoutedEventArgs e)
        {
            _statsWindow = new StatsWindow(_settings);
            _statsWindow.ShowDialog();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            _gameWindow = new GameWindow(_settings.Size);
            _gameWindow.ShowDialog();
        }
    }
}