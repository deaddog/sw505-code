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
        static McNxtBrick brick = new McNxtBrick(NxtCommLinkType.Bluetooth, 10);
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

            RobotControl robot = new RobotControl(brick, leftMotor, rightMotor, sensorMotor, sensor, sensorData);

            //robot.RotateSensor(100, 90, false);
            for (int i = 0; i < 10; i++)
            {
                robot.Left(MotorControlMotorPort.PortA, (uint)5, true);
                Thread.Sleep(1000);
                robot.Right(MotorControlMotorPort.PortB, (uint)5, false);
                Thread.Sleep(1000);
                i++;
            }
            
            //robot.ForwardMM(50, 50);

            //robot.ForwardDegrees(100, 100);

            brick.Disconnect();

            Console.ReadLine();
        }
    }
}
