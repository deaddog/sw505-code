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
        static McNxtMotor mcb = new McNxtMotor();
        static McNxtMotor mcc = new McNxtMotor();
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("results.txt", FileMode.Create);
            var w = new StreamWriter(fs);
            w.AutoFlush = true;

            Console.SetOut(w);
            Console.SetError(w);


                McNxtBrick mcbrick = new McNxtBrick(NxtCommLinkType.Bluetooth, 10);

                

                mcbrick.MotorB = mcb;
                mcbrick.MotorC = mcc;

                mcb.PollInterval = 1;
                mcc.PollInterval = 1;

                mcb.OnPolled += mcl_OnPolled;
                mcc.OnPolled += mcr_OnPolled;

                McNxtMotorSync motorPair = new McNxtMotorSync(mcb, mcc);

                mcbrick.Connect();

                mcbrick.StartMotorControl();

                motorPair.Run(100, 360, 0);

                Console.ReadKey();

                mcbrick.Disconnect();
        }

        static void mcr_OnPolled(NxtPollable polledItem)
        {
            Console.WriteLine("motor port c: " + mcc.TachoCount.Value);
        }

        private static void mcl_OnPolled(NxtPollable polledItem)
        {

            Console.WriteLine("motor port b: " + mcb.TachoCount.Value);
            
        }
    }
}
