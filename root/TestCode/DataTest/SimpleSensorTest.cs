using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLib.Interfaces;
using Services.RobotServices;
using Data;
using CommonLib.DTOs;

namespace DataTest
{
    [TestClass]
    public class SimpleSensorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var robot = RobotFactory.GetInstance().CreateRobot();
            var sensor = new SensorDataDTO();
            var sensorModel = SensorModelFactory.GetInstance().CreateSimpleSensorModel();

            //sensorModel.GetProbabilityUltrasonicSensorX(robot.
        }
    }
}
