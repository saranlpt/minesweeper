using System;


namespace MiniMineSweeper.Models
{
    
     public class Board
    {
        public int Rows { get; }
        public int Columns { get; }
        public char[,] Grid { get; }

        public Board(int rows = 8, int columns = 8)
        {
            Rows = rows;
            Columns = columns;
            Grid = new char[Rows, Columns];
        }

        // Initialize the grid with empty cells
        public void Initialize()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Grid[row, col] = '.';
                }
            }
        }

        // Set mines randomly on the board
        public void SetMines(int mineCount, Random random)
        {
            for (int i = 0; i < mineCount; i++)
            {
                int randRow, randColumn;
                do
                {
                    randRow = random.Next(Rows);
                    randColumn = random.Next(Columns);
                }
                while (Grid[randRow, randColumn] == '*');  // Avoiding duplicate placement

                Grid[randRow, randColumn] = '*';
            }
        }

        // Check if the given position contains a mine
        public bool IsMine(int row, int column)
        {
            return Grid[row, column] == '*';
        }

        // Show the board state, including the player's position and game status
        public void ShowBoard(Player player)
        {
            Console.Clear();
            Console.WriteLine("Board:");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    // Display the player as 'P'
                    if (i == player.Row && j == player.Column)
                    {
                        Console.Write('P' + " "); // 'P' represents the player
                    }
                    else
                    {
                        Console.Write(Grid[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Lives: {player.Lives}  Moves: {player.Moves}");
            Console.WriteLine($"Current Position: {GetChessNotation(player.Row, player.Column)}");
        }

        // Get the chessboard notation for the current row/column
        private string GetChessNotation(int row, int column)
        {
            char file = (char)('A' + column);
            int rank = 8 - row; // Adjust for chessboard notation
            return $"{file}{rank}";
        }
    }
}
