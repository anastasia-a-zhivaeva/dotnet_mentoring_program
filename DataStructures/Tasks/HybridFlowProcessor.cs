using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private DoublyLinkedList<T> _queue = new DoublyLinkedList<T>();
        public T Dequeue()
        {
            ThrowIfEmpty();
            return _queue.RemoveAt(0);
        }

        public void Enqueue(T item)
        {
            _queue.Add(item);
        }

        public T Pop()
        {
            ThrowIfEmpty();
            return _queue.RemoveAt(0);
        }

        public void Push(T item)
        {
            _queue.AddAt(0, item);
        }

        private void ThrowIfEmpty()
        {
            if (_queue.Length == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
