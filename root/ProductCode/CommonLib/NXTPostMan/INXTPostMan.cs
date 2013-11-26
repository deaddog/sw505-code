using System;

namespace CommonLib.NXTPostMan
{
    public interface INXTPostMan
    {
        void SendMessage(NXTMessage msg);

        bool HasMessageArrived(NXTMessageType type);

        bool HasMessageArrived(string msg);

        NXTMessage RetrieveMessage(NXTMessageType type);

        NXTMessage RetrieveMessage();
    }
}
