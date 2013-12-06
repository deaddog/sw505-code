using System;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;
using CommonLib.Interfaces;

namespace CommonLib.NXTPostMan
{
    public class PostMan : INXTPostMan
    {
        private static PostMan instance;

        // Used for sending/receiving commands to/from NXT
        private const NxtMailbox2 PC_INBOX = NxtMailbox2.Box0;
        private const NxtMailbox PC_OUTBOX = NxtMailbox.Box1;
        private McNxtBrick CommunicationBrick;

        #region cTor Chain

        public static PostMan Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PostMan(ConnectionSettings.Default.NXTPort);
                }
                return instance;
            }
        }

        private PostMan(byte serialPortNumber)
        {

            CommunicationBrick = new McNxtBrick(NxtCommLinkType.Bluetooth, serialPortNumber);
            CommunicationBrick.Sensor1 = new NxtUltrasonicSensor();
            CommunicationBrick.Sensor2 = new NxtUltrasonicSensor();

            CommunicationBrick.Connect();
            if (!CommunicationBrick.IsMotorControlRunning())
                CommunicationBrick.StartMotorControl();
        }

        #endregion

        public void SendMessage(NXTMessage msg)
        {
            // send the preformed message to the robot.
            string toSendMessage = String.Format("{0}{1}", (byte)msg.MessageType, msg.EncodedMsg);
            CommunicationBrick.CommLink.MessageWrite(PC_OUTBOX, toSendMessage);
        }

        public void SendMessage(ICoordinate cord)
        {
            string encodedString = NXTEncoder.Encode(cord);
            string toSendMessage = String.Format("{0}{1}", (byte)NXTMessageType.MoveToPos, encodedString);
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
            try
            {
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

        public bool HasMessageArrived()
        {
            try
            {
                byte[] msg = CommunicationBrick.CommLink.MessageReadToBytes(PC_INBOX, NxtMailbox.Box0, false);

                if (msg != null) { return true; } else { return false; }
            }
            catch (NxtCommunicationProtocolException ex)
            {
                if (ex.ErrorMessage.Equals(NxtErrorMessage.SpecifiedMailboxQueueIsEmpty))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Retrieves the message on top of mailbox.
        /// </summary>
        /// <returns>the message on top of mailbox formatted as an NXTMessage</returns>
        public NXTMessage RetrieveMessage()
        {
            try
            {
                byte[] msg = CommunicationBrick.CommLink.MessageReadToBytes(PC_INBOX, NxtMailbox.Box0, true);
                return new NXTMessage(msg);
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
                // 48 is subtracted to convert from ASCII numbers
                return (NXTMessageType)msg[0] - 48;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Unable to parse commandtype!");
            }
        }



    }
}
