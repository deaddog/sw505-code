using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls;
using NKH.MindSqualls.HiTechnic;

namespace TestInfaredSensor
{
    class Program
    {
        static MindsensorsHPMRInfrared u;
        static StringBuilder sb;

        static void Main(string[] args)
        {
            sb = new StringBuilder();


            NxtBrick b = new NxtBrick(NxtCommLinkType.Bluetooth, 10);
            u = new MindsensorsHPMRInfrared();

            b.Sensor1 = u;

            b.Connect();

            //u.PollInterval = 500;
            u.OnPolled += u_OnPolled;

            while (true)
            {
                Console.WriteLine("distance:");
                sb.Append(Console.ReadLine() + " ");

                ConsoleKey k = Console.ReadKey(true).Key;

                if (k == ConsoleKey.Enter)
                    u.Poll();
                else if (k == ConsoleKey.Escape)
                {
                    writeFile();
                    break;
                }

            }
        }

        static void writeFile()
        {
            using (StreamWriter outfile = new StreamWriter("results"))
            {
                outfile.Write(sb.ToString());
            }
        }

        static void u_OnPolled(NxtPollable polledItem)
        {
            Console.WriteLine(u.Distance);
            sb.Append(u.Distance);
            sb.AppendLine();
        }
    }
}
