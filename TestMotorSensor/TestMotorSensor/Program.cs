using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;

namespace TestMotorSensor
{
    class Program
    {
        static McNxtMotor mcl = new McNxtMotor();
        static McNxtMotor mcr = new McNxtMotor();
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("results.txt", FileMode.Create);
            var w = new StreamWriter(fs);
            w.AutoFlush = true;

            Console.SetOut(w);
            Console.SetError(w);


                McNxtBrick mcb = new McNxtBrick(NxtCommLinkType.Bluetooth, 10);

                

                mcb.MotorB = mcl;
                mcb.MotorC = mcr;

                mcl.PollInterval = 1;
                mcr.PollInterval = 1;

                mcl.OnPolled += mcl_OnPolled;
                mcr.OnPolled += mcr_OnPolled;

                McNxtMotorSync motorPair = new McNxtMotorSync(mcl, mcr);

                mcb.Connect();

                mcb.StartMotorControl();

                motorPair.Run(100, 3600, 0);

                Console.ReadKey();

                mcb.Disconnect();
        }

        static void mcr_OnPolled(NxtPollable polledItem)
        {
            Console.WriteLine("r: " + mcr.TachoCount.Value);
        }

        private static void mcl_OnPolled(NxtPollable polledItem)
        {

            Console.WriteLine("l: " + mcl.TachoCount.Value);
            
        }
    }
}
