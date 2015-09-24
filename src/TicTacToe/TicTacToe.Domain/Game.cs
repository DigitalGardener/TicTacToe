namespace TicTacToe.Domain
{
    /// <summary>
    /// Represents a Tic-Tac-Toe game
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Indicates whether player's can continue to play
        /// </summary>
        public bool IsOver { get; } = false;
    }
}