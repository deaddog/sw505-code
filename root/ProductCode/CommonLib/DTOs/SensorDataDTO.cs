using System;
using CommonLib.Interfaces;

namespace CommonLib.DTOs
{
    public struct SensorDataDTO : ISensorData
    {
        private byte sensorFront;
        private byte sensorBack;
        private byte sensorLeft;
        private byte sensorRight;

        public byte SensorFront
        {
            get { return sensorFront; }
            set { sensorFront = value; }
        }

        public byte SensorBack
        {
            get { return sensorBack; }
            set { sensorBack = value; }
        }

        public byte SensorLeft
        {
            get { return sensorLeft; }
            set { sensorLeft = value; }
        }

        public byte SensorRight
        {
            get { return sensorRight; }
            set { sensorRight = value; }
        }

        public SensorDataDTO(byte sensorFront, byte sensorBack, byte sensorLeft, byte sensorRight)
        {
            this.sensorFront = sensorFront;
            this.sensorBack = sensorBack;
            this.sensorLeft = sensorLeft;
            this.sensorRight = sensorRight;
        }
    }
}