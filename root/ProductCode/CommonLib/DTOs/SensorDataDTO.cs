using System;
using CommonLib.Interfaces;

namespace CommonLib.DTOs
{
    public struct SensorDataDTO : ISensorData
    {
        private byte sensorADistance;
        private byte sensorBDistance;

        public byte SensorADistance
        {
            get { return sensorADistance; }
        }

        public byte SensorBDistance
        {
            get { return sensorBDistance; }
        }

        public SensorDataDTO(byte sensorADistance, byte sensorBDistance)
        {
            this.sensorADistance = sensorADistance;
            this.sensorBDistance = sensorBDistance;
        }
    }
}