using System;
using NKH.MindSqualls;
using CommonLib.Interfaces;

namespace CommonLib.NXTPostMan
{
    public class PostMan : INXTPostMan
    {
        private static PostMan instance;
        private const byte SERIAL_PORT_NUMBER = 4;
        
        // Used for sending/receiving commands to/from NXT
        private const NxtMailbox2 PC_INBOX = NxtMailbox2.Box0;
        private const NxtMailbox PC_OUTBOX = NxtMailbox.Box1;
        private NxtBrick CommunicationBrick;

        #region cTor Chain

        public static PostMan getInstance()
        {
            if (instance == null)
            {
                instance = new PostMan();
            }
            return instance;
        }

        /// <summary>
        /// default cTor.
        /// </summary>
        private PostMan() {

            CommunicationBrick = new NxtBrick(NxtCommLinkType.Bluetooth, SERIAL_PORT_NUMBER);

        }

        #endregion
        
        public void SendMessage(NXTMessage msg)
        {
            // send the preformed message to the robot.
            string toSendMessage = String.Format("{0}{1}", (byte)msg.MessageType, msg.EncodedMsg);
            CommunicationBrick.Connect();
            CommunicationBrick.CommLink.MessageWrite(PC_OUTBOX, toSendMessage);
        }

        public void SendMessage(ICoordinate cord)
        {
            string encodedString = NXTEncoder.Encode(cord);
            string toSendMessage = String.Format("{0}{1}", (byte)NXTMessageType.MoveToPos, encodedString);
            CommunicationBrick.Connect();
            CommunicationBrick.CommLink.MessageWrite(PC_OUTBOX, encodedString);
        }

        /// <summary>
        /// Checks if the message on top of mailbox is the specified type.
        /// </summary>
        /// <param name="type">the type to check against.</param>
        /// <returns>true if types match.</returns>
        public bool HasMessageArrived(NXTMessageType type)
        {
            try
            {
                byte[] msg = CommunicationBrick.CommLink.MessageReadToBytes(PC_INBOX, NxtMailbox.Box0, false);
                NXTMessageType recievedType = parseCommandType(msg);
                return type == recievedType;
            }
            catch (NxtCommunicationProtocolException ex)
            {
                if (ex.ErrorMessage != NxtErrorMessage.SpecifiedMailboxQueueIsEmpty)
                {
                    throw;
                }
                return false;
            }
        }

        /// <summary>
        /// Checks if the message on top of mailbox matches string.
        /// </summary>
        /// <param name="msg">the string to match against.</param>
        /// <returns>true if string matches item from mailbox</returns>
        public bool HasMessageArrived(string msg)
        {
            try {
                string mailMsg = CommunicationBrick.CommLink.MessageReadToStringASCII(PC_INBOX, NxtMailbox.Box0, false);
                return msg.Equals(mailMsg);
            }
            catch (NxtCommunicationProtocolException ex)
            {
                if (ex.ErrorMessage != NxtErrorMessage.SpecifiedMailboxQueueIsEmpty)
                {
                    throw;
                }
                return false;
            }
        }

        /// <summary>
        /// Retrieves the message on top of mailbox.
        /// </summary>
        /// <returns>the message on top of mailbox formatted as an NXTMessage</returns>
        public NXTMessage RetrieveMessage()
        {
            try {
                byte[] msg = CommunicationBrick.CommLink.MessageReadToBytes(PC_INBOX, NxtMailbox.Box0, true);
                NXTMessageType recievedType = parseCommandType(msg);
                return new NXTMessage(recievedType, msg.ToString().Substring(1, msg.Length - 2));
            }
            catch (NxtCommunicationProtocolException ex)
            {
                if (ex.ErrorMessage != NxtErrorMessage.SpecifiedMailboxQueueIsEmpty)
                {
                    throw;
                }
                else
                {
                    throw new NoMailException();
                }
            }
        }
        
        private NXTMessageType parseCommandType(byte[] msg)
        {
            try
            {
                return (NXTMessageType)msg[0];
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Unable to parse commandtype!");
            }
        }
    }
}
