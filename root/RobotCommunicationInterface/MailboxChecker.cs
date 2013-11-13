using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;

namespace RobotCommunicationInterface
{
    public class MailboxChecker
    {
        private NxtCommunicationProtocol comm;
        private NxtMailbox2 pcInbox;
        private char controlCharacter;
        private volatile bool shouldStop;

        private MailboxChecker(NxtCommunicationProtocol comm, NxtMailbox2 mailbox, char controlCharacter)
        {
            this.comm = comm;
            pcInbox = mailbox;
            this.controlCharacter = controlCharacter;
        }

        public MailboxChecker(NxtCommunicationProtocol comm, char controlCharacter)
            : this(comm, NxtMailbox2.Box0, controlCharacter) { }

        public string Checker()
        {
            while (true)
            {
                try
                {
                    System.Threading.Thread.Sleep(10);

                    string reply = comm.MessageRead(pcInbox, NxtMailbox.Box0, true);

                    if (reply[0] != controlCharacter) continue;
                    return reply.Substring(1);
                }
                catch (NxtCommunicationProtocolException ex)
                {
                    if (ex.ErrorMessage != NxtErrorMessage.SpecifiedMailboxQueueIsEmpty) throw;
                }
            }
        }
    }
}
