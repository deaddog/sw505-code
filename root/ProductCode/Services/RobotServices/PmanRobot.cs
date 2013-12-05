using System;
using CommonLib.NXTPostMan;
using CommonLib.Interfaces;
using System.Threading;
using CommonLib.DTOs;

namespace Services.RobotServices
{
    public class PmanRobot : IRobot
    {
        private const int THREAD_SLEEP_INTERVAL_IN_MILLISECONDS = 100;
        private INXTPostMan postman;

        #region cTor chain
        
        public PmanRobot() : this(PostMan.Instance) { }

        public PmanRobot(INXTPostMan p)
        {
            postman = p;
        }

        #endregion

        public void MoveToPosition(ICoordinate destination)
        {
            string encodedMsg = CommonLib.NXTPostMan.NXTEncoder.Encode(destination);
            byte[] byteEncMsg = CommonLib.NXTPostMan.NXTEncoder.ByteEncode(destination);
            NXTMessage msg = new NXTMessage(NXTMessageType.MoveToPos, encodedMsg, byteEncMsg);
            postman.SendMessage(msg);
        }

        public ISensorData GetSensorData()
        {
            postman.SendMessage(new NXTMessage(NXTMessageType.GetSensorMeasurement, "", null));

            while (!postman.HasMessageArrived(NXTMessageType.SendSensorData))
            {
                Thread.Sleep(THREAD_SLEEP_INTERVAL_IN_MILLISECONDS);
            }

            byte[] reply = postman.RetrieveMessage().ByteMsg;
            return new SensorDataDTO(reply[1], reply[2], reply[3], reply[4]);
        }
    }
}
