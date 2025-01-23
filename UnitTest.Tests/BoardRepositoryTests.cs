using MiniMineSweeper.Models;
using MiniMineSweeper.Repositories;
using System.IO;
using Xunit;

namespace UnitTest.Tests
{
    public class BoardRepositoryTests
    {
        private readonly IBoardRepository _boardRepository;
        private readonly Board _board;

        public BoardRepositoryTests()
        {
            _boardRepository = new BoardRepository();
            _board = new Board(8, 8);
        }

        [Fact]
        public void Initialize_ShouldSetUpBoardWithCorrectChar()
        {
            // Arrange
            _boardRepository.Initialize(_board);

            // Act & Assert
            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Columns; j++)
                {
                    Assert.Equal('.', _board.Grid[i, j]);
                }
            }
        }

        [Fact]
        public void SetMines_ShouldPlaceCorrectNumberOfMines()
        {
            // Arrange
            _boardRepository.Initialize(_board);
            int mineCount = 10;

            // Act
            _boardRepository.SetMines(_board, mineCount);

            // Assert
            int actualMineCount = 0;
            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Columns; j++)
                {
                    if (_board.Grid[i, j] == '*')
                    {
                        actualMineCount++;
                    }
                }
            }
            Assert.Equal(mineCount, actualMineCount);
        }

        [Fact]
        public void IsMine_ShouldReturnTrueForMineLocation()
        {
            // Arrange
            _boardRepository.Initialize(_board);
            _board.Grid[0, 1] = '*'; // Place a mine at a specific location

            // Act
            bool isMine = _boardRepository.IsMine(_board, 0, 1);

            // Assert
            Assert.True(isMine);
        }

        [Fact]
        public void Save_ShouldSaveBoardStateToFile()
        {
            // Arrange
            _boardRepository.Initialize(_board);
            _boardRepository.SetMines(_board, 10);

            // Act
            _boardRepository.Save(_board);

            // Assert
            Assert.True(File.Exists("board_save.txt"));

            // Verify file content
            var savedContent = File.ReadAllLines("board_save.txt");
            Assert.Equal($"{_board.Rows},{_board.Columns}", savedContent[0]);

            for (int i = 0; i < _board.Rows; i++)
            {
                Assert.Equal(new string(_board.Grid.Cast<char>().Skip(i * _board.Columns).Take(_board.Columns).ToArray()), savedContent[i + 1]);
            }
        }

        [Fact]
        public void Load_ShouldLoadBoardStateFromFile()
        {
            // Arrange
            _boardRepository.Initialize(_board);
            _boardRepository.SetMines(_board, 10);
            _boardRepository.Save(_board);

            // Act
            var loadedBoard = _boardRepository.Load();

            // Assert
            Assert.NotNull(loadedBoard);
            Assert.Equal(_board.Rows, loadedBoard.Rows);
            Assert.Equal(_board.Columns, loadedBoard.Columns);

            // Verify the loaded board has the same grid configuration
            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Columns; j++)
                {
                    Assert.Equal(_board.Grid[i, j], loadedBoard.Grid[i, j]);
                }
            }
        }
    }
}