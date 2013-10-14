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

        public byte? DistanceMSB()
        {
            return ReadByteFromAddress(0x43);
        }
        public byte? DistanceLSB()
        {
            return ReadByteFromAddress(0x42);
        }

        public UInt16? Distance
        {
            get
            {
                byte? msb = polldataMSB, lsb = polldataLSB;
                if (msb.HasValue && lsb.HasValue)
                    return (ushort?)((msb.Value << 8) | lsb.Value);
                else
                    return (ushort?)null;
            }
        }

        private byte? polldataMSB, polldataLSB;
        private object pollDataLock = new object();

        public override void Poll()
        {
            if (Brick.IsConnected)
            {
                UInt16? oldDistance, newDistance;
                lock (pollDataLock)
                {
                    oldDistance = Distance;
                    polldataMSB = DistanceMSB();
                    polldataLSB = DistanceLSB();
                    base.Poll();
                    newDistance = Distance;
                }

                //Handle distance-range events...
            }
        }
    }
}
