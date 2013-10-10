using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls;
using NKH.MindSqualls.HiTechnic;
using NKH.MindSqualls.MotorControl;

namespace RotatingSensors
{
    class Program
    {
        static McNxtBrick b = new McNxtBrick(NKH.MindSqualls.NxtCommLinkType.Bluetooth, 10);
        static McNxtMotor rm = new McNxtMotor();
        static NxtUltrasonicSensor u = new NxtUltrasonicSensor();
        static HiTechnicCompassSensor c = new HiTechnicCompassSensor();
      
        static void Main(string[] args)
        {
            b.MotorA = rm;
            b.Sensor1 = c;
            b.Sensor2 = u;

            b.Connect();

            c.PollInterval = 1;
            u.PollInterval = 1;

            b.StartMotorControl();

            c.OnPolled += c_OnPolled;
            u.OnPolled += u_OnPolled;

            bool rotate = true;

            while (rotate)
            {
                Console.WriteLine("Rotate?(y=Enter, n=any)");
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    rm.Run(10, 360);

                    System.Threading.Thread.Sleep(5000);

                    rm.Run(-10, 360);
                }
                else
                    rotate = false;
            }

            b.Disconnect();
        }

        static void u_OnPolled(NxtPollable polledItem)
        {
            Console.WriteLine("Afstand: " + u.DistanceCm + "\n");
        }

        static void c_OnPolled(NxtPollable polledItem)
        {
            Console.WriteLine("Compass: " + c.Heading + "\n");
        }
    }
}