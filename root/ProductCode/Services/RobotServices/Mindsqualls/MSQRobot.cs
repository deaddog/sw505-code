using System;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;
using CommonLib.Interfaces;
using CommonLib.DTOs;
using System.Threading;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Services.RobotServices.Mindsqualls
{
    public class MSQRobot : IRobot
    {
        #region Static Variables & Constants.

        private const byte SERIAL_PORT_NUMBER = 16;
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

        // Used for sending/receiving commands to/from NXT
        private const NxtMailbox2 PC_INBOX = NxtMailbox2.Box0;
        private const NxtMailbox PC_OUTBOX = NxtMailbox.Box1;

        #endregion

        private McNxtBrick robot;
        private NxtUltrasonicSensor sensor1;
        private NxtUltrasonicSensor sensor2;
        private McNxtMotor sensorMotor;
        private McNxtMotor leftDriveMotor;
        private McNxtMotor rightDriveMotor;
        private McNxtMotorSync driveMotors;

        private IPose currentPose;

        private bool stopMailcheckerThread = false;

        #region cTor Chain

        public MSQRobot() : this(SERIAL_PORT_NUMBER, SENSOR_POLL_INTERVAL) { }

        public MSQRobot(byte serialPort, int sensorPollInterval) : this(serialPort, sensorPollInterval, new McNxtMotor()) { }

        public MSQRobot(byte serialPort, int sensorPollInterval, McNxtMotor sensormotor) :
            this(serialPort, sensorPollInterval, sensormotor, new McNxtMotor(), new McNxtMotor()) { }


        public MSQRobot(byte serialPort, int sensorPollInterval,
            McNxtMotor sensormotor, McNxtMotor leftmotor, McNxtMotor rightmotor)
        {
            robot = new McNxtBrick(NxtCommLinkType.Bluetooth, serialPort);

            sensor1 = new NxtUltrasonicSensor();
            sensor2 = new NxtUltrasonicSensor();

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

        /// <summary>
        /// Sends command to robot, telling it to go to <paramref name="position"/>
        /// Also starts thread, checking for replies
        /// </summary>
        /// <param name="position">The position to go to</param>
        public void MoveToPosition(ICoordinate position)
        {
            InitializeRobot(true);

            string encodedPosition = NXTEncoder.Encode(position);

            //Sends command to robot with the position param
            string toSendMessage = String.Format("{0}{1}", (byte)OutgoingCommand.MoveToPos, encodedPosition);
            robot.CommLink.MessageWrite(PC_OUTBOX, toSendMessage);

            //Thread being run, checking inbox every 10 ms
            stopMailcheckerThread = false;
            Thread mailChecker = new Thread(CheckIncoming);
            mailChecker.Start();

            //FreeRobot(true);
        }

        private ISensorData sensorData;
        public ISensorData GetSensorData()
        {
            InitializeRobot(true);

            string toSendMessage = String.Format("{0}", (byte)OutgoingCommand.GetSensorData);
            robot.CommLink.MessageWrite(PC_OUTBOX, toSendMessage);

            stopMailcheckerThread = false;
            Thread mailChecker = new Thread(CheckIncoming);
            mailChecker.Start();

            return sensorData;
        }


        /// <summary>
        /// Updates the pose.
        /// </summary>
        /// <param name="pose">The pose.</param>
        public void UpdatePose(IPose pose)
        {
            this.currentPose = pose;
        }

        private void CheckIncoming()
        {
            //Keeps checking until stopMailcheckerThread is deemed false
            //   which only happens when robot sends back command RobotHasArrivedAtDestination
            while (!stopMailcheckerThread)
            {
                try
                {
                    Thread.Sleep(10);

                    //Checking the mailbox, if empty, exception is ignored and loop is reset
                    byte[] reply = robot.CommLink.MessageReadToBytes(PC_INBOX, NxtMailbox.Box0, true);
                    
                    //Attempt to parse the incoming command as the IncomingCommand enum
                    //  if failed, simple reset the loop via ArgumentException
                    //  if all is well, do switch on the command and do the corresponding action
                    IncomingCommand cmd = (IncomingCommand)Enum.Parse(typeof(IncomingCommand), reply[0].ToString());
                    switch (cmd)
                    {
                        case IncomingCommand.RobotRequestsLocation:
                            SendRobotItsPose(currentPose);
                            break;
                        case IncomingCommand.RobotHasArrivedAtDestination:
                            stopMailcheckerThread = true;
                            Console.WriteLine("Destination reached!");
                            Console.ReadKey();
                            break;
                        case IncomingCommand.GetSensorData:
                            sensorData = new SensorDataDTO();
                            sensorData.SensorFront = reply[0];
                            sensorData.SensorBack = reply[1];
                            sensorData.SensorRight = reply[2];
                            sensorData.SensorLeft = reply[3];

                            //For testing of distance:
                            //foreach (var item in sensorData)
                            //{
                            //    Console.WriteLine("s1: " + item.SensorADistance + " s2: " + item.SensorBDistance);
                            //}

                            Console.WriteLine("Measured!");
                            Console.ReadKey();
                            break;
                        default:
                            continue;
                    }
                }
                catch (ArgumentException ex)
                {
                    continue;
                }
                catch (NxtCommunicationProtocolException ex)
                {
                    if (ex.ErrorMessage != NxtErrorMessage.SpecifiedMailboxQueueIsEmpty) throw;
                }
            }
        }

        private void SendRobotItsPose(IPose pose)
        {
            string encodedPose = NXTEncoder.Encode(pose);
            string message = String.Format("{0}{1}", (byte)IncomingCommand.RobotRequestsLocation, encodedPose);
            robot.CommLink.MessageWrite(PC_OUTBOX, message);
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

        private void InitializeRobot(bool usesMotorControl=true)
        {
            robot.Connect();

            if (usesMotorControl && !robot.IsMotorControlRunning())
            {
                robot.StartMotorControl();
            }
        }

        private void FreeRobot(bool usesMotorControl)
        {

            if (usesMotorControl && robot.IsMotorControlRunning())
            {
                robot.StopMotorControl();
            }
            robot.Disconnect();
        }
    }
}
