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
        private static MotorControlMotorPort port;
        private static McNxtBrick brick;
        public GearingMotor() : base() 
        {
            port = this.Port;
            brick = this.Brick;
        }

        public MotorControlMotorPort Port
        {
            get { return port; }
            set { port = value; }
        }


        public McNxtBrick Brick
        {
            get { return brick; }
            set { brick = value; }
        }
        
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
            double length = (((WheelAxelLengthMM * Math.PI) / 360.0) * degressToTurn) * 2;
            string l = MMToMotorDegrees(length).ToString();

            switch (l.Length)
            {
                case 0:
                    return "0";
                case 1:
                    return "00000" + l;
                case 2:
                    return "0000" + l;
                case 3:
                    return "000" + l;
                case 4:
                    return "00" + l;
                case 5:
                    return "0" + l;
                default:
                    throw new Exception("TachoLimit value is to big, has to be between 0-999999");
            }
        }

        public override void Run(uint degrees, bool forward)
        {
            if (forward)
            {
                MotorControlProxy.CONTROLLED_MOTORCMD(brick.CommLink, port, "200", TachoLimitToString(degrees), '5');
            }
            else
            {
                MotorControlProxy.CONTROLLED_MOTORCMD(brick.CommLink, port, "200", TachoLimitToString(degrees), '5');
            }
        }

    }
}
