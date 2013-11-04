using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.RobotServices;
using Services.RobotServices.Mindsqualls;

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

    }
}
