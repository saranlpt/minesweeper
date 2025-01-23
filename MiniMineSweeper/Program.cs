using MiniMineSweeper.Repositories;
using MiniMineSweeper.Services;

namespace MiniMineSweeper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mine Sweeper Game");
            IBoardRepository boardRepository = new BoardRepository();
            IPlayerRepository playerRepository = new PlayerRepository();

            GameService gameService = new GameService(boardRepository, playerRepository);
            gameService.GameStart();
        }
    }
}
