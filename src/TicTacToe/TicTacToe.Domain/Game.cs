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
        /// Contents of all cells in no particular order
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

        /// <summary>
        /// Addresses of cells in the first row of game grid
        /// </summary>
        public static readonly CellAddress[] FirstRow;
        /// <summary>
        /// Addresses of cells in the second row of game grid
        /// </summary>
        public static readonly CellAddress[] SecondRow;
        /// <summary>
        /// Addresses of cells in the third row of game grid
        /// </summary>
        public static readonly CellAddress[] ThirdRow;
        /// <summary>
        /// Addresses of cells in the first column of game grid
        /// </summary
        public static readonly CellAddress[] FirstColumn;
        /// <summary>
        /// Addresses of cells in the second column of game grid
        /// </summary>
        public static CellAddress[] SecondColumn { get; private set; }
        /// <summary>
        /// Addresses of cells in the third column of game grid
        /// </summary>
        public static readonly CellAddress[] ThirdColumn;
        /// <summary>
        /// Addresses of cells in the diagonal which starts in the top left corner of the game grid
        /// </summary>
        public static readonly CellAddress[] TopLeftDiagonal;
        /// <summary>
        /// Addresses of cells in the diagonal which starts in the top right corner of the game grid
        /// </summary>
        public static readonly CellAddress[] TopRightDiagonal;

        static private CellAddress[][] _possibleWinningCombinations;

        static Game()
        {
            _possibleWinningCombinations = new CellAddress[][]{

            FirstRow = new CellAddress[] { new CellAddress(0, 0), new CellAddress(0, 1), new CellAddress(0, 2) },
            SecondRow = new CellAddress[] { new CellAddress(1, 0), new CellAddress(1, 1), new CellAddress(1, 2) },
            ThirdRow = new CellAddress[] { new CellAddress(2, 0), new CellAddress(2, 1), new CellAddress(2, 2) },

            FirstColumn = new CellAddress[] { new CellAddress(0, 0), new CellAddress(1, 0), new CellAddress(2, 0) },
            SecondColumn = new CellAddress[] { new CellAddress(0, 1), new CellAddress(1, 1), new CellAddress(2, 1) },
            ThirdColumn = new CellAddress[] { new CellAddress(0, 2), new CellAddress(1, 2), new CellAddress(2, 2) },

            TopLeftDiagonal = new CellAddress[] { new CellAddress(0, 0), new CellAddress(1, 1), new CellAddress(2, 2) },
            TopRightDiagonal = new CellAddress[] { new CellAddress(2, 0), new CellAddress(1, 1), new CellAddress(0, 2) }

            };
        }

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
        /// Indicates whether a player has won the game
        /// </summary>
        public bool HasWinner => WinningCombination != null;

        private bool _isDrawn;
        /// <summary>
        /// Indicates whether all cells have been marked without producing a winner
        /// </summary>
        public bool IsDrawn
        {
            get
            {
                if (!_isDrawn && !HasWinner)
                {
                    _isDrawn = CellsContent.All(content => !content.Equals(CellContent.Unmarked));
                }
                return _isDrawn;
            }
        }

        /// <summary>
        /// Sets the content of a cell
        /// </summary>
        /// <param name="row">Row number of cell</param>
        /// <param name="column">Column number of cell</param>
        /// <param name="content">New content of cell</param>
        public void Play(CellAddress address, CellContent content)
        {
            if (Status == GameStatus.Over)
            {
                throw new InvalidOperationException("Play cannot be made after game is over");
            }

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

            if (HasWinner || IsDrawn)
            {
                Status = GameStatus.Over;
            }

            LastPlayCellContent = content;
        }
    }
}