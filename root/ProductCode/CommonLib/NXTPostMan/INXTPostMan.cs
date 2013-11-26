using System;

namespace CommonLib.NXTPostMan
{
    public interface INXTPostMan
    {

        void SendMessage(INXTMessage msg);

        bool HasMessageArrived(IMessageType type);

        bool HasMessageArrived(string msg);


    }

    public interface IMessageType { }
    public interface INXTMessage { }
}
