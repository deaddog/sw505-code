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
            public static double WheelCircumferenceMM { get { return (Constants.WheelRadiusMM * 2.0 * Math.PI); } }

            public static uint ActualDegreesToMotorDegrees(uint degreesToTurn, double gearRatio)
            {
                return (uint)(degreesToTurn / gearRatio);
            }

            public static uint MMToMotorDegrees(double distanceMM)
            {
                return (ActualDegreesToMotorDegrees(360, MotorsGearRatio) / (uint)WheelCircumferenceMM) * (uint)distanceMM;
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

        public void RotateRobot(sbyte power, uint degrees, bool clockwise)
        {
            sbyte forward = power;
            sbyte reverse = (sbyte)(-1 * power);

            double length = ((Constants.WheelAxelLengthMM * Math.PI) / 360.0) * degrees;

            if (clockwise)
            {
                leftMotor.Run(reverse, Constants.MMToMotorDegrees(length));
                rightMotor.Run(forward, Constants.MMToMotorDegrees(length));
            }
            else
            {
                leftMotor.Run(forward, Constants.MMToMotorDegrees(length));
                rightMotor.Run(reverse, Constants.MMToMotorDegrees(length));
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
