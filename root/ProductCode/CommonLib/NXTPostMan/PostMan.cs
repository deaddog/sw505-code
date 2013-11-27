using System;

namespace CommonLib.NXTPostMan
{
    public class PostMan : INXTPostMan
    {
        // Used for sending/receiving commands to/from NXT
        private const NxtMailbox2 PC_INBOX = NxtMailbox2.Box0;
        private const NxtMailbox PC_OUTBOX = NxtMailbox.Box1;





        public void SendMessage(NXTMessage encodedMsg)
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
