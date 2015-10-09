using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToe.Domain.Tests
{
    /// <summary>
    /// CellAddress instance tests
    /// </summary>
    [TestClass]
    public class CellAddressTest
    {
        public CellAddressTest()
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IfAttemptIsMadeToCreateAddressWithRowNumberLessThanMinimumThenExceptionIsThrown()
        {
            new CellAddress(CellAddress.MIN_ROW_COLUMN - 1, CellAddress.MIN_ROW_COLUMN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IfAttemptIsMadeToCreateAddressWithRowNumberGreaterThanMaximumThenExceptionIsThrown()
        {
            new CellAddress(CellAddress.MAX_ROW_COLUMN + 1, CellAddress.MIN_ROW_COLUMN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IfAttemptIsMadeToMakeCellAddressWithColumnNumberLessThanMinimumThenExceptionisThrown()
        {
            new CellAddress(CellAddress.MIN_ROW_COLUMN, CellAddress.MIN_ROW_COLUMN - 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IfAttemptIsMadeToMakeCellAddressColumnNumberGreaterThanMaximumThenExceptionIsThrown()
        {
            new CellAddress(CellAddress.MIN_ROW_COLUMN, CellAddress.MAX_ROW_COLUMN + 1);
        }

    }
}
