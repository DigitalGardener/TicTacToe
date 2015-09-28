using System;
using System.Collections.Generic;
using static System.Linq.Enumerable;

namespace TicTacToe.Domain
{
    /// <summary>
    /// Represents a Tic-Tac-Toe game
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Size of Tic-Tac-Toe grid
        /// </summary>
        public const int GRID_SIZE = 3;

        private CellStatus[,] _cells = new CellStatus[GRID_SIZE, GRID_SIZE];

        /// <summary>
        /// Maximum value for cell row numbers and column numbers
        /// </summary>
        public const int MAX_ROW_COLUMN = GRID_SIZE - 1;

        /// <summary>
        /// Minimum value for cell row numbers and column numbers
        /// </summary>
        public const int MIN_ROW_COLUMN = 0;

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

        /// <summary>
        /// Sets the status of a cell
        /// </summary>
        /// <param name="row">Row number of cell</param>
        /// <param name="column">Column number of cell</param>
        /// <param name="status">New status of cell</param>
        public void Play(int row, int column, CellStatus status)
        {
            if (row < MIN_ROW_COLUMN || row > MAX_ROW_COLUMN)
            {
                throw new ArgumentOutOfRangeException(nameof(row), $"'{nameof(row)}' should be between {MIN_ROW_COLUMN} and {MAX_ROW_COLUMN}");
            }

            if (column < MIN_ROW_COLUMN || column > MAX_ROW_COLUMN)
            {
                throw new ArgumentOutOfRangeException(nameof(column), $"'{nameof(column)}' should be between {MIN_ROW_COLUMN} and {MAX_ROW_COLUMN}");
            }

            if (status != CellStatus.X && status != CellStatus.O)
            {
                throw new ArgumentException(nameof(status), $"'{nameof(status)}' '{status}' is invalid");
            }

            if (_cells[row, column] != CellStatus.Unmarked)
            {
                throw new InvalidOperationException($"Cell with row={row} and column={column} is already marked");
            }

            _cells[row, column] = status;

            if (Status == GameStatus.New)
            {
                Status = GameStatus.InProgress;
            }

        }
    }
}