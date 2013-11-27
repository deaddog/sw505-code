﻿using System;
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

        
        public bool HasMessageArrived(NXTMessageType type)
        {
            string msg = CommunicationBrick.CommLink.MessageRead(PC_INBOX, NxtMailbox.Box0, false);
            NXTMessageType recievedType = (NXTMessageType)Enum.Parse(typeof(NXTMessageType), msg[0].ToString());

            return type == recievedType;
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
