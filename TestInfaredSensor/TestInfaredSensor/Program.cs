using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls;

namespace TestInfaredSensor
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("results.txt", FileMode.Create);
            var w = new StreamWriter(fs);
            w.AutoFlush = true;
            //Console.SetOut(w);
            //Console.SetError(w);

            NxtBrick b = new NxtBrick(NxtCommLinkType.Bluetooth, 10);
            NxtUltrasonicSensor u = new NxtUltrasonicSensor();

            b.Sensor1 = u;
            u.PollInterval = 20;

            b.Connect();

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    b.Disconnect();
                        return;
                }
                else
                {
                    Console.WriteLine(u.DistanceCm);
                }
            }

        }
    }
}
