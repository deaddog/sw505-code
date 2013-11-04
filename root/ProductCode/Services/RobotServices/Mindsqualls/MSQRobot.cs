using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;

namespace Services.RobotServices.Mindsqualls
{
    public class MSQRobot : IRobot
    {
        private const byte SERIAL_PORT_NUMBER = 10;
        private const int SENSOR_POLL_INTERVAL = 20;
        private NxtBrick robot;  

        #region cTor Chain

        public MSQRobot() : this(SERIAL_PORT_NUMBER, SENSOR_POLL_INTERVAL) { }

        public MSQRobot(byte serialPort, int sensorPollInteval)
        {
            robot = new NxtBrick(NxtCommLinkType.Bluetooth, serialPort);

            NxtUltrasonicSensor sensor1 = new NxtUltrasonicSensor();
            NxtUltrasonicSensor sensor2 = new NxtUltrasonicSensor();

            sensor1.PollInterval = sensorPollInteval;
            sensor2.PollInterval = sensorPollInteval;

            robot.Sensor1 = sensor1;
            robot.Sensor2 = sensor2;

            robot.Connect();
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
            


            throw new NotImplementedException();
        }
    }
}
