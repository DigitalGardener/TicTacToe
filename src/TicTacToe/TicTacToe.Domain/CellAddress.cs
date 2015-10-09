using System;

namespace TicTacToe.Domain
{
    /// <summary>
    /// Represents the coordinates of a cell in game grid
    /// </summary>
    public class CellAddress
    {
        public const int GRID_SIZE = 3;

        /// <summary>
        /// Maximum value for cell row numbers and column numbers
        /// </summary>
        public const int MAX_ROW_COLUMN = GRID_SIZE - 1;

        /// <summary>
        /// Minimum value for cell row numbers and column numbers
        /// </summary>
        public const int MIN_ROW_COLUMN = 0;

        private Tuple<int, int> _tuple;

        public CellAddress(int row, int column)
        {
            if (row < MIN_ROW_COLUMN || row > MAX_ROW_COLUMN)
            {
                throw new ArgumentOutOfRangeException(nameof(row), $"'{nameof(row)}' should be between {MIN_ROW_COLUMN} and {MAX_ROW_COLUMN}");
            }

            if (column < MIN_ROW_COLUMN || column > MAX_ROW_COLUMN)
            {
                throw new ArgumentOutOfRangeException(nameof(column), $"'{nameof(column)}' should be between {MIN_ROW_COLUMN} and {MAX_ROW_COLUMN}");
            }

            _tuple = new Tuple<int, int>(row, column);
        }
        
        /// <summary>
        /// Cell's row number
        /// </summary>
        public int Row => _tuple.Item1;

        /// <summary>
        /// Cell's column number
        /// </summary>
        public int Column => _tuple.Item2;

        /// <summary>
        /// Returns a string that represents the value of this CellAddress instance.
        /// </summary>
        /// <returns>The string representation of this CellAddress object</returns>
        public override string ToString()
        {
            return _tuple.ToString();
        }
        /// <summary>
        /// Returns a value which indicates whether the current CellAddress object equals the specified object
        /// </summary>
        /// <param name="obj">The object to compare with this instance</param>
        /// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var cellAddress = obj as CellAddress;

            if (cellAddress == null)
            {
                return false;
            }

            return _tuple.Equals(cellAddress._tuple);
        }

        /// <summary>
        /// Returns the hash code for the current CellAddress object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return _tuple.GetHashCode();
        }
    }
}