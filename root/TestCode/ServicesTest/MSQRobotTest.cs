using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.RobotServices;
using Services.RobotServices.Mindsqualls;
using CommonLib.DTOs;
using CommonLib.Interfaces;
using System.Windows.Forms;

namespace ServicesTest
{
    [TestClass]
    public class MSQRobotTest
    {
        private static TestContext context;
        private RobotFactory factory;

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
            factory = RobotFactory.GetInstance();
        }

        [TestCleanup]
        public void Cleaner()
        {
            // cleanup after test.
            factory = null;
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

        [TestMethod]
        public void MoveToPosition_BogusPosition()
        {
            //Arrange
            IRobot rob = factory.CreateRobot();
            Vector2D bogusPosition = new Vector2D(5.1f, 7.5f);

            DialogResult result;

            //Act
            rob.MoveToPosition(bogusPosition);
            result = MessageBox.Show("Did the display show the message \"BogusPosition\" ?", "Test Result", MessageBoxButtons.YesNo);

            //Assert
            Assert.IsTrue(result == DialogResult.Yes);
        }
        
        //[TestMethod]
        //public void DoStuff()
        //{
        //    // Arrange
        //    IRobot rob = factory.createRobot();
        //    const uint DRIVE_DISTANCE = 900;
        //    const bool FORWARD = false;
        //    DialogResult result;

        //    // Act
        //    rob.Drive(FORWARD, DRIVE_DISTANCE);
        //    result = MessageBox.Show("Did the robot Drive 30cm forward ?", "Test Result", MessageBoxButtons.YesNo);
        //    rob.TurnRobot(90, true);
        //    result = MessageBox.Show("Did the robot Drive 30cm forward ?", "Test Result", MessageBoxButtons.YesNo);
        //    rob.Drive(FORWARD, 500);
        //    // Assert
        //    result = MessageBox.Show("Did the robot Drive 30cm forward ?", "Test Result", MessageBoxButtons.YesNo);
        //    Assert.IsTrue(result == DialogResult.Yes);
        //}

    }
}
