using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;

namespace NXTRobot
{
    class Program
    {
        static McNxtBrick brick = new McNxtBrick(NxtCommLinkType.Bluetooth, 6);
        static McNxtMotor leftMotor = new McNxtMotor();
        static McNxtMotor rightMotor = new McNxtMotor();
        static McNxtMotor sensorMotor = new McNxtMotor();
        static NxtUltrasonicSensor sensor = new NxtUltrasonicSensor();
        static UltraSonicData sensorData = new UltraSonicData();

        static void Main(string[] args)
        {
            brick.MotorA = leftMotor;
            brick.MotorB = rightMotor;
            brick.MotorC = sensorMotor;

            brick.Connect();

            GearingMotor gm = new GearingMotor(leftMotor, rightMotor, brick);
            GearingSensor gs = new GearingSensor(sensorMotor, brick);
            gm.ForwardDegrees(50, 1000);
            gs.RotateSensor(100, 360, false);
            

            brick.Disconnect();

            Console.ReadLine();
        }
    }
}
