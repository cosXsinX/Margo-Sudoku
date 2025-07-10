namespace SudokuMargo.Core.Tests
{
    public class SudokuTests
    {
        private Sudoku sudoku;

        [SetUp]
        public void Setup()
        {
            sudoku = new Sudoku();
        }

        [Test]
        public void InitializeBoard_ShouldSetAllCellsToZero()
        {
            sudoku.InitializeBoard();
            var board = sudoku.Board;

            // Print the initialized board to TestContext
            TestHelpers.PrintBoard(sudoku, "Initialized Board (All Zeros)");

            for (int i = 0; i < Sudoku.Size; i++)
                for (int j = 0; j < Sudoku.Size; j++)
                    Assert.AreEqual(0, board[i, j]);
        }

        [Test]
        public void SetCell_ValidMove_ShouldUpdateBoardAndScore()
        {
            sudoku.InitializeBoard();
            
            // Print initial state
            TestHelpers.PrintBoard(sudoku, "Before Move");
            
            sudoku.SetCell(0, 0, 5, SudokuPlayerEnum.Player1);
            
            // Print after move
            TestHelpers.PrintGameState(sudoku, "After Valid Move");

            var board = sudoku.Board;
            Assert.AreEqual(5, board[0, 0]);
            Assert.AreEqual(1, sudoku.getScoreForPlayer(SudokuPlayerEnum.Player1));
        }

        [Test]
        public void SetCell_InvalidMove_ShouldNotUpdateBoardOrScore()
        {
            sudoku.InitializeBoard();
            sudoku.SetCell(0, 0, 5, SudokuPlayerEnum.Player1);
            
            // Print state before invalid move
            TestHelpers.PrintBoard(sudoku, "Before Invalid Move");
            
            // Log the invalid move attempt
            TestHelpers.PrintMove(SudokuPlayerEnum.Player2, 0, 1, 5, false);
            
            sudoku.SetCell(0, 1, 5, SudokuPlayerEnum.Player2); // Duplicate in row

            // Print state after invalid move
            TestHelpers.PrintGameState(sudoku, "After Invalid Move");

            var board = sudoku.Board;
            Assert.AreEqual(5, board[0, 0]);
            Assert.AreEqual(0, board[0, 1]);
            Assert.AreEqual(1, sudoku.getScoreForPlayer(SudokuPlayerEnum.Player1));
            Assert.AreEqual(0, sudoku.getScoreForPlayer(SudokuPlayerEnum.Player2));
        }

        [Test]
        public void GetScoreForPlayer_ShouldReturnCorrectScores()
        {
            sudoku.InitializeBoard();
            sudoku.SetCell(0, 0, 1, SudokuPlayerEnum.Player1);
            sudoku.SetCell(1, 1, 2, SudokuPlayerEnum.Player2);

            // Print the game state with scores
            TestHelpers.PrintGameState(sudoku, "Two Players Made Moves");

            Assert.AreEqual(1, sudoku.getScoreForPlayer(SudokuPlayerEnum.Player1));
            Assert.AreEqual(1, sudoku.getScoreForPlayer(SudokuPlayerEnum.Player2));
        }

        [Test]
        public void IsBoardFull_ShouldReturnTrueWhenNoZeros()
        {
            sudoku.InitializeBoard();
            
            // Fill the board with a valid Sudoku pattern
            // This creates a valid Sudoku grid where each row, column, and 3x3 box has numbers 1-9
            int[,] validSudokuPattern = {
                {1, 2, 3, 4, 5, 6, 7, 8, 9},
                {4, 5, 6, 7, 8, 9, 1, 2, 3},
                {7, 8, 9, 1, 2, 3, 4, 5, 6},
                {2, 3, 1, 5, 6, 4, 8, 9, 7},
                {5, 6, 4, 8, 9, 7, 2, 3, 1},
                {8, 9, 7, 2, 3, 1, 5, 6, 4},
                {3, 1, 2, 6, 4, 5, 9, 7, 8},
                {6, 4, 5, 9, 7, 8, 3, 1, 2},
                {9, 7, 8, 3, 1, 2, 6, 4, 5}
            };
            
            // Apply the pattern to the board
            for (int i = 0; i < Sudoku.Size; i++)
                for (int j = 0; j < Sudoku.Size; j++)
                    sudoku.SetCell(i, j, validSudokuPattern[i, j], SudokuPlayerEnum.Player1);

            // Print the completed board
            TestHelpers.PrintBoard(sudoku, "Completed Board");

            Assert.IsTrue(sudoku.IsBoardFull());
        }

        [Test]
        public void IsBoardFull_ShouldReturnFalseWhenEmptyCellsExist()
        {
            sudoku.InitializeBoard();
            sudoku.SetCell(0, 0, 1, SudokuPlayerEnum.Player1);

            // Print the partially filled board
            TestHelpers.PrintBoard(sudoku, "Partially Filled Board");

            // Print the uncompleted board
            TestHelpers.PrintBoard(sudoku, "Uncompleted Board");

            Assert.IsFalse(sudoku.IsBoardFull());
        }

        [Test]
        public void ResetBoard_ShouldClearBoardAndScores()
        {
            sudoku.SetCell(0, 0, 3, SudokuPlayerEnum.Player1);
            sudoku.SetCell(1, 1, 4, SudokuPlayerEnum.Player2);

            // Print state before reset
            TestHelpers.PrintGameState(sudoku, "Before Reset");

            sudoku.ResetBoard();
            
            // Print state after reset
            TestHelpers.PrintGameState(sudoku, "After Reset");
            
            var board = sudoku.Board;

            for (int i = 0; i < Sudoku.Size; i++)
                for (int j = 0; j < Sudoku.Size; j++)
                    Assert.AreEqual(0, board[i, j]);


            // Print the reset board
            TestHelpers.PrintBoard(sudoku, "Reset Board");

            Assert.AreEqual(0, sudoku.getScoreForPlayer(SudokuPlayerEnum.Player1));
            Assert.AreEqual(0, sudoku.getScoreForPlayer(SudokuPlayerEnum.Player2));
        }

        [Test]
        public void SetCell_InvalidPosition_ShouldThrowException()
        {
            // Test invalid row
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                sudoku.SetCell(-1, 0, 5, SudokuPlayerEnum.Player1));
            
            // Test invalid column
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                sudoku.SetCell(0, 10, 5, SudokuPlayerEnum.Player1));
            
            // Test invalid value
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                sudoku.SetCell(0, 0, 15, SudokuPlayerEnum.Player1));
        }

        [Test]
        public void GetScoreForPlayer_InvalidEnum_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(() =>
                sudoku.getScoreForPlayer((SudokuPlayerEnum)99));
        }
    }
}