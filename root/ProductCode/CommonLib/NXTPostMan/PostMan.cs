using System;

namespace CommonLib.NXTPostMan
{
    public class PostMan : INXTPostMan
    {

        public void SendMessage(NXTMessage msg)
        {
            throw new NotImplementedException();
        }

        public bool HasMessageArrived(NXTMessageType type)
        {
            throw new NotImplementedException();
        }

        public bool HasMessageArrived(string msg)
        {
            throw new NotImplementedException();
        }

        public NXTMessage RetrieveMessage(NXTMessageType type)
        {
            throw new NotImplementedException();
        }

        public NXTMessage RetrieveMessage()
        {
            throw new NotImplementedException();
        }
    }
}
