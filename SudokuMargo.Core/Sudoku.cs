namespace SudokuMargo.Core
{
    public class Sudoku
    {
        public const int Size = 9; // Standard Sudoku size
        readonly int[,] _board = new int[Size, Size];
        object _lock = new object();
        int player1Score = 0;
        int player2Score = 0;



        public int[,] Board
        {
            get
            {
                lock (_lock)
                {
                    return (int[,])_board.Clone(); // Return a copy to prevent external modification
                }
            }
        }

        public void InitializeBoard()
        {
            lock (_lock)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        _board[i, j] = 0; // Initialize with zeros
                    }
                }
            }
        }

        public int getScoreForPlayer(SudokuPlayerEnum sudokuPlayerEnum)
        {
            lock (_lock)
            {
                switch (sudokuPlayerEnum)
                {
                    case SudokuPlayerEnum.Player1: return player1Score;
                    case SudokuPlayerEnum.Player2: return player2Score;
                    default: throw new ArgumentException("Invalid player enum value.");
                }
            }
        }

        public void SetCell(int row, int col, int value, SudokuPlayerEnum player)
        {
            lock (_lock)
            {
                if (row < 0 || row >= Size || col < 0 || col >= Size || value < 0 || value > 9)
                {
                    throw new ArgumentOutOfRangeException("Invalid cell or value.");
                }
                if (IsValidMove(row, col, value))
                {
                    _board[row, col] = value;
                    switch (player)
                    {
                        case SudokuPlayerEnum.Player1:
                            player1Score++;
                            break;
                        case SudokuPlayerEnum.Player2:
                            player2Score++;
                            break;
                    }
                }
            }
        }

        public bool IsValidMove(int row, int col, int value)
        {
            lock (_lock)
            {
                // Check if the value is already in the row
                for (int j = 0; j < Size; j++)
                {
                    if (_board[row, j] == value) return false;
                }
                // Check if the value is already in the column
                for (int i = 0; i < Size; i++)
                {
                    if (_board[i, col] == value) return false;
                }
                // Check if the value is already in the 3x3 subgrid
                int startRow = (row / 3) * 3;
                int startCol = (col / 3) * 3;
                for (int i = startRow; i < startRow + 3; i++)
                {
                    for (int j = startCol; j < startCol + 3; j++)
                    {
                        if (_board[i, j] == value) return false;
                    }
                }
                return true; // Valid move
            }
        }

        public bool IsBoardFull()
        {
            lock (_lock)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (_board[i, j] == 0) return false; // Found an empty cell
                    }
                }
                return true; // No empty cells found
            }
        }

        public void ResetBoard()
        {
            lock (_lock)
            {
                InitializeBoard();
                player1Score = 0;
                player2Score = 0;
            }
        }
    }
}
