using System;

namespace CommonLib.NXTPostMan
{
    public interface INXTPostMan
    {
        void SendMessage(NXTMessage encodedMsg);

        bool HasMessageArrived(NXTMessageType type);

        bool HasMessageArrived(string msg);

        bool HasMessageArrived();

        NXTMessage RetrieveMessage();
    }
}
