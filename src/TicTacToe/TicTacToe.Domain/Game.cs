using System.Collections.Generic;
using static System.Linq.Enumerable;

namespace TicTacToe.Domain
{
    /// <summary>
    /// Represents a Tic-Tac-Toe game
    /// </summary>
    public class Game
    {

        public const int GRID_SIZE = 3;
        private CellStatus[,] _cells = new CellStatus[GRID_SIZE, GRID_SIZE];

        /// <summary>
        /// Indicates the current status of all cells
        /// </summary>
        public IEnumerable<CellStatus> CellStatuses => _cells.Cast<CellStatus>();

        /// <summary>
        /// Indicates the current state of play
        /// </summary>
        public GameStatus Status { get; private set; } = GameStatus.New;

        /// <summary>
        /// Initializes a new Game with unmarked cells
        /// </summary>
        public Game()
        {
            for (int row = 0; row < GRID_SIZE; row++)
            {
                for (int column = 0; column < GRID_SIZE; column++)
                {
                    _cells[row, column] = CellStatus.Unmarked;
                }
            }
        }
    }
}