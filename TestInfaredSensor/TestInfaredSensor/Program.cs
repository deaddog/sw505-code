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
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("results.txt", FileMode.Create);
            var w = new StreamWriter(fs);
            w.AutoFlush = true;
            //Console.SetOut(w);
            //Console.SetError(w);

            NxtBrick b = new NxtBrick(NxtCommLinkType.Bluetooth, 10);
            NxtUltrasonicSensor u = new NxtUltrasonicSensor();


            //NxtBluetoothConnection bc = new NxtBluetoothConnection("COM10");
            //bc.Connect();

            b.Sensor1 = u;
            //u.PollInterval = 20;

            b.Connect();

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    b.Disconnect();
                    //bc.Disconnect();
                        return;
                }
                else
                {
                    u.Poll();
                    
                    byte? x = u.ReadMeasurementByteX(0);
                    byte? y = u.ReadMeasurementByteX(1);
                    if (x != null && y != null)
                    {
                        byte[] a = new byte[] { x.Value, y.Value };

                        Console.WriteLine((y << 8)+x);
                        //Console.WriteLine(BitConverter.ToInt16(a, 0));
                    }
                    
                    for (int i = 0; i < 8; i++)
                    {
                        Console.WriteLine(i + ": " + u.ReadMeasurementByteX((byte)i));
                    }
                    
                    
                }
            }

        }
    }
}
