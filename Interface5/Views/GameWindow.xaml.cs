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
        private readonly DispatcherTimer _timer1;
        private int _timeLeft1;
        private int _timeLeft2;
        private const int TurnTimeLimit = 60;
        public GameWindow(int size)
        {
            InitializeComponent();
            _gameModel = new GameModel(size);
            InitializeGameGrid(size);

            _timer1 = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer1.Tick += OnTimerTick;

            StartNewTurn();
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
                        _timer1.Stop();
                        Close();
                    }
                }
            }
            
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            if (_gameModel.CurrentPlayer == 'X')
            {
                _timeLeft1--;
                tbTimer1.Text = _timeLeft1.ToString();
            }
            else
            {
                _timeLeft2--;
                tbTimer2.Text = _timeLeft2.ToString();
            }
            

            if (_timeLeft1 <= 0 || _timeLeft2 <= 0)
            {
                _timer1.Stop();
                MessageBox.Show($"Игрок '{_gameModel.CurrentPlayer}' проиграл!");
                _timer1.Stop();
                Close();
            }
        }

        private void StartNewTurn()
        {
            _timeLeft1 = _timeLeft2 = TurnTimeLimit;
            tbTimer1.Text = _timeLeft1.ToString();
            tbTimer2.Text = _timeLeft2.ToString();
            _timer1.Start();
        }

    }
}
