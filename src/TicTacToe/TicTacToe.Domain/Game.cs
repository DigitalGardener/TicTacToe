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

        private CellStatus[,] _rawCells = new CellStatus[GRID_SIZE, GRID_SIZE];
        private CellStatus GetCell(CellAddress address) => _rawCells[address.Row, address.Column];
        private void SetCell(CellAddress address, CellStatus status) => _rawCells[address.Row, address.Column] = status;

        /// <summary>
        /// Indicates the current state of play
        /// </summary>
        public GameStatus Status { get; private set; } = GameStatus.New;

        /// <summary>
        /// ID of player whose turn it is to play
        /// </summary>
        public PlayerRole PlayerTurn { get; private set; } = PlayerRole.Initiator;
        public IEnumerable<CellStatus> CellStatuses => _rawCells.Cast<CellStatus>();

        /// <summary>
        /// Initializes a new Game with unmarked cells
        /// </summary>
        public Game()
        {
            for (int row = 0; row < GRID_SIZE; row++)
            {
                for (int column = 0; column < GRID_SIZE; column++)
                {
                    _rawCells[row, column] = CellStatus.Unmarked;
                }
            }
        }

        /// <summary>
        /// Sets the status of a cell
        /// </summary>
        /// <param name="row">Row number of cell</param>
        /// <param name="column">Column number of cell</param>
        /// <param name="cellStatus">New status of cell</param>
        public void Play(CellAddress cellAddress, CellStatus cellStatus)
        {
            if (cellStatus != CellStatus.X && cellStatus != CellStatus.O)
            {
                throw new ArgumentException(nameof(cellStatus), $"'{nameof(cellStatus)}' '{cellStatus}' is invalid");
            }

            if (GetCell(cellAddress) != CellStatus.Unmarked)
            {
                throw new InvalidOperationException($"Cell with row={cellAddress.Row} and column={cellAddress.Column} is already marked");
            }

            SetCell(cellAddress, cellStatus);

            if (Status == GameStatus.New)
            {
                Status = GameStatus.InProgress;
            }

            PlayerTurn = PlayerTurn == PlayerRole.Initiator ? PlayerRole.Opponent : PlayerRole.Initiator;
        }
    }
}