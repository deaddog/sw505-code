using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls;
using NKH.MindSqualls.HiTechnic;

namespace TestSensor
{
    class Program
    {
        
        //Press any key for result, enter for return
        //Ultrasonic sensor in Port 1 &/or 4
        //Compass in Port 2

        static void Main(string[] args)
        {
            NxtBrick b = new NxtBrick(NxtCommLinkType.Bluetooth, 10);
            NxtUltrasonicSensor ul = new NxtUltrasonicSensor();
            NxtUltrasonicSensor ur = new NxtUltrasonicSensor();
            HiTechnicCompassSensor c = new HiTechnicCompassSensor();
            

            b.Sensor1 = ur;
            b.Sensor4 = ul;
            b.Sensor2 = c;

            ul.PollInterval = 20;
            ur.PollInterval = 20;
            c.PollInterval = 20;

            b.Connect();

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    b.Disconnect();
                    return;
                }
                else
                {
                    Console.WriteLine("ul = " + ul.DistanceCm.ToString());
                    Console.WriteLine("ur = " + ur.DistanceCm.ToString());
                    Console.WriteLine("c = " + c.TwoDegreeHeading().ToString());
                    Console.WriteLine("\n");
                }
            }

        }
    }
}
