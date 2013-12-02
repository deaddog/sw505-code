using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonLib.Interfaces;
using CommonLib.NXTPostMan;

namespace Control
{
    public class QueueControl
    {
        public const int THREAD_SLEEP_MS = 1000;
        volatile bool shouldMessage = false;

        private INXTPostMan postman;
        private Queue<ICoordinate> coordinateQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueControl"/> class.
        /// </summary>
        public QueueControl() : this(PostMan.getInstance()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueControl"/> class.
        /// </summary>
        /// <param name="p">The p.</param>
        public QueueControl(INXTPostMan p)
        {
            postman = p;
            coordinateQueue = new Queue<ICoordinate>();
        }

        /// <summary>
        /// Enqueues the specified coordinate to the queue
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        public void Enqueue(ICoordinate coordinate)
        {
            coordinateQueue.Enqueue(coordinate);
        }

        /// <summary>
        /// Starts the queue checking for messages
        /// </summary>
        public void StartQueue()
        {
            shouldMessage = true;
            Task t = Task.Factory.StartNew(MessageLoop);
        }

        /// <summary>
        /// Stops the queue checking for messages
        /// </summary>
        public void StopQueue()
        {
            shouldMessage = false;
        }

        /// <summary>
        /// A loop that checks for new messages every THREAD_SLEEP_MS milliseconds
        /// </summary>
        private void MessageLoop()
        {
            while (shouldMessage)
            {
                Thread.Sleep(THREAD_SLEEP_MS);
                CheckArrivedMessage();
            }
        }

        /// <summary>
        /// Checks if the postman has a "RobotHasArrivedAtDestination" message.
        /// If this is the case and a new coordinate is in the queue, it retrieves the message and sends a new coordinate
        /// If there is no coordinate to send, the message is left in the inbox
        /// </summary>
        private void CheckArrivedMessage()
        {
            if (postman.HasMessageArrived(NXTMessageType.RobotHasArrivedAtDestination))
            {
                if (coordinateQueue.Peek() != null)
                {
                    postman.RetrieveMessage();
                    (postman as PostMan).SendMessage(coordinateQueue.Dequeue());
                }
            }
        }
    }
}
