using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls;

namespace TestSensor
{
    class Program
    {
        
        //Press any key for result, enter for return

        static void Main(string[] args)
        {
            NxtBrick b = new NxtBrick(NxtCommLinkType.Bluetooth, 10);
            NxtUltrasonicSensor u = new NxtUltrasonicSensor();

            b.Sensor1 = u;

            u.PollInterval = 20;

            b.Connect();

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    return;
                }
                else
                {
                    Console.WriteLine(u.DistanceCm.ToString());
                }
            }

        }
    }
}
