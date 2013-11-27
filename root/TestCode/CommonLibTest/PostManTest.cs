using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLib;
using CommonLib.Interfaces;
using CommonLib.DTOs;
using CommonLib.NXTPostMan;
using System.Windows.Forms;

namespace CommonLibTest
{
    [TestClass]
    public class PostManTest
    {

        private static TestContext context;
        private INXTPostMan postman;

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
            postman = PostMan.getInstance();
        }

        [TestCleanup]
        public void Cleaner()
        {
            // cleanup after test.
            postman = null;
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
        public void SendMessage_RobotReadyAndAbleMessageWellFormedAndEncoded_MessageRecievedByRobot()
        {
            // Arrange
            ICoordinate cord = new Vector2D(5.0f, 5.0f);
            string encodedCord = NXTEncoder.Encode(cord);
            NXTMessage msg = new NXTMessage(NXTMessageType.MoveToPos, encodedCord);
            DialogResult result;

            // Act
            postman.SendMessage(msg);
            result  = MessageBox.Show("Did the robot recieve move to command ?", "Test Result", MessageBoxButtons.YesNo);

            // Assert
            Assert.IsTrue(result == DialogResult.Yes);
        }


        [TestMethod]
        public void SendMessage_RobotReadyAndAbleCoordinateAsMessage_MessageRecievedByRobot()
        {
            // Arrange
            ICoordinate cord = new Vector2D(5.0f, 5.0f);
            DialogResult result;

            // Act
            ((PostMan)postman).SendMessage(cord);
            result = MessageBox.Show("Did the robot recieve move to command ?", "Test Result", MessageBoxButtons.YesNo);

            // Assert
            Assert.IsTrue(result == DialogResult.Yes);
        }

        [TestMethod]
        public void HasMessageArrived_MessageHasArrived_ReturnTrue()
        {
            // Arrange
            ICoordinate cord = new Vector2D(50.0f, 50.0f);
            ((PostMan)postman).SendMessage(cord);

            // Act
            MessageBox.Show("Press OK when message from robot has arrived?", "User Input", MessageBoxButtons.OK);
            bool result = postman.HasMessageArrived(NXTMessageType.RobotRequestsLocation);

            // Assert
            Assert.IsTrue(result);
        }



    }
}
