namespace TicTacToe.Domain
{
    /// <summary>
    /// Represents the state of play
    /// </summary>
    public enum GameStatus
    {
        /// <summary>
        /// No plays have been made
        /// </summary>
        New,
        /// <summary>
        /// 1st play has been made
        /// </summary>
        InProgress,
        /// <summary>
        /// No more plays can be made
        /// </summary>
        Over
    }
}