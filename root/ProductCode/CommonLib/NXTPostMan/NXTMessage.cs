using System;
using CommonLib;

namespace CommonLib.NXTPostMan
{
    public struct NXTMessage
    {
        private NXTMessageType msgType;
        public NXTMessageType MessageType
        {
            get { return msgType; }
        }

        private string encodedMsg;
        public string EncodedMsg
        {
            get { return encodedMsg; }
        }

        private byte[] byteMsg;
        public byte[] ByteMsg
        {
            get { return byteMsg; }
        }


        public NXTMessage(byte[] completeByteMsg)
        {
            try
            {
                NXTMessageType recievedType = (NXTMessageType)Enum.Parse(typeof(NXTMessageType), StringByteConverter.GetString(completeByteMsg[0]));
                msgType = recievedType;
                encodedMsg = completeByteMsg.ToString().Substring(1, completeByteMsg.Length - 2);
                byteMsg = completeByteMsg;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Unable to parse commandtype!");
            }
        }

        public NXTMessage(NXTMessageType type, string msg, byte[] bMsg = null)
        {
            msgType = type;
            encodedMsg = msg;
            byteMsg = bMsg;
        }
    }
}
