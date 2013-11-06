﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.RobotServices;
using Services.RobotServices.Mindsqualls;
using CommonLib.Interfaces;
using System.Windows.Forms;

namespace ServicesTest
{
    [TestClass]
    public class MSQRobotTest
    {
        private static TestContext context;
        private RobotFactory factory;

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
            factory = RobotFactory.getInstance();
        }

        [TestCleanup]
        public void Cleaner()
        {
            // cleanup after test.
            factory = null;
        }

        [TestMethod]
        public void Template_StateUnderTest_ExpectedResult()
        {
            // Arrange


            // Act


            // Assert

            Assert.Inconclusive();
        }

        [TestMethod]
        public void MeasureDistanceUsingSensor_RobotReadyAndAble_ValidSensorDataOfTypeISensorData()
        {
            // Arrange
            MSQRobot rob = (MSQRobot)factory.createRobot();
            
            // Act
            ISensorData data = rob.MeasureDistanceUsingSensor();

            // Assert
            Assert.IsInstanceOfType(data, typeof(ISensorData));
        }

        [TestMethod]
        public void MeasureDistanceUsingSensor_TargetAtDistance30To50CmFromSensor_SensorDataWithMeasurementBetween30And50Cm()
        {
            // Arrange
            IRobot rob = factory.createRobot();
            ISensorData data;
                
            // Act
            data = rob.MeasureDistanceUsingSensor();

            // Assert
            Assert.IsNotNull(data);
            Assert.IsTrue((data.Distance[0] < 50 && data.Distance[0] > 30) || 
                (data.Distance[1] < 50 && data.Distance[1] > 30));
        }

        [TestMethod]
        public void TurnSensor_RobotReadyAndAble_SensorTurns90Degrees()
        {
            // Arrange
            IRobot rob = factory.createRobot();
            DialogResult result;
            bool clockwise = true;

            // Act           
            rob.TurnSensor(90, clockwise);
            result = MessageBox.Show("Did the robot sensor turn 90 degrees clockwise ?", "Test Result", MessageBoxButtons.YesNo);

            // Assert
            Assert.IsTrue(result == DialogResult.Yes);
        }

    }
}
