using System;
using System.Collections.Generic;
using System.Text;
using NKH.MindSqualls;

namespace RobotCommand
{
    public static class NxtControl
    {
        private const NxtMailbox2 PC_INBOX = NxtMailbox2.Box6;
        private const NxtMailbox PC_OUTBOX = NxtMailbox.Box5;
        public static void NxtTone(NxtCommunicationProtocol commLink, int tone)
        {
            string messageData = string.Format("{0}",
                (byte)tone
                );
            commLink.MessageWrite(PC_OUTBOX, messageData);

            
        }

        public static string NxtSensorReading(NxtCommunicationProtocol commLink)
        {
            return commLink.MessageReadToStringASCII(PC_INBOX, NxtMailbox.Box0, true);
        }
    }
}
