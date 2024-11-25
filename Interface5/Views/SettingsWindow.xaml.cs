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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (SettingsViewModel)DataContext;

            if (viewModel.Size < 10 || viewModel.Size > 20)
            {
                MessageBox.Show("Размер поля должен быть от 10 до 20.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _settings.Size = viewModel.Size;
            _settings.PathStats = viewModel.PathStats;
            _settings.Save();

            MessageBox.Show("Настройки успешно сохранены.", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
