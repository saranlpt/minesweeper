using MiniMineSweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMineSweeper.Repositories
{
    public interface IBoardRepository
    {
        void Initialize(Board board);   // Initialize the board
        void SetMines(Board board, int mineCount);  // Add this method to the interface
        void Save(Board board);         // Save the board state
        Board Load();                   // Load the board state
        bool IsMine(Board board, int row, int column);

    }
}
