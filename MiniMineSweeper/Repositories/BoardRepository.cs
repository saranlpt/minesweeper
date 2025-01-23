using MiniMineSweeper.Models;

namespace MiniMineSweeper.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private const string BoardSaveFilePath = "board_save.txt"; // Path to save/load the board state

        public void Save(Board board)
        {
            using (var writer = new StreamWriter(BoardSaveFilePath))
            {
                // Save the grid dimensions
                writer.WriteLine($"{board.Rows},{board.Columns}");

                // Save the board grid (as a series of strings, e.g., "....\n..*.\n...")
                for (int i = 0; i < board.Rows; i++)
                {
                    for (int j = 0; j < board.Columns; j++)
                    {
                        writer.Write(board.Grid[i, j]);
                    }
                    writer.WriteLine();
                }
            }
        }

        public Board Load()
        {
            if (!File.Exists(BoardSaveFilePath))
                return null; // No saved board found

            using (var reader = new StreamReader(BoardSaveFilePath))
            {
                // Read dimensions
                var dimensions = reader.ReadLine().Split(',');
                int rows = int.Parse(dimensions[0]);
                int columns = int.Parse(dimensions[1]);

                var board = new Board(rows, columns);

                // Read the grid and populate it
                for (int i = 0; i < rows; i++)
                {
                    var line = reader.ReadLine();
                    for (int j = 0; j < columns; j++)
                    {
                        board.Grid[i, j] = line[j];
                    }
                }

                return board;
            }
        }

        public void Initialize(Board board)
        {
            for (int row = 0; row < board.Rows; row++)
            {
                for (int column = 0; column < board.Columns; column++)
                {
                    board.Grid[row, column] = '.';
                }
            }
        }

        public void SetMines(Board board, int mineCount)
        {
            Random random = new Random();
            int minesPlaced = 0;

            while (minesPlaced < mineCount)
            {
                int randRow = random.Next(board.Rows);
                int randColumn = random.Next(board.Columns);

                if (board.Grid[randRow, randColumn] == '.' && (randRow != 0 || randColumn != 0)) // Don't put a mine on the starting position
                {
                    board.Grid[randRow, randColumn] = '*';
                    minesPlaced++;
                }
            }
        }

        public bool IsMine(Board board, int row, int column)
        {
            return board.Grid[row, column] == '*';  // Checks if the specific cell is a mine
        }
    }
}