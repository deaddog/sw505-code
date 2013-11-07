﻿using System;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;
using CommonLib.Interfaces;
using CommonLib.DTOs;

namespace Services.RobotServices.Mindsqualls
{
    public class MSQRobot : IRobot
    {
        #region Static Variables & Constants.

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

        private const sbyte DRIVE_MOTOR_TURN_POWER = 50;

        private const float NUMBER_OF_TEETH_ON_MOTOR_DRIVEGEAR = 16F;
        private const float NUMBER_OF_TEETH_ON_MOTOR_FOLLOWERGEAR = 40F;
        private const float MOTOR_GEAR_RATIO = NUMBER_OF_TEETH_ON_MOTOR_FOLLOWERGEAR / NUMBER_OF_TEETH_ON_MOTOR_DRIVEGEAR;


        private const float WHEEL_RADIUS_IN_MM = 28F;
        private const float WHEEL_AXEL_LENGTH_IN_MM = 162F;
        private const float WHEEL_CIRCUMFERENCE_IN_MM = WHEEL_RADIUS_IN_MM * 2.0F * (float)Math.PI;

        private const uint DEGREES_IN_CICLE = 360;

        private const sbyte DUMMY_VALUE = 0; // turn ratio not implemented in mindsqualls.

        #endregion 

        private McNxtBrick robot;
        private NxtUltrasonicSensor sensor1;
        private NxtUltrasonicSensor sensor2;
        private NxtMotor sensorMotor;
        private McNxtMotor leftDriveMotor;
        private McNxtMotor rightDriveMotor;
        private McNxtMotorSync driveMotors;

        #region cTor Chain

        public MSQRobot() : this(SERIAL_PORT_NUMBER, SENSOR_POLL_INTERVAL) { }

        public MSQRobot(byte serialPort, int sensorPollInterval) : this(serialPort, sensorPollInterval, new NxtMotor()) { }

        public MSQRobot(byte serialPort, int sensorPollInterval, NxtMotor sensormotor) : 
            this(serialPort, sensorPollInterval, sensormotor, new McNxtMotor(), new McNxtMotor()) { }


        public MSQRobot(byte serialPort, int sensorPollInterval, 
            NxtMotor sensormotor, McNxtMotor leftmotor, McNxtMotor rightmotor)
        {
            robot = new McNxtBrick(NxtCommLinkType.Bluetooth, serialPort);
            
            sensor1 = new NxtUltrasonicSensor();
            sensor2 = new NxtUltrasonicSensor();

            sensor1.PollInterval = sensorPollInterval;
            sensor2.PollInterval = sensorPollInterval;

            robot.Sensor1 = sensor1;
            robot.Sensor2 = sensor2;

            this.sensorMotor = sensormotor;
            robot.MotorC = sensorMotor;

            this.leftDriveMotor = leftmotor;
            this.rightDriveMotor = rightmotor;

            robot.MotorA = this.leftDriveMotor;
            robot.MotorB = this.rightDriveMotor;

            this.driveMotors = new McNxtMotorSync(leftDriveMotor, rightDriveMotor);
        }
        
        #endregion
        
        public void TurnRobot(uint degrees, bool clockwise)
        {
            sbyte forwardTurnPower = DRIVE_MOTOR_TURN_POWER;
            sbyte reverseTurnPower = -DRIVE_MOTOR_TURN_POWER;

            float lengthInMM = (float)((WHEEL_AXEL_LENGTH_IN_MM * Math.PI) / DEGREES_IN_CICLE) * degrees;
            uint MotorDegrees = ConvertMMToMotorDegrees(lengthInMM);

            InitializeRobot(true);

            if (clockwise)
            {
                this.leftDriveMotor.Run(reverseTurnPower, MotorDegrees);
                this.rightDriveMotor.Run(forwardTurnPower, MotorDegrees);
            }
            else
            {
                this.leftDriveMotor.Run(forwardTurnPower, MotorDegrees);
                this.rightDriveMotor.Run(reverseTurnPower, MotorDegrees);
            }
        }

        public void TurnSensor(uint degrees, bool clockwise)
        {
            uint motordegrees = ConvertActualDegreesToMotorDegrees(degrees, SENSOR_GEAR_RATIO);

            InitializeRobot(true);

            if (clockwise)
            {
                sensorMotor.Run(SENSOR_MOTOR_TURN_POWER, motordegrees);
            }
            else
            {
                sensorMotor.Run(-SENSOR_MOTOR_TURN_POWER, motordegrees);
            }

            //FreeRobot(true);
        }

        public void Drive(bool forward, uint distanceInMM)
        {
            ushort distanceInMotorDegrees = (ushort)ConvertMMToMotorDegrees(distanceInMM);

            InitializeRobot(true);

            if (forward)
            {
                driveMotors.Run(-DRIVE_MOTOR_TURN_POWER, distanceInMotorDegrees, DUMMY_VALUE);
            }
            else
            {
                driveMotors.Run(DRIVE_MOTOR_TURN_POWER, distanceInMotorDegrees, DUMMY_VALUE);
            }

            //FreeRobot(true);
        }

        public ISensorData MeasureDistanceUsingSensor()
        {
            //byte[] data = new byte[NUMBER_OF_SENSORS];

            InitializeRobot(false);

            sensor1.Poll();
            sensor2.Poll();

            //data[0] = sensor1.DistanceCm ?? DEFAULT_SENSOR_VALUE;
            //data[1] = sensor2.DistanceCm ?? DEFAULT_SENSOR_VALUE;

            byte dataA = sensor1.DistanceCm ?? DEFAULT_SENSOR_VALUE;
            byte dataB = sensor2.DistanceCm ?? DEFAULT_SENSOR_VALUE;

            //FreeRobot(false);

            return new SensorDataDTO(dataA, dataB);
        }
        
        private uint ConvertMMToMotorDegrees(float distance)
        {
            float motordegrees = (float)ConvertActualDegreesToMotorDegrees(DEGREES_IN_CICLE, MOTOR_GEAR_RATIO);
            return (uint)((motordegrees / WHEEL_CIRCUMFERENCE_IN_MM) * distance);
        }

        private uint ConvertActualDegreesToMotorDegrees(uint degreesToTurn, float gearRatio)
        {
            return (uint)(degreesToTurn * gearRatio);
        }

        private void InitializeRobot(bool usesMotorControl)
        {
            robot.Connect();
            if (usesMotorControl && !robot.IsMotorControlRunning())
            {
                robot.StartMotorControl();
            }
        }

        private void FreeRobot(bool usesMotorControl) {

            if (usesMotorControl && robot.IsMotorControlRunning())
            {
                robot.StopMotorControl();
            }
            robot.Disconnect();
        }
    }
}