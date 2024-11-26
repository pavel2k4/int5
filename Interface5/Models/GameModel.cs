using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Interface5.Models
{
    public class GameModel : INotifyPropertyChanged
    {
        private readonly int _size;
        private readonly char[,] _board;
        private char _currentPlayer;
        private readonly DispatcherTimer _timer1;
        private int _timeLeft1;
        private int _timeLeft2;
        private const int TurnTimeLimit = 60;

        public event PropertyChangedEventHandler? PropertyChanged;

        public char[,] Board => _board;
        public char CurrentPlayer => _currentPlayer;
        public int Size => _size;
        public int TimeLeft
        {
            get => _timeLeft1;
            private set
            {
                if (_timeLeft1 != value)
                {
                    _timeLeft1 = value;
                    OnPropertyChanged(nameof(TimeLeft));
                }
            }
        }

        public int TimeLeft2
        {
            get => _timeLeft2;
            private set
            {
                if (_timeLeft2 != value)
                {
                    _timeLeft2 = value;
                    OnPropertyChanged(nameof(TimeLeft2));
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public GameModel(int size)
        {
            _timer1 = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer1.Tick += OnTimerTick;
            if (size < 10 || size > 20)
                throw new ArgumentOutOfRangeException(nameof(size), "Размер поля должен быть от 10 до 20.");

            _size = size;
            _board = new char[_size, _size];
            _currentPlayer = 'X'; 
            StartNewTurn();
        }

        public bool MakeMove(int x, int y)
        {
            if (x < 0 || x >= _size || y < 0 || y >= _size)
                throw new ArgumentOutOfRangeException("Координаты выходят за пределы поля.");

            if (_board[x, y] != '\0') 
                return false;

            _board[x, y] = _currentPlayer;
            _currentPlayer = _currentPlayer == 'X' ? 'O' : 'X';
            return true;
        }

        public char CheckWinner()
        {
            for (int x = 0; x < _size; x++)
            {
                for (int y = 0; y < _size; y++)
                {
                    if (_board[x, y] != '\0' && (CheckLine(x, y, 1, 0) || CheckLine(x, y, 0, 1) ||
                                                 CheckLine(x, y, 1, 1) || CheckLine(x, y, 1, -1)))
                        return _board[x, y];
                }
            }

            return '\0';
        }

        private bool CheckLine(int startX, int startY, int dx, int dy)
        {
            char symbol = _board[startX, startY];
            int count = 0;

            for (int i = 0; i < 5; i++)
            {
                int x = startX + i * dx;
                int y = startY + i * dy;

                if (x < 0 || x >= _size || y < 0 || y >= _size || _board[x, y] != symbol)
                    return false;

                count++;
            }

            return count == 5;
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            if (_currentPlayer == 'X')
            {
                TimeLeft--;
                if (TimeLeft <= 0)
                {
                    TimerEneded('O'); 
                }
            }
            else
            {
                TimeLeft2--;
                if (TimeLeft2 <= 0)
                {
                    TimerEneded('X'); 
                }
            }
        }

        public void StartNewTurn()
        {
            _timeLeft1 = _timeLeft2 = TurnTimeLimit;
            _timer1.Start();
        }

        public void TimerEneded(char c) 
        {
            _timer1.Stop();
            MessageBox.Show($"игрок {_currentPlayer} проиграл, время вышло!");
            Application.Current.Shutdown();
        }

        

    }

}
