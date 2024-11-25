using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Interface5.Models;

namespace Interface5.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private Settings _settings;

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel(Settings settings)
        {
            _settings = settings;
        }

        public int Size
        {
            get => _settings.Size;
            set
            {
                if (_settings.Size != value || _settings.Size < 10 || _settings.Size > 20)
                {
                    _settings.Size = value;
                    OnPropertyChanged();
                }
                else
                {
                    MessageBox.Show("Error");
                    _settings.Size = 10;
                    OnPropertyChanged();
                }
                
            }
        }

        public string PathStats
        {
            get => _settings.PathStats;
            set
            {
                if (_settings.PathStats != value)
                {
                    _settings.PathStats = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
