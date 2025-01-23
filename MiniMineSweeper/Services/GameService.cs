using MiniMineSweeper.Models;
using MiniMineSweeper.Repositories;
using System;

namespace MiniMineSweeper.Services
{
    public class GameService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IPlayerRepository _playerRepository;
        private Board _board;
        private Player _player;

        public GameService(IBoardRepository boardRepository, IPlayerRepository playerRepository)
        {
            _boardRepository = boardRepository ?? throw new ArgumentNullException(nameof(boardRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));

        }

        public void GameStart()
        {
            _board = _boardRepository.Load() ?? new Board();
            _player = _playerRepository.Load() ?? new Player();

            _board.Initialize();
            _board.SetMines(10, new Random());

            PlayGame();
        }

        private void PlayGame()
        {
            while (_player.Lives > 0 && _player.Row < _board.Rows - 1)
            {
                // Get player move input (assuming the user interface is abstracted)
                string move = GetPlayerMove(); // This is a placeholder for user input logic
                _player.Move(move, _board);

                if (_board.IsMine(_player.Row, _player.Column))
                {
                    _player.Lives--;
                    Console.WriteLine($"Boom! You hit a mine. Lives remaining: {_player.Lives}");
                }

                _player.Moves++;
                ShowGameStatus();

                // Optionally, save the state periodically
                _playerRepository.Save(_player);
                _boardRepository.Save(_board);
            }

            ShowEndGameMessage();
        }

        private void ShowGameStatus()
        {
            Console.Clear();
            // Display board and other game info
            Console.WriteLine($"Lives: {_player.Lives}, Moves: {_player.Moves}");
        }

        private void ShowEndGameMessage()
        {
            if (_player.Lives > 0)
            {
                Console.WriteLine($"Congratulations! You won in {_player.Moves} moves.");
            }
            else
            {
                Console.WriteLine("Game Over! You ran out of lives.");
            }
        }

        // Placeholder for getting input
        private string GetPlayerMove()
        {
            Console.Write("Enter move (up, down, left, right): ");
            return Console.ReadLine()?.ToLower();
        }
    }
}

