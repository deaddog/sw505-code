using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls.MotorControl;

namespace NXTRobot
{
    public class GearingMotor : McNxtMotor
    {
        private McNxtMotor leftMotor;
        private McNxtMotor rightMotor;
        private McNxtMotorSync motors;
        private McNxtBrick brick;

        public GearingMotor(McNxtMotor leftMotor, McNxtMotor rightMotor, McNxtBrick brick) : base() 
        {
            this.leftMotor = leftMotor;
            this.rightMotor = rightMotor;
            this.brick = brick;

            if (!brick.IsMotorControlRunning())
            {
                brick.StartMotorControl();
            }

            motors = new McNxtMotorSync(leftMotor, rightMotor);
        }

        public McNxtBrick Brick
        {
            get { return brick; }
        }
        public McNxtMotorSync Motors
        {
            get { return motors; }
        }
        public McNxtMotor RightMotor
        {
            get { return rightMotor; }
        }
        public McNxtMotor LeftMotor
        {
            get { return leftMotor; }
        }
        
        public static double WheelRadiusMM { get { return 28; } }
        public static double WheelAxelLengthMM { get { return 162; } }
        public static double MotorsGearRatio { get { return 16.0 / 40.0; } }
        public static double WheelCircumferenceMM { get { return (WheelRadiusMM * 2.0 * Math.PI); } }

        public static uint ActualDegreesToMotorDegrees(uint degreesToTurn, double gearRatio)
        {
            return (uint)(degreesToTurn / gearRatio);
        }

        public static uint MMToMotorDegrees(double distanceMM)
        {
            return (ActualDegreesToMotorDegrees(360, MotorsGearRatio) / (uint)WheelCircumferenceMM) * (uint)distanceMM;
        }

        public void ReverseDegrees(sbyte power, ushort degrees)
        {
            sbyte reverse = power;
            motors.Run(reverse, (ushort)ActualDegreesToMotorDegrees(degrees, MotorsGearRatio), 0);
        }

        public void ForwardDegrees(sbyte power, ushort degrees)
        {
            sbyte forward = (sbyte)(-1 * power);
            motors.Run(forward, (ushort)ActualDegreesToMotorDegrees(degrees, MotorsGearRatio), 0);
        }

        public void ReverseMM(sbyte power, ushort distanceMM)
        {
            sbyte reverse = power;
            motors.Run(reverse, (ushort)MMToMotorDegrees(distanceMM), 0);
        }

        public void ForwardMM(sbyte power, ushort distanceMM)
        {
            sbyte forward = (sbyte)(-1 * power);
            motors.Run(forward, (ushort)MMToMotorDegrees(distanceMM), 0);
        }

        public void RotateRobot(sbyte power, uint degrees, bool clockwise)
        {
            sbyte forward = power;
            sbyte reverse = (sbyte)(-1 * power);

            double length = ((WheelAxelLengthMM * Math.PI) / 360.0) * degrees;

            if (clockwise)
            {
                leftMotor.Run(reverse, MMToMotorDegrees(length));
                rightMotor.Run(forward, MMToMotorDegrees(length));
            }
            else
            {
                leftMotor.Run(forward, MMToMotorDegrees(length));
                rightMotor.Run(reverse, MMToMotorDegrees(length));
            }
        }

    }
}
