using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;

namespace NXTRobot
{
    class RobotControl
    {
        private McNxtBrick brick;
        private McNxtMotor leftMotor;
        private McNxtMotor rightMotor;
        private McNxtMotor sensorMotor;
        private NxtUltrasonicSensor sensor;
        private UltraSonicData sensorData;
        private McNxtMotorSync motors;

        public static class Constants
        {
            public static double WheelRadiusMM { get { return 28; } }
            public static double WheelAxelLengthMM { get { return 162; } }
            public static double MotorsGearRatio { get { return 16.0 / 40.0; } }
            public static double SensorMotorGearRatio { get { return (1.0 / 40.0) * (24.0 / 56.0); } }
            public static double WheelCircumferenceMM { get { return (WheelRadiusMM * 2.0 * Math.PI); } }

            public static uint ActualDegreesToMotorDegrees(uint degreesToTurn, double gearRatio)
            {
                return (uint)(degreesToTurn / gearRatio);
            }

            public static uint MMToMotorDegrees(double distanceMM)
            {
                return (ActualDegreesToMotorDegrees(360, MotorsGearRatio) / (uint)WheelCircumferenceMM) * (uint)distanceMM;
            }

            public static string TachoLimitToString(uint degressToTurn)
            {
                double length = (((WheelAxelLengthMM * Math.PI) / 360.0) * degressToTurn)*2;
                string l = MMToMotorDegrees(length).ToString();

                switch (l.Length)
                {
                    case 0 :
                        return "0";
                    case 1 :
                        return "00000" + l;
                    case 2 :
                        return "0000" + l;
                    case 3 :
                        return "000" + l;
                    case 4:
                        return "00" + l;
                    case 5 :
                        return "0" + l;
                    default:
                        throw new Exception("TachoLimit value is to big, has to be between 0-999999");
                }
            }
        }

        public RobotControl(McNxtBrick _brick, McNxtMotor _leftMotor, McNxtMotor _rightMotor, McNxtMotor _sensorMotor, NxtUltrasonicSensor _sensor, UltraSonicData _sensorData)
        {
            brick = _brick;
            leftMotor = _leftMotor;
            rightMotor = _rightMotor;
            sensorMotor = _sensorMotor;
            sensor = _sensor;
            sensorData = _sensorData;

            motors = new McNxtMotorSync(leftMotor, rightMotor);

            if (!brick.IsMotorControlRunning())
            {
                brick.StartMotorControl();
            }

            //leftMotor.PollInterval = 1;
            //leftMotor.OnPolled += leftMotor_OnPolled;

            //rightMotor.PollInterval = 1;
            //rightMotor.OnPolled += rightMotor_OnPolled;

            sensorMotor.PollInterval = 1;
            sensorMotor.OnPolled += sensorMotor_OnPolled;
        }

        void rightMotor_OnPolled(NxtPollable polledItem)
        {
            Console.WriteLine("Right motor tachocount: " + rightMotor.TachoCount);
        }

        void leftMotor_OnPolled(NxtPollable polledItem)
        {
            Console.WriteLine("Left motor tachocount: " + leftMotor.TachoCount);
        }

        void sensorMotor_OnPolled(NxtPollable polledItem)
        {
            Console.WriteLine("Sensor motor tachocount: " + sensorMotor.TachoCount);
        }

        public void ReverseDegrees(sbyte power, ushort degrees)
        {
            sbyte reverse = power;
            motors.Run(reverse, (ushort)Constants.ActualDegreesToMotorDegrees(degrees, Constants.MotorsGearRatio), 0);
        }

        public void ForwardDegrees(sbyte power, ushort degrees)
        {
            sbyte forward = (sbyte)(-1 * power);
            motors.Run(forward, (ushort)Constants.ActualDegreesToMotorDegrees(degrees, Constants.MotorsGearRatio), 0);
        }

        public void ReverseMM(sbyte power, ushort distanceMM)
        {
            sbyte reverse = power;
            motors.Run(reverse, (ushort)Constants.MMToMotorDegrees(distanceMM), 0);
        }

        public void ForwardMM(sbyte power, ushort distanceMM)
        {
            sbyte forward = (sbyte)(-1 * power);
            motors.Run(forward, (ushort)Constants.MMToMotorDegrees(distanceMM), 0);
        }

        public void Left(MotorControlMotorPort port, uint degrees, bool forward)
        {
            if (forward)
            {
                MotorControlProxy.CONTROLLED_MOTORCMD(brick.CommLink, port, "200", Constants.TachoLimitToString(degrees), '5');
            }
            else
            {
                MotorControlProxy.CONTROLLED_MOTORCMD(brick.CommLink, port, "100", Constants.TachoLimitToString(degrees), '5');
            }
        }

        public void Right(MotorControlMotorPort port, uint degrees, bool forward)
        {
            if (forward)
            {
                MotorControlProxy.CONTROLLED_MOTORCMD(brick.CommLink, port, "200", Constants.TachoLimitToString(degrees), '5');
            }
            else
            {
                MotorControlProxy.CONTROLLED_MOTORCMD(brick.CommLink, port, "100", Constants.TachoLimitToString(degrees), '5');
            }
        }

        public void RotateSensor(sbyte power, uint degrees, bool clockwise)
        {
            sbyte forward = power;
            sbyte reverse = (sbyte)(-1 * power);

            if (clockwise)
            {
                sensorMotor.Run(forward, Constants.ActualDegreesToMotorDegrees((ushort)degrees, Constants.SensorMotorGearRatio));
            }
            else
            {
                sensorMotor.Run(reverse, Constants.ActualDegreesToMotorDegrees((ushort)degrees, Constants.SensorMotorGearRatio));
            }
        }

        public void TurnLeft()
        {
            throw new NotImplementedException();
        }

        public void TurnRight()
        {
            throw new NotImplementedException();
        }

        public void Get360DegreeSensorData()
        {
            // Se UltraSonicControl klassen for de gamle (dårlige) implementatioer :P
            // Brug UltraSonicData datastrukturen til at gemme sensor data med...
            throw new NotImplementedException();
        }
    }
}
