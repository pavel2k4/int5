namespace Interface5.Models
{
    public class GameModel
    {
        private readonly int _size;
        private readonly char[,] _board;
        private char _currentPlayer;

        public char[,] Board => _board;
        public char CurrentPlayer => _currentPlayer;
        public int Size => _size;


        public GameModel(int size)
        {
            if (size < 10 || size > 20)
                throw new ArgumentOutOfRangeException(nameof(size), "Размер поля должен быть от 10 до 20.");

            _size = size;
            _board = new char[_size, _size];
            _currentPlayer = 'X'; 
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

    }

}
