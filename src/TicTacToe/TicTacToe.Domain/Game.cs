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
        private CellContent[,] _rawCells = new CellContent[CellAddress.GRID_SIZE, CellAddress.GRID_SIZE];
        private CellContent GetCellContent(CellAddress address) => _rawCells[address.Row, address.Column];
        private void SetCellContent(CellAddress address, CellContent content) => _rawCells[address.Row, address.Column] = content;
 
        /// <summary>
        /// Contents of all cells in particular order
        /// </summary>
        public IEnumerable<CellContent> CellsContent => _rawCells.Cast<CellContent>();

        /// <summary>
        /// Indicates the current state of play
        /// </summary>
        public GameStatus Status { get; private set; } = GameStatus.New;

        /// <summary>
        /// Contents of cell marked by last player
        /// </summary>
        public CellContent LastPlayCellContent { get; private set; }

        private CellAddress[][] _possibleWinningCombinations = {
            //Row combinations
            new CellAddress[] { new CellAddress(0,0), new CellAddress(0,1), new CellAddress(0,2) },
            new CellAddress[] { new CellAddress(1,0), new CellAddress(1,1), new CellAddress(1,2) },
            new CellAddress[] { new CellAddress(2,0), new CellAddress(2,1), new CellAddress(2,2) },

            //Column combinations
            new CellAddress[] { new CellAddress(0,0), new CellAddress(1,0), new CellAddress(2,0) },
            new CellAddress[] { new CellAddress(0,1), new CellAddress(1,1), new CellAddress(2,1) },
            new CellAddress[] { new CellAddress(0,2), new CellAddress(1,2), new CellAddress(2,2) },

            //Diagonal combinations
            new CellAddress[] { new CellAddress(0,0), new CellAddress(1,1), new CellAddress(2,2) },
            new CellAddress[] { new CellAddress(2,0), new CellAddress(1,1), new CellAddress(0,2) }
        };

        private CellAddress[] _winningCombination;
        /// <summary>
        /// Addresses of cells that make up the winning combination of marked cells
        /// </summary>
        public CellAddress[] WinningCombination
        {
            get
            {
                if (_winningCombination == null)
                {
                    _winningCombination = _possibleWinningCombinations.FirstOrDefault(addresses =>
                                            GetCellContent(addresses[0]) != CellContent.Unmarked
                                            && GetCellContent(addresses[0]) == GetCellContent(addresses[1])
                                            && GetCellContent(addresses[1]) == GetCellContent(addresses[2])
                                            );
                }
                return _winningCombination;
            }

        }
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

            if (LastPlayCellContent != CellContent.Unmarked && LastPlayCellContent == content)
            {
                throw new ArgumentException(nameof(content), $"'{content}' cannot be played two times consecutively");
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

            LastPlayCellContent = content;
        }
    }
}