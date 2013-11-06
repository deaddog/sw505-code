using System;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;
using CommonLib.Interfaces;
using CommonLib.DTOs;

namespace Services.RobotServices.Mindsqualls
{
    public class MSQRobot : IRobot
    {
        private const byte SERIAL_PORT_NUMBER = 4;
        //private const byte SERIAL_PORT_NUMBER = 3;
        private const int SENSOR_POLL_INTERVAL = 20;
        private const ushort NUMBER_OF_SENSORS = 2;
        private const byte DEFAULT_SENSOR_VALUE = 255;
        private const float NUMBER_OF_TEETH_ON_BOTTOM_WORMGEAR = 1.0F;
        private const float NUMBER_OF_TEETH_ON_BOTTOM_FOLLOWERGEAR = 40.0F;
        private const float NUMBER_OF_TEETH_ON_TOPSIDE_DRIVERGEAR = 24.0F;
        private const float NUMBER_OF_TEETH_ON_TOPSIDE_FOLLOWERGEAR = 40.0F;
        private const float SENSOR_GEAR_RATIO = (NUMBER_OF_TEETH_ON_BOTTOM_FOLLOWERGEAR / NUMBER_OF_TEETH_ON_BOTTOM_WORMGEAR)
            * (NUMBER_OF_TEETH_ON_TOPSIDE_FOLLOWERGEAR / NUMBER_OF_TEETH_ON_TOPSIDE_DRIVERGEAR);
        private const sbyte SENSOR_MOTOR_TURN_POWER = 100;
        private NxtBrick robot;
        private NxtUltrasonicSensor sensor1;
        private NxtUltrasonicSensor sensor2;
        private NxtMotor sensorMotor;

        #region cTor Chain

        public MSQRobot() : this(SERIAL_PORT_NUMBER, SENSOR_POLL_INTERVAL) { }

        public MSQRobot(byte serialPort, int sensorPollInteval) : this(serialPort, sensorPollInteval, new NxtMotor()) { }

        public MSQRobot(byte serialPort, int sensorPollInteval, NxtMotor sensormotor)
        {
            robot = new NxtBrick(NxtCommLinkType.Bluetooth, serialPort);
            
            sensor1 = new NxtUltrasonicSensor();
            sensor2 = new NxtUltrasonicSensor();

            sensor1.PollInterval = sensorPollInteval;
            sensor2.PollInterval = sensorPollInteval;

            robot.Sensor1 = sensor1;
            robot.Sensor2 = sensor2;

            this.sensorMotor = sensormotor;
            robot.MotorC = sensorMotor;
        }
        
        #endregion
        
        public void TurnRobot(uint degrees)
        {
            throw new NotImplementedException();
        }

        public void TurnSensor(uint degrees, bool clockwise)
        {
            uint motordegrees = ActualDegreesToMotorDegrees(degrees, SENSOR_GEAR_RATIO);

            InitializeRobot(true);

            if (clockwise)
            {
                sensorMotor.Run(SENSOR_MOTOR_TURN_POWER, motordegrees);
            }
            else
            {
                sensorMotor.Run(-SENSOR_MOTOR_TURN_POWER, motordegrees);
            }

            FreeRobot(true);
        }

        public void Drive(bool forward, uint distanceInMM)
        {
            throw new NotImplementedException();
        }

        public ISensorData MeasureDistanceUsingSensor()
        {
            byte[] data = new byte[NUMBER_OF_SENSORS];

            InitializeRobot(false);

            sensor1.Poll();
            sensor2.Poll();

            data[0] = sensor1.DistanceCm ?? DEFAULT_SENSOR_VALUE;
            data[1] = sensor2.DistanceCm ?? DEFAULT_SENSOR_VALUE;

            FreeRobot(false);

            return new SensorDataDTO(data);
        }

        private uint ActualDegreesToMotorDegrees(uint degreesToTurn, float gearRatio)
        {
            return (uint)(degreesToTurn * gearRatio);
        }

        private void InitializeRobot(bool usesMotorControl)
        {
            robot.Connect();
            //if (usesMotorControl)
            //{
            //    robot.StartMotorControl();
            //}
        }

        private void FreeRobot(bool usesMotorControl) {
            
            //if(usesMotorControl) {
            //    //robot.StopMotorControl();
            //}
            robot.Disconnect();
        }
    }
}
