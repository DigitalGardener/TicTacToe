using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static System.Linq.Enumerable;

namespace TicTacToe.Domain.Tests
{
    /// <summary>
    /// Game instance tests
    /// </summary>
    [TestClass]
    public class GameTest
    {
        public GameTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        Game gameUnderTest;

        [TestInitialize()]
        public void Initialize() {
            gameUnderTest = new Game();
        }

        [TestMethod]
        public void AtStartOfGameStatusIsNew()
        {
            Assert.IsTrue(gameUnderTest.Status == GameStatus.New);
        }

        [TestMethod]
        public void AtStartOfGameAllCellsAreUnmarked()
        {
            Assert.IsTrue(gameUnderTest.CellStatuses.All(status => status == CellStatus.Unmarked));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IfAttemptIsMadeToMarkCellWithRowNumberLessThanMinimumThenExceptionIsThrown()
        {
            gameUnderTest.Play(Game.MIN_ROW_COLUMN - 1, Game.MIN_ROW_COLUMN, CellStatus.X);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IfAttemptIsMadeToMarkCellWithRowNumberGreaterThanMaximumThenExceptionIsThrown()
        {
            gameUnderTest.Play(Game.MAX_ROW_COLUMN + 1, Game.MIN_ROW_COLUMN, CellStatus.X);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IfAttemptIsMadeToMarkCellWithColumnNumberLessThanMinimumThenExceptionisThrown()
        {
            gameUnderTest.Play(Game.MIN_ROW_COLUMN, Game.MIN_ROW_COLUMN - 1, CellStatus.X);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IfAttemptIsMadeToMarkCellWithColumnNumberGreaterThanMaximumThenExceptionIsThrown()
        {
            gameUnderTest.Play(Game.MIN_ROW_COLUMN, Game.MAX_ROW_COLUMN + 1, CellStatus.X);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IfAttemptIsMadeToSetCellStatusToUnmarkedThenAnExceptionIsThrown()
        {
            gameUnderTest.Play(Game.MIN_ROW_COLUMN, Game.MIN_ROW_COLUMN, CellStatus.Unmarked);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void IfAttemptIsMadeToMarkACellWhichIsNotUnmarkedThenExceptionIsThrown()
        {
            gameUnderTest.Play(Game.MIN_ROW_COLUMN, Game.MIN_ROW_COLUMN, CellStatus.X);
            gameUnderTest.Play(Game.MIN_ROW_COLUMN, Game.MIN_ROW_COLUMN, CellStatus.O);
        }

        [TestMethod]
        public void AfterFirstPlayGameStatusBecomesInProgress()
        {
            gameUnderTest.Play(Game.MIN_ROW_COLUMN, Game.MIN_ROW_COLUMN, CellStatus.X);
            Assert.IsTrue(gameUnderTest.Status == GameStatus.InProgress);
        }
    }
}
