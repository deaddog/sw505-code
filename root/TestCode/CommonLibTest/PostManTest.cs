﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLib;
using CommonLib.Interfaces;
using CommonLib.DTOs;
using CommonLib.NXTPostMan;
using System.Windows.Forms;
using CommonLib;

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
            postman = PostMan.Instance;
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
            byte[] byteMsg = NXTEncoder.ByteEncode(cord);
            NXTMessage msg = new NXTMessage(NXTMessageType.MoveToPos, encodedCord,byteMsg);
            DialogResult result;

            // Act
            postman.SendMessage(msg);
            result  = MessageBox.Show("Did the robot recieve move to command ?", "Test Result", MessageBoxButtons.YesNo);

            // Assert
            Assert.IsTrue(result == DialogResult.Yes);
        }

    }
}
