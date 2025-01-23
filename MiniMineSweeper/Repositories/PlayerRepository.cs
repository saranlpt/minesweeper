using MiniMineSweeper.Models;
using System;
using System.IO;

namespace MiniMineSweeper.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private const string PlayerSaveFilePath = "player_save.txt"; // Path to save/load the player state

        // Save the current state of the player to a file
        public void Save(Player player)
        {
            using (var writer = new StreamWriter(PlayerSaveFilePath))
            {
                // Save player information (position, lives, moves)
                writer.WriteLine($"{player.Row},{player.Column}");
                writer.WriteLine($"{player.Lives}");
                writer.WriteLine($"{player.Moves}");
            }
        }

        // Load the player state from a file
        public Player Load()
        {
            if (!File.Exists(PlayerSaveFilePath))
                return null; // No saved player found

            using (var reader = new StreamReader(PlayerSaveFilePath))
            {
                // Read player data from file
                var position = reader.ReadLine().Split(',');
                int row = int.Parse(position[0]);
                int column = int.Parse(position[1]);

                int lives = int.Parse(reader.ReadLine());
                int moves = int.Parse(reader.ReadLine());

                return new Player(row, column, lives) { Moves = moves };
            }
        }
        public void Move(string direction, Board board, Player player)
        {
            switch (direction.ToLower())  // Normalize to lowercase
            {
                case "up":
                    if (player.Row > 0)
                        player.Row--;  // Move up if not at the top row
                    break;
                case "down":
                    if (player.Row < board.Rows - 1)
                        player.Row++;  // Move down if not at the bottom row
                    break;
                case "left":
                    if (player.Column > 0)
                        player.Column--;  // Move left if not at the first column
                    break;
                case "right":
                    if (player.Column < board.Columns - 1)
                        player.Column++;  // Move right if not at the last column
                    break;
                default:
                    Console.WriteLine("Invalid move. Try again.");
                    break;
            }
        }
    }
}
