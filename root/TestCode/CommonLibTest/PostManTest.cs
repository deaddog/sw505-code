using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLib;
using CommonLib.Interfaces;
using CommonLib.DTOs;
using CommonLib.NXTPostMan;

namespace CommonLibTest
{
    [TestClass]
    public class PostManTest
    {

        private static TestContext context;
        
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
 
        }

        [TestCleanup]
        public void Cleaner()
        {
            // cleanup after test.
  
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
