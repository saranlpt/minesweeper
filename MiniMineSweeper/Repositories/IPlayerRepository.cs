using MiniMineSweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMineSweeper.Repositories
{
    public interface IPlayerRepository
    {
        void Move(string direction, Board board, Player player);
        void Save(Player player);
        Player Load();
    }
}
