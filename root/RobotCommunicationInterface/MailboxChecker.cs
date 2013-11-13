using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;

namespace RobotCommunicationInterface
{
    public class MailboxChecker
    {
        private NxtCommunicationProtocol comm;
        private NxtMailbox2 pcInbox;
        private char controlCharacter;
        private Thread thread;
        private volatile bool shouldStop;

        private MailboxChecker(NxtCommunicationProtocol comm, NxtMailbox2 mailbox, char controlCharacter)
        {
            this.comm = comm;
            pcInbox = mailbox;
            this.controlCharacter = controlCharacter;
            thread = new Thread(Checker);
        }

        public MailboxChecker(NxtCommunicationProtocol comm, char controlCharacter)
            : this(comm, NxtMailbox2.Box0, controlCharacter) { }

        public void StartChecking()
        {
            thread.Start();
            shouldStop = false;
        }

        public void StopChecking()
        {
            shouldStop = true;
        }

        private void Checker()
        {
            while (!shouldStop)
            {
                try
                {
                    string reply = comm.MessageRead(pcInbox, NxtMailbox.Box0, true);

                    if (reply[0] != controlCharacter) continue;
                }
                catch (NxtCommunicationProtocolException ex)
                {
                    if (ex.ErrorMessage != NxtErrorMessage.SpecifiedMailboxQueueIsEmpty) throw;
                }
            }
        }
    }
}
