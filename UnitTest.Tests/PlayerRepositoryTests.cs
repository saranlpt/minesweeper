

using MiniMineSweeper.Models;
using MiniMineSweeper.Repositories;
using Xunit;

namespace UnitTest.Tests
{
    public class PlayerRepositoryTests
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly Player _player;
        private readonly Board _board;

        public PlayerRepositoryTests()
        {
            // Setup board and player
            _board = new Board(8, 8); // 8x8 board
            _playerRepository = new PlayerRepository();
            _player = new Player { Row = 0, Column = 0 }; // Player starts at (0, 0)
        }

        // Test that player position updates correctly after each move
        [Theory]
        [InlineData("down", 1, 0)]  // Player moves down
        [InlineData("right", 0, 1)] // Player moves right
        public void MovePlayer_ShouldUpdatePlayerPosition(string direction, int expectedRow, int expectedCol)
        {
            // Act
            _playerRepository.Move(direction, _board, _player);

            // Assert
            Assert.Equal(expectedRow, _player.Row);
            Assert.Equal(expectedCol, _player.Column);
        }

        // Test that the player cannot move out of bounds (e.g., up when at row 0 or left when at column 0)
        [Theory]
        [InlineData("up", 0, 0)]    // Player cannot move up since they're at the top
        [InlineData("left", 0, 0)]  // Player cannot move left since they're at the left-most
        public void MovePlayer_ShouldNotGoOutOfBounds(string direction, int expectedRow, int expectedCol)
        {
            // Act
            _playerRepository.Move(direction, _board, _player);

            // Assert
            Assert.Equal(expectedRow, _player.Row);
            Assert.Equal(expectedCol, _player.Column);
        }
    }
}