using System;
using System.Collections.Generic;
using System.IO;
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
            FileStream fs = new FileStream("results.txt", FileMode.Create);
            var w = new StreamWriter(fs);
            w.AutoFlush = true;
            Console.SetOut(w);
            Console.SetError(w);


            NxtBrick b = new NxtBrick(NxtCommLinkType.Bluetooth, 10);
            NxtUltrasonicSensor ul = new NxtUltrasonicSensor();
            

            b.Sensor1 = ul;

            ul.PollInterval = 20;

            b.Connect();

            string distance;
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    b.Disconnect();
                    return;
                }
                else
                {
                    Console.WriteLine("Distance in cm?");
                    distance = Console.ReadLine();
                    Console.WriteLine("Actual distance(cm): " + distance);
                    Console.WriteLine("Ultrasonic sensors measured distance(cm): " + ul.DistanceCm.ToString());
                    Console.WriteLine("\n");
                }
            }

        }
    }
}
