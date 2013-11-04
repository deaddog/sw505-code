using System;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;
using CommonLib.Interfaces;
using CommonLib.DTOs;

namespace Services.RobotServices.Mindsqualls
{
    public class MSQRobot : IRobot
    {
        private const byte SERIAL_PORT_NUMBER = 5;
        private const int SENSOR_POLL_INTERVAL = 20;
        private const ushort NUMBER_OF_SENSORS = 2;
        private const byte DEFAULT_SENSOR_VALUE = 255;
        private NxtBrick robot;
        private NxtUltrasonicSensor sensor1;
        private NxtUltrasonicSensor sensor2;

        #region cTor Chain

        public MSQRobot() : this(SERIAL_PORT_NUMBER, SENSOR_POLL_INTERVAL) { }

        public MSQRobot(byte serialPort, int sensorPollInteval)
        {
            robot = new NxtBrick(NxtCommLinkType.Bluetooth, serialPort);

            sensor1 = new NxtUltrasonicSensor();
            sensor2 = new NxtUltrasonicSensor();

            sensor1.PollInterval = sensorPollInteval;
            sensor2.PollInterval = sensorPollInteval;

            robot.Sensor1 = sensor1;
            robot.Sensor2 = sensor2;
        }
        
        #endregion

        public void TurnRobot(uint degrees)
        {
            throw new NotImplementedException();
        }

        public void TurnSensor(uint degrees)
        {
            throw new NotImplementedException();
        }

        public void Drive(bool forward, uint distanceInMM)
        {
            throw new NotImplementedException();
        }

        public ISensorData MeasureDistanceUsingSensor()
        {
            byte[] data = new byte[NUMBER_OF_SENSORS];

            robot.Connect();

            data[0] = sensor1.DistanceCm ?? DEFAULT_SENSOR_VALUE;
            data[1] = sensor2.DistanceCm ?? DEFAULT_SENSOR_VALUE;

            robot.Disconnect();
            return new SensorDataDTO(data);
        }
    }
}
