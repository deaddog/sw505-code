using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Control;
using System.Windows.Forms;
using CommonLib.Interfaces;
using CommonLib.DTOs;

namespace ControlTest
{
    [TestClass]
    public class ScanningControlTest
    {
        private static TestContext context;
        private ScanningControl sc;

        #region Initializers

        [ClassInitialize]
        public static void ClassInitializer(TestContext c)
        {
            context = c;
        }

        [ClassCleanup]
        public static void ClassCleaner()
        {
            context = null;
        }

        [TestInitialize]
        public void Initializer()
        {
            sc = new ScanningControl();
        }

        [TestCleanup]        public void Cleaner()
        {
            // cleanup after test.
            sc = null;
        }


        #endregion


        [TestMethod]
        public void Template_StateUnderTest_ExpectedResult()
        {
            // Arrange


            // Act


            // Assert

            Assert.Inconclusive();
        }
    }
}
