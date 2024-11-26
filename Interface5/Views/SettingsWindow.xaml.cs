using System.Windows;
using Interface5.Models;
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

            tbSize.Text = _settings.Size.ToString();
            tbPathStats.Text = _settings.PathStats;
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
                _settings.PathStats = fileDialog.FileName;
                tbPathStats.Text = _settings.PathStats; 
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbSize.Text, out int size))
            {
                if (size < 10 || size > 20)
                {
                    MessageBox.Show("Размер поля должен быть от 10 до 20.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbSize.Text = "10";
                    return;
                }

                _settings.Size = size;
            }
            else
            {
                MessageBox.Show("Введите корректное числовое значение для размера поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbSize.Text = "10";
                return;
            }

            _settings.Save();

            MessageBox.Show("Настройки успешно сохранены.", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
