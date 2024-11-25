using Interface5.Models;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Interface5.Views
{
    /// <summary>
    /// Interaction logic for StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        private readonly Settings _settings;

        public int NumberOfGames { get; set; }
        public int MaxSteps { get; set; }
        public int MinSteps { get; set; }


        public StatsWindow(Settings settings)
        {
            InitializeComponent();
            _settings = settings;
            DataContext = this;
            DefStats();
        }



        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DefStats()
        {
            string json = File.ReadAllText(_settings.PathStats);
            if (json == null) return;
            var stats = JsonSerializer.Deserialize<Stats>(json);
            NumberOfGames = stats.NumberOfGames;
            MaxSteps = stats.MaxSteps;
            MinSteps = stats.MinSteps;
        }
    }
}
