using System;
using System.Collections;
using System.Collections.Generic;

namespace RunawaySystems.Pong {

    /// <summary> A list that can be added to and removed from while being iterated on. </summary>
    public class ProtectedList<T> : IEnumerable<T> {

        private List<T> frontBuffer;
        private Queue<T> addQueue;
        private Queue<T> removeQueue;

        public ProtectedList(){
            frontBuffer = new List<T>();
            removeQueue = new Queue<T>();
            addQueue = new Queue<T>();
        }

        /// <summary> Queues an object to be added to the list. </summary>
        public void Add(T thing) {
            addQueue.Enqueue(thing);
        }

        /// <summary> Queues an object to be removed from the list. </summary>
        public void Remove(T thing) {
            removeQueue.Enqueue(thing);
        }

        /// <summary> Number of objects in the list not counting queued objects that have not yet been <see cref="Pump"/>ed into the front buffer. </summary>
        public int Count() => frontBuffer.Count;

        /// <summary> Number of objects that have not yet been <see cref="Pump"/>ed into the front buffer. </summary>
        public int QueuedCount() => addQueue.Count + removeQueue.Count;

        /// <summary> Transfers all queued objects into/out of the list. </summary>
        public void Pump() {
            while(addQueue.Count > 0)
                frontBuffer.Add(addQueue.Dequeue());

            while (removeQueue.Count > 0)
                frontBuffer.Remove(removeQueue.Dequeue());
        }

        #region IEnumerable
        public IEnumerator<T> GetEnumerator() {
            return ((IEnumerable<T>)frontBuffer).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable)frontBuffer).GetEnumerator();
        }
        #endregion IEnumerable
    }
}
