using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls.MotorControl;

namespace NXTRobot
{
    public class GearingSensor : McNxtMotor
    {
        public GearingSensor(McNxtMotor motor, McNxtBrick brick)
        {
            this.motor = motor;
            this.brick = brick;

            if (!brick.IsMotorControlRunning())
            {
                brick.StartMotorControl();
            }
        }

        private McNxtBrick brick;

        public McNxtBrick Brick
        {
            get { return brick; }
            set { brick = value; }
        }
        

        private McNxtMotor motor;

        public McNxtMotor Motor
        {
            get { return motor; }
            set { motor = value; }
        }

        public static double SensorMotorGearRatio { get { return (1.0 / 40.0) * (24.0 / 56.0); } }

        public static uint ActualDegreesToMotorDegrees(uint degreesToTurn, double gearRatio)
        {
            return (uint)(degreesToTurn / gearRatio);
        }

        public void RotateSensor(sbyte power, uint degrees, bool clockwise)
        {
            sbyte forward = power;
            sbyte reverse = (sbyte)(-1 * power);

            if (clockwise)
            {
                motor.Run(forward, ActualDegreesToMotorDegrees((ushort)degrees, SensorMotorGearRatio));
            }
            else
            {
                motor.Run(reverse, ActualDegreesToMotorDegrees((ushort)degrees, SensorMotorGearRatio));
            }
        }
        
    }
}
