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
        CellAddress firstCellAddress;

        [TestInitialize()]
        public void Initialize() {
            gameUnderTest = new Game();
            firstCellAddress = new CellAddress(CellAddress.MIN_ROW_COLUMN, CellAddress.MIN_ROW_COLUMN);
        }

        [TestMethod]
        public void AtStartOfGameStatusIsNew()
        {
            Assert.IsTrue(gameUnderTest.Status == GameStatus.New);
        }

        [TestMethod]
        public void AtStartOfGameAllCellsAreUnmarked()
        {
            Assert.IsTrue(gameUnderTest.CellsContent.All(content => content == CellContent.Unmarked));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IfAttemptIsMadeToSetCellStatusToUnmarkedThenAnExceptionIsThrown()
        {
            gameUnderTest.Play(firstCellAddress, CellContent.Unmarked);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void IfAttemptIsMadeToMarkACellThatIsAlreadyMarkedThenExceptionIsThrown()
        {
            gameUnderTest.Play(firstCellAddress, CellContent.X);
            gameUnderTest.Play(firstCellAddress, CellContent.O);
        }

        [TestMethod]
        public void AfterFirstPlayGameStatusBecomesInProgress()
        {
            gameUnderTest.Play(firstCellAddress, CellContent.X);
            Assert.IsTrue(gameUnderTest.Status == GameStatus.InProgress);
        }

        [TestMethod]
        public void AfterPlayIsMadeLastPlayCellContentIsEqual()
        {
            var content = CellContent.X;
            gameUnderTest.Play(firstCellAddress, content);

            Assert.AreEqual(content, gameUnderTest.LastPlayCellContent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IfCellContentIsSetToSameValueInTwoConsecutivePlaysThenAnExceptionIsThrown()
        {
            var content = CellContent.X;
            gameUnderTest.Play(firstCellAddress, content);
            gameUnderTest.Play(new CellAddress(2,2), content);
        }

        [TestMethod]
        public void AtStartOfGameThereIsNoWinner()
        {
            Assert.IsFalse(gameUnderTest.HasWinner);
        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInFirstRowGameHasWinner()
        {
            gameUnderTest.Play(new CellAddress(0, 0), CellContent.X);
            gameUnderTest.Play(new CellAddress(1, 0), CellContent.O);
            gameUnderTest.Play(new CellAddress(0, 1), CellContent.X);
            gameUnderTest.Play(new CellAddress(2, 0), CellContent.O);
            gameUnderTest.Play(new CellAddress(0, 2), CellContent.X);

            Assert.IsTrue(gameUnderTest.HasWinner);
        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInSecondRowGameHasWinner()
        {
            gameUnderTest.Play(new CellAddress(1, 0), CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 0), CellContent.O);
            gameUnderTest.Play(new CellAddress(1, 1), CellContent.X);
            gameUnderTest.Play(new CellAddress(2, 0), CellContent.O);
            gameUnderTest.Play(new CellAddress(1, 2), CellContent.X);

            Assert.IsTrue(gameUnderTest.HasWinner);
        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInThirdRowGameHasWinner()
        {
            gameUnderTest.Play(new CellAddress(2, 0), CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 0), CellContent.O);
            gameUnderTest.Play(new CellAddress(2, 1), CellContent.X);
            gameUnderTest.Play(new CellAddress(1, 0), CellContent.O);
            gameUnderTest.Play(new CellAddress(2, 2), CellContent.X);

            Assert.IsTrue(gameUnderTest.HasWinner);
        }


        [TestMethod]
        public void WhenPlayerMarksAllCellsInFirstColumnGameHasWinner()
        {
            gameUnderTest.Play(new CellAddress(0, 0), CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 1), CellContent.O);
            gameUnderTest.Play(new CellAddress(1, 0), CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 2), CellContent.O);
            gameUnderTest.Play(new CellAddress(2, 0), CellContent.X);

            Assert.IsTrue(gameUnderTest.HasWinner);
        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInSecondColumnGameThenHasWinner()
        {
            gameUnderTest.Play(new CellAddress(0, 1), CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 0), CellContent.O);
            gameUnderTest.Play(new CellAddress(1, 1), CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 2), CellContent.O);
            gameUnderTest.Play(new CellAddress(2, 1), CellContent.X);

            Assert.IsTrue(gameUnderTest.HasWinner);
        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInThirdColumnThenGameHasAWinner()
        {
            gameUnderTest.Play(new CellAddress(0, 2), CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 0), CellContent.O);
            gameUnderTest.Play(new CellAddress(1, 2), CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 1), CellContent.O);
            gameUnderTest.Play(new CellAddress(2, 2), CellContent.X);

            Assert.IsTrue(gameUnderTest.HasWinner);
        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInTopLeftDiagonalThenGameHasAWinner()
        {
            gameUnderTest.Play(new CellAddress(0, 0), CellContent.X);
            gameUnderTest.Play(new CellAddress(1, 0), CellContent.O);
            gameUnderTest.Play(new CellAddress(1, 1), CellContent.X);
            gameUnderTest.Play(new CellAddress(2, 0), CellContent.O);
            gameUnderTest.Play(new CellAddress(2, 2), CellContent.X);

            Assert.IsTrue(gameUnderTest.HasWinner);
        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInBottomLeftDiagonalThenGameHasAWinner()
        {
            gameUnderTest.Play(new CellAddress(2, 0), CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 0), CellContent.O);
            gameUnderTest.Play(new CellAddress(1, 1), CellContent.X);
            gameUnderTest.Play(new CellAddress(1, 0), CellContent.O);
            gameUnderTest.Play(new CellAddress(0, 2), CellContent.X);

            Assert.IsTrue(gameUnderTest.HasWinner);
        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInFirstRowThenWinningCombinationReturnsTheirAddresses()
        {
            gameUnderTest.Play(Game.FirstRow[0], CellContent.X);
            gameUnderTest.Play(new CellAddress(1,0), CellContent.O);
            gameUnderTest.Play(Game.FirstRow[1], CellContent.X);
            gameUnderTest.Play(new CellAddress(2,0), CellContent.O);
            gameUnderTest.Play(Game.FirstRow[2], CellContent.X);

            Assert.IsTrue(Game.FirstRow.All(cellAddress => gameUnderTest.WinningCombination.Contains(cellAddress)));
        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInSecondRowThenWinningCombinationReturnsTheirAddresses()
        {
            gameUnderTest.Play(Game.SecondRow[0], CellContent.X);
            gameUnderTest.Play(new CellAddress(0,0), CellContent.O);
            gameUnderTest.Play(Game.SecondRow[1], CellContent.X);
            gameUnderTest.Play(new CellAddress(2,0), CellContent.O);
            gameUnderTest.Play(Game.SecondRow[2], CellContent.X);

            Assert.IsTrue(Game.SecondRow.All(cellAddress => gameUnderTest.WinningCombination.Contains(cellAddress)));

        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInThirdRowThenWinningCombinationReturnsTheirAddresses()
        {
            gameUnderTest.Play(Game.ThirdRow[0], CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 0), CellContent.O);
            gameUnderTest.Play(Game.ThirdRow[1], CellContent.X);
            gameUnderTest.Play(new CellAddress(1, 0), CellContent.O);
            gameUnderTest.Play(Game.ThirdRow[2], CellContent.X);

            Assert.IsTrue(Game.ThirdRow.All(cellAddress => gameUnderTest.WinningCombination.Contains(cellAddress)));

        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInFirstColumnThenWinningCombinationReturnsTheirAddresses()
        {
            gameUnderTest.Play(Game.FirstColumn[0], CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 1), CellContent.O);
            gameUnderTest.Play(Game.FirstColumn[1], CellContent.X);
            gameUnderTest.Play(new CellAddress(1, 1), CellContent.O);
            gameUnderTest.Play(Game.FirstColumn[2], CellContent.X);

            Assert.IsTrue(Game.FirstColumn.All(cellAddress => gameUnderTest.WinningCombination.Contains(cellAddress)));

        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInSecondColumnThenWinningCombinationReturnsTheirAddresses()
        {
            gameUnderTest.Play(Game.SecondColumn[0], CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 0), CellContent.O);
            gameUnderTest.Play(Game.SecondColumn[1], CellContent.X);
            gameUnderTest.Play(new CellAddress(1, 0), CellContent.O);
            gameUnderTest.Play(Game.SecondColumn[2], CellContent.X);

            Assert.IsTrue(Game.SecondColumn.All(cellAddress => gameUnderTest.WinningCombination.Contains(cellAddress)));
        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInThirdColumnThenWinningCombinationReturnsTheirAddresses()
        {
            gameUnderTest.Play(Game.ThirdColumn[0], CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 0), CellContent.O);
            gameUnderTest.Play(Game.ThirdColumn[1], CellContent.X);
            gameUnderTest.Play(new CellAddress(1, 0), CellContent.O);
            gameUnderTest.Play(Game.ThirdColumn[2], CellContent.X);

            Assert.IsTrue(Game.ThirdColumn.All(cellAddress => gameUnderTest.WinningCombination.Contains(cellAddress)));
        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInTopLeftDiagonalThenWinningCombinationReturnsTheirAddresses()
        {
            gameUnderTest.Play(Game.TopLeftDiagonal[0], CellContent.X);
            gameUnderTest.Play(new CellAddress(1, 0), CellContent.O);
            gameUnderTest.Play(Game.TopLeftDiagonal[1], CellContent.X);
            gameUnderTest.Play(new CellAddress(2, 0), CellContent.O);
            gameUnderTest.Play(Game.TopLeftDiagonal[2], CellContent.X);

            Assert.IsTrue(Game.TopLeftDiagonal.All(cellAddress => gameUnderTest.WinningCombination.Contains(cellAddress)));
        }

        [TestMethod]
        public void WhenPlayerMarksAllCellsInTopRightDiagonalThenWinningCombinationReturnsTheirAddresses()
        {
            gameUnderTest.Play(Game.TopRightDiagonal[0], CellContent.X);
            gameUnderTest.Play(new CellAddress(0, 0), CellContent.O);
            gameUnderTest.Play(Game.TopRightDiagonal[1], CellContent.X);
            gameUnderTest.Play(new CellAddress(1, 0), CellContent.O);
            gameUnderTest.Play(Game.TopRightDiagonal[2], CellContent.X);

            Assert.IsTrue(Game.TopRightDiagonal.All(cellAddress => gameUnderTest.WinningCombination.Contains(cellAddress)));
        }
    }
}
