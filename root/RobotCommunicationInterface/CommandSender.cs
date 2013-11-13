using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;

namespace RobotCommunicationInterface
{
    public class CommandSender
    {
        private NxtCommunicationProtocol comm;
        private NxtMailbox pcOutbox;

        private CommandSender(NxtCommunicationProtocol comm, NxtMailbox mailbox)
        {
            this.comm = comm;
            pcOutbox = mailbox;
        }

        public CommandSender(NxtCommunicationProtocol comm) : this(comm, NxtMailbox.Box1) { }

        public void SendCommand(MapperCommand cmd, string message)
        {
            string toSendMessage = String.Format("{0}{1}", (byte)cmd, message);
            comm.MessageWrite(pcOutbox, toSendMessage);
        }
    }
}
