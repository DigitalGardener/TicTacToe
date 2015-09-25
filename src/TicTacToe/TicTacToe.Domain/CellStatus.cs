namespace TicTacToe.Domain
{
    /// <summary>
    /// Represents cell status
    /// </summary>
    public enum CellStatus
    {
        /// <summary>
        /// Cell has been marked by one player
        /// </summary>
        X,
        /// <summary>
        /// Cell has been marked by the other player
        /// </summary>
        O,
        /// <summary>
        /// Cell has not been marked by either player
        /// </summary>
        Unmarked
    }
}