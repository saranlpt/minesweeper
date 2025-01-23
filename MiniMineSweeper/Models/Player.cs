using System;


namespace MiniMineSweeper.Models
{
    public class Player
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Lives { get; set; }
        public int Moves { get; set; }

        public Player(int row = 0, int column = 0, int lives = 3)
        {
            Row = row;
            Column = column;
            Lives = lives;
            Moves = 0;
        }

        // Movement logic is now encapsulated here
        public void Move(string direction, Board board)
        {
            switch (direction)
            {
                case "up":
                    if (Row > 0) Row--;
                    break;
                case "down":
                    if (Row < board.Rows - 1) Row++;
                    break;
                case "left":
                    if (Column > 0) Column--;
                    break;
                case "right":
                    if (Column < board.Columns - 1) Column++;
                    break;
                default:
                    throw new ArgumentException("Invalid move direction.");
            }
        }
    }
}
