using System;

// Implemented using the design priciples og HiTechnicCompassSensor and NxtUltrasonicSensor

namespace NKH.MindSqualls
{
    public class MindsensorsHPMRInfrared : NxtDigitalSensor
    {
        public MindsensorsHPMRInfrared()
            : base()
        {
        }

        internal override void InitSensor()
        {
            base.InitSensor();

            //Set sensor module mode to GP2Y0A21YK (Medium Range)
            CommandToAddress(0x41, 0x33);

            //Energize sensor module
            CommandToAddress(0x41, 0x45);
        }
    }
}
