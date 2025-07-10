using NUnit.Framework;
using System.Text;

namespace SudokuMargo.Core.Tests
{
    /// <summary>
    /// Helper class for printing Sudoku boards in test contexts
    /// </summary>
    public static class TestHelpers
    {
        /// <summary>
        /// Prints the current state of the Sudoku board to TestContext
        /// </summary>
        /// <param name="sudoku">The Sudoku instance to print</param>
        /// <param name="title">Optional title for the board display</param>
        public static void PrintBoard(Sudoku sudoku, string title = "Current Sudoku Board")
        {
            var board = sudoku.Board;
            var boardString = FormatBoard(board, title);
            TestContext.WriteLine(boardString);
        }

        /// <summary>
        /// Prints a 2D array representing a Sudoku board to TestContext
        /// </summary>
        /// <param name="board">The 2D array representing the board</param>
        /// <param name="title">Optional title for the board display</param>
        public static void PrintBoard(int[,] board, string title = "Sudoku Board")
        {
            var boardString = FormatBoard(board, title);
            TestContext.WriteLine(boardString);
        }

        /// <summary>
        /// Formats a Sudoku board as a string with proper formatting
        /// </summary>
        /// <param name="board">The 2D array representing the board</param>
        /// <param name="title">Optional title for the board display</param>
        /// <returns>Formatted string representation of the board</returns>
        public static string FormatBoard(int[,] board, string title = "Sudoku Board")
        {
            var sb = new StringBuilder();
            
            if (!string.IsNullOrEmpty(title))
            {
                sb.AppendLine($"=== {title} ===");
            }

            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                if (i % 3 == 0 && i > 0)
                {
                    sb.AppendLine("---+---+---");
                }

                for (int j = 0; j < cols; j++)
                {
                    if (j % 3 == 0 && j > 0)
                    {
                        sb.Append("|");
                    }

                    int value = board[i, j];
                    string displayValue = value == 0 ? "." : value.ToString();
                    sb.Append($" {displayValue} ");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Prints the current scores for both players to TestContext
        /// </summary>
        /// <param name="sudoku">The Sudoku instance</param>
        public static void PrintScores(Sudoku sudoku)
        {
            int player1Score = sudoku.getScoreForPlayer(SudokuPlayerEnum.Player1);
            int player2Score = sudoku.getScoreForPlayer(SudokuPlayerEnum.Player2);
            
            TestContext.WriteLine("=== Current Scores ===");
            TestContext.WriteLine($"Player 1: {player1Score}");
            TestContext.WriteLine($"Player 2: {player2Score}");
            TestContext.WriteLine("=====================");
        }

        /// <summary>
        /// Prints both the board and scores to TestContext
        /// </summary>
        /// <param name="sudoku">The Sudoku instance</param>
        /// <param name="title">Optional title for the board display</param>
        public static void PrintGameState(Sudoku sudoku, string title = "Current Game State")
        {
            PrintBoard(sudoku, title);
            TestContext.WriteLine();
            PrintScores(sudoku);
        }

        /// <summary>
        /// Prints a move attempt to TestContext
        /// </summary>
        /// <param name="player">The player making the move</param>
        /// <param name="row">Row position</param>
        /// <param name="col">Column position</param>
        /// <param name="value">Value to place</param>
        /// <param name="isValid">Whether the move is valid</param>
        public static void PrintMove(SudokuPlayerEnum player, int row, int col, int value, bool isValid)
        {
            string status = isValid ? "VALID" : "INVALID";
            TestContext.WriteLine($"[{status}] Player {player}: ({row}, {col}) = {value}");
        }

        /// <summary>
        /// Creates a visual representation of the board with highlighted cells
        /// </summary>
        /// <param name="board">The 2D array representing the board</param>
        /// <param name="highlightedCells">List of (row, col) tuples to highlight</param>
        /// <param name="title">Optional title for the board display</param>
        /// <returns>Formatted string with highlighted cells</returns>
        public static string FormatBoardWithHighlights(int[,] board, List<(int row, int col)> highlightedCells, string title = "Sudoku Board with Highlights")
        {
            var sb = new StringBuilder();
            
            if (!string.IsNullOrEmpty(title))
            {
                sb.AppendLine($"=== {title} ===");
            }

            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                if (i % 3 == 0 && i > 0)
                {
                    sb.AppendLine("---+---+---");
                }

                for (int j = 0; j < cols; j++)
                {
                    if (j % 3 == 0 && j > 0)
                    {
                        sb.Append("|");
                    }

                    int value = board[i, j];
                    string displayValue = value == 0 ? "." : value.ToString();
                    
                    bool isHighlighted = highlightedCells.Any(cell => cell.row == i && cell.col == j);
                    string formattedValue = isHighlighted ? $"[{displayValue}]" : $" {displayValue} ";
                    
                    sb.Append(formattedValue);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
} 