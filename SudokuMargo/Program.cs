// See https://aka.ms/new-console-template for more information
using SudokuMargo.Core;
Sudoku sudokuMargo = new Sudoku();
sudokuMargo.InitializeBoard();

AutoResetEvent player1Turn = new AutoResetEvent(true);  // Player 1 starts
AutoResetEvent player2Turn = new AutoResetEvent(false); // Player 2 waits

Task player1 = Task.Run(() => PlayGame(sudokuMargo,SudokuPlayerEnum.Player1, player1Turn, player2Turn));
Task player2 = Task.Run(() => PlayGame(sudokuMargo,SudokuPlayerEnum.Player2, player2Turn, player1Turn));

Task.WaitAll(player1, player2);

Console.WriteLine($"Score final : Joueur 1 = {sudokuMargo.getScoreForPlayer(SudokuPlayerEnum.Player1)}" +
    $", Joueur 2 = {sudokuMargo.getScoreForPlayer(SudokuPlayerEnum.Player2)}");
Console.WriteLine("Grille finale :");
PrintGrid(sudokuMargo.Board);

static void PlayGame(Sudoku sudokuMargo,SudokuPlayerEnum sudokuPlayerEnum, AutoResetEvent myTurn, AutoResetEvent otherTurn)
{
    Random random = new Random();
    int maxMoves = 100; // Maximum moves per player
    int movesCount = 0;
    while (!sudokuMargo.IsBoardFull() && movesCount < maxMoves)
    {
        myTurn.WaitOne(); // Wait for the player's turn
        int row = random.Next(0, Sudoku.Size);
        int col = random.Next(0, Sudoku.Size);
        int value = random.Next(1, 10); // Random value between 1 and 9
        if(sudokuMargo.IsValidMove(row, col, value))
        {
            sudokuMargo.SetCell(row, col, value, sudokuPlayerEnum);
            Console.WriteLine($"Joueur {sudokuPlayerEnum}: ({row}, {col}) = {value}");
        }
        else
        {
            Console.WriteLine($"Joueur {sudokuPlayerEnum}: Mouvement invalide ({row}, {col}) = {value}");
        }
        otherTurn.Set(); // Signal the other player's turn
        movesCount++;
    }
    Console.WriteLine("La grille est pleine !");
}

static void PrintGrid(int[,] sudokuBoard)
{
    Console.WriteLine("\nGrille finale:");
    int firstDimensionSize = sudokuBoard.GetLength(0);
    int secondDimensionSize = sudokuBoard.GetLength(1);
    for (int i = 0; i < firstDimensionSize; i++)
    {
        for (int j = 0; j < secondDimensionSize; j++)
            Console.Write($"{sudokuBoard[i, j]} ");
        Console.WriteLine();
    }
}