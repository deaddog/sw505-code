using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;
using NXTRobot;

namespace RobotCommand
    
{
    class Program
    {
        static McNxtMotor mc = new McNxtMotor();
        static McNxtBrick brick = new McNxtBrick(NxtCommLinkType.Bluetooth, 8);

        static void Main(string[] args)
        {
            brick.MotorA = mc;

            brick.Connect();

            GearingSensor gs = new GearingSensor(mc, brick);
            /*
            NxtControl.NxtTone(brick.CommLink, 1);

            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine(NxtControl.NxtSensorReading(brick.CommLink));
            }*/

            gs.RotateSensor(100, 1000000, true);

            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine(NxtControl.NxtSensorReading(brick.CommLink));
            }
            
        }
    }
}
