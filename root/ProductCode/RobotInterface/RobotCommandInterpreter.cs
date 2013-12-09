using System;
using CommonLib;
using CommonLib.NXTPostMan;
using CommonLib.Interfaces;
using CommonLib.DTOs;
using System.Threading;
using Control;

namespace SystemInterface.RobotInterface
{
    public class RobotCommandInterpreter
    {
        private const int THREAD_SLEEP_INTERVAL_IN_MILLISECONDS = 100;

        private INXTPostMan postman;
        private bool RUNNING = true;
        private LocationControl locCon;
        private MappingControl mapCon;

        #region cTor Chain.

        /// <summary>
        /// Default cTor.
        /// </summary>
        public RobotCommandInterpreter() 
            : this(PostMan.Instance, LocationControl.Instance, MappingControl.Instance)
        { }

        /// <summary>
        /// Master cTor.
        /// </summary>
        public RobotCommandInterpreter(PostMan p, LocationControl loc, MappingControl map)
        {
            postman = p;
            locCon = loc;
            mapCon = map;
            new Thread(new ThreadStart(this.listener)).Start();
        }

        #endregion

        private void listener()
        {
            while (RUNNING)
            {
                if (checkForMessages())
                {
                    NXTMessage msg = postman.RetrieveMessage();

                    switch (msg.MessageType)
                    {
                        case (NXTMessageType.RobotRequestsLocation):
                            RobotRequestLocation();
                            break;
                        case (NXTMessageType.RobotHasArrivedAtDestination):
                            RobotHasArrivedAtDestination();
                            break;
                        default:
                            throw new Exception("Dont know what to do ???");
                    }

                }
                Thread.Sleep(THREAD_SLEEP_INTERVAL_IN_MILLISECONDS);
            }
        }

        private bool checkForMessages()
        {
            return postman.HasMessageArrived(NXTMessageType.RobotRequestsLocation)
                || postman.HasMessageArrived(NXTMessageType.RobotHasArrivedAtDestination);
        }

        #region Command Handlers

        private void RobotRequestLocation()
        {
            IPose pose = locCon.RobotPose;
            string encodedMsg = NXTEncoder.Encode(pose);
            NXTMessage outMsg = new NXTMessage(NXTMessageType.SendPostion, encodedMsg);
            postman.SendMessage(outMsg);
        }

        private void RobotHasArrivedAtDestination()
        {
            mapCon.SendRobotToNextLocation();
        }

        #endregion
    }
}
