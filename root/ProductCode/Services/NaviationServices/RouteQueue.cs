using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Interfaces;

namespace Services.NaviationServices
{
    public class RouteQueue : IEnumerable<ICoordinate>
    {
        private Queue<ICoordinate> coordinateQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteQueue"/> class.
        /// </summary>
        public RouteQueue()
        {
            coordinateQueue = new Queue<ICoordinate>();
        }

        /// <summary>
        /// Retrieves the next coordinate and removes it from the queue.
        /// </summary>
        /// <returns></returns>
        public ICoordinate RetrieveNextCoordinate()
        {
            return coordinateQueue.Dequeue();
        }

        /// <summary>
        /// Returns the coordinate at the beginning of the queue without removing it
        /// </summary>
        /// <returns></returns>
        public ICoordinate Peek()
        {
            return coordinateQueue.Peek();
        }

        /// <summary>
        /// Enqueues a coordinate in the queue.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        public void EnqueueCoordinate(ICoordinate coordinate)
        {
            coordinateQueue.Enqueue(coordinate);
        }


        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>w
        public IEnumerator<ICoordinate> GetEnumerator()
        {
            return coordinateQueue.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
