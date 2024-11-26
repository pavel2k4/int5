using Interface5.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Interface5.Views
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private readonly GameModel _gameModel;
        
        public GameWindow(int size)
        {
            InitializeComponent();
            _gameModel = new GameModel(size);
            InitializeGameGrid(size);
            DataContext = _gameModel;
            
        }

        private void InitializeGameGrid(int size)
        {
            for (int i = 0; i < size; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var button = new Button
                    {
                        Tag = (i, j),
                        Margin = new Thickness(1)
                    };

                    button.Click += OnCellClick;

                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);

                    GameGrid.Children.Add(button);
                }
            }
        }

        private void OnCellClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is (int x, int y))
            {
                if (_gameModel.MakeMove(x, y))
                {
                    button.Content = _gameModel.Board[x, y];
                    var winner = _gameModel.CheckWinner();
                    if (winner != '\0')
                    {
                        MessageBox.Show($"Победитель: {winner}!", "Игра окончена");
                        Close();
                    }
                }
            }
            
        }

    }
}
