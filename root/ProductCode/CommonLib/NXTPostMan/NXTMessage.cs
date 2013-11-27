using System;

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


        public NXTMessage(NXTMessageType type, string msg)
        {
            msgType = type;
            encodedMsg = msg;
        }
    }
}
