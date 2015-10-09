﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.IsTrue(gameUnderTest.CellStatuses.All(status => status == CellStatus.Unmarked));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IfAttemptIsMadeToSetCellStatusToUnmarkedThenAnExceptionIsThrown()
        {
            gameUnderTest.Play(firstCellAddress, CellStatus.Unmarked);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void IfAttemptIsMadeToMarkACellThatIsAlreadyMarkedThenExceptionIsThrown()
        {
            gameUnderTest.Play(firstCellAddress, CellStatus.X);
            gameUnderTest.Play(firstCellAddress, CellStatus.O);
        }

        [TestMethod]
        public void AfterFirstPlayGameStatusBecomesInProgress()
        {
            gameUnderTest.Play(firstCellAddress, CellStatus.X);
            Assert.IsTrue(gameUnderTest.Status == GameStatus.InProgress);
        }
    }
}
