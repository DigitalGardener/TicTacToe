namespace TicTacToe.Domain
{
    /// <summary>
    /// Represents cell's content
    /// </summary>
    public enum CellContent
    {
        /// <summary>
        /// Cell has not been marked by either player
        /// </summary>
        Unmarked,
        /// <summary>
        /// Cell has been marked by one player
        /// </summary>
        X,
        /// <summary>
        /// Cell has been marked by the other player
        /// </summary>
        O
    }
}