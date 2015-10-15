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

        private CellContent[,] _rawCells = new CellContent[GRID_SIZE, GRID_SIZE];
        private CellContent GetCellContent(CellAddress address) => _rawCells[address.Row, address.Column];
        private void SetCellContent(CellAddress address, CellContent content) => _rawCells[address.Row, address.Column] = content;

        /// <summary>
        /// Indicates the current state of play
        /// </summary>
        public GameStatus Status { get; private set; } = GameStatus.New;

        /// <summary>
        /// ID of player whose turn it is to play
        /// </summary>
        public PlayerRole PlayerTurn { get; private set; } = PlayerRole.Initiator;
        public IEnumerable<CellContent> CellsContent => _rawCells.Cast<CellContent>();

        /// <summary>
        /// Sets the status of a cell
        /// </summary>
        /// <param name="row">Row number of cell</param>
        /// <param name="column">Column number of cell</param>
        /// <param name="content">New status of cell</param>
        public void Play(CellAddress address, CellContent content)
        {
            if (content != CellContent.X && content != CellContent.O)
            {
                throw new ArgumentException(nameof(content), $"'{nameof(content)}' '{content}' is invalid");
            }

            if (GetCellContent(address) != CellContent.Unmarked)
            {
                throw new InvalidOperationException($"Cell with row={address.Row} and column={address.Column} is already marked");
            }

            SetCellContent(address, content);

            if (Status == GameStatus.New)
            {
                Status = GameStatus.InProgress;
            }

            PlayerTurn = PlayerTurn == PlayerRole.Initiator ? PlayerRole.Opponent : PlayerRole.Initiator;
        }
    }
}