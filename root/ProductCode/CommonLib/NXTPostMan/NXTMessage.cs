using System;

namespace CommonLib.NXTPostMan
{
    public struct NXTMessage
    {

        private NXTMessageType msgType;
        private string encodedMsg;

        public NXTMessage(NXTMessageType type, string msg)
        {
            msgType = type;
            encodedMsg = msg;
        }
    }
}
