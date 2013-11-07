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


        [TestMethod]
        public void FullSweep_AnObjectIsWithinSensorRangeAndRobotReadyWithSensorAtBearing000_RobotPerformsFull360SensorSweepAndReturnsData()
        {
            // Arrange
            const ushort DEFAULT_SENSOR_VALUE = 255;
            DialogResult result;
            bool containsData = false;

            // Act
            SensorSweepDTO data = ScanningControl.FullSweep();
            result = MessageBox.Show("Did the sensor turn 360 degrees ?", "Test Result", MessageBoxButtons.YesNo);
            
            // Assert
            
            Assert.IsInstanceOfType(data, typeof(SensorSweepDTO));

            foreach (ISensorData d in data)
            {
                if (d.SensorADistance != DEFAULT_SENSOR_VALUE || d.SensorBDistance != DEFAULT_SENSOR_VALUE)
                {
                    containsData = true;
                }
            }
            Assert.IsTrue(containsData);

            Assert.IsTrue(result == DialogResult.Yes);
        }
    }
}
