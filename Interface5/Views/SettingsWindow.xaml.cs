using System.Windows;
using Interface5.Models;
using Interface5.ViewModels;
using Microsoft.Win32;

namespace Interface5.Views
{
    public partial class SettingsWindow : Window
    {
        private readonly Settings _settings;

        public SettingsWindow(Settings settings)
        {
            InitializeComponent();
            _settings = settings;
            DataContext = new SettingsViewModel(_settings);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            _settings.Save(); 
            Close();
        }

        private void btnPathStats_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "JSON file | *.json"
            };

            if (fileDialog.ShowDialog() == true)
            {
                ((SettingsViewModel)DataContext).PathStats = fileDialog.FileName;
            }
        }
    }
}
