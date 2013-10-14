using System;

// Implemented using the design priciples og HiTechnicCompassSensor and NxtUltrasonicSensor

namespace NKH.MindSqualls
{
    /// <summary>
    /// Class representing the Mindsensors High Precision Medium Range Infrared Distance Sensor
    /// </summary>
    public class MindsensorsHPMRInfrared : NxtDigitalSensor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MindsensorsHPMRInfrared"/> class.
        /// </summary>
        public MindsensorsHPMRInfrared()
            : base()
        {
        }

        internal override void InitSensor()
        {
            base.InitSensor();

            //Set sensor module mode to GP2Y0A21YK (Medium Range)
            CommandToAddress(0x41, 0x33);
            Energize();
        }

        /// <summary>
        /// Reads the most-significant distance byte.
        /// </summary>
        /// <returns></returns>
        public byte? DistanceMSB()
        {
            return ReadByteFromAddress(0x43);
        }
        /// <summary>
        /// Reads the least-significant distance byte.
        /// </summary>
        /// <returns></returns>
        public byte? DistanceLSB()
        {
            return ReadByteFromAddress(0x42);
        }

        /// <summary>
        /// Gets the distance to an object, as read by the last call to <see cref="Poll"/>.
        /// </summary>
        /// <value>
        /// The distance (int mm).
        /// </value>
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

        /// <summary>
        /// Turns on the sensor.
        /// </summary>
        public void Energize()
        {
            CommandToAddress(0x41, 0x45);
        }
        /// <summary>
        /// Turns off the sensor.
        /// </summary>
        public void DeEnergize()
        {
            CommandToAddress(0x41, 0x44);
        }

        private byte? polldataMSB, polldataLSB;
        private object pollDataLock = new object();

        /// <summary>
        /// Poll the sensor and updates the <see cref="Distance"/> property.
        /// </summary>
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
