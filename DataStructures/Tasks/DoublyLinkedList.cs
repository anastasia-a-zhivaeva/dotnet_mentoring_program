using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private int _length = 0;
        private Node<T> _head;
        private Node<T> _tail;

        public int Length => _length;

        public void Add(T e)
        {
            CheckDataValidity(e);
            
            var node = new Node<T>(e);

            if (_head == null)
            {
                InitializeList(node);
            }
            else
            {
                AddAfterTail(node);
            }

            _length++;
        }

        public void AddAt(int index, T e)
        {
            CheckDataValidity(e);
            CheckIndexValidity(index, true);

            var node = new Node<T>(e);

            if (_head == null)
            {
                InitializeList(node);
            }
            else if (index == 0)
            {
                AddBeforeHead(node);
            }
            else if (index == _length)
            {
                AddAfterTail(node);
            }
            else
            {
                AddAt(node, index);
            }

            _length++;
        }

        private void InitializeList(Node<T> node)
        {
            _head = node;
            _tail = _head;
        }

        private void AddBeforeHead(Node<T> node)
        {
            node.Next = _head;
            _head = node;
        }

        private void AddAt(Node<T> node, int index)
        {
            var nodeAt = FindAt(index);
            node.Previous = nodeAt.Previous;
            node.Next = nodeAt;
            nodeAt.Previous.Next = node;
            nodeAt.Previous = node;
        }

        private void AddAfterTail(Node<T> node)
        {
            _tail.Next = node;
            _tail.Next.Previous = _tail;
            _tail = _tail.Next;

        }

        public T ElementAt(int index)
        {
            CheckIndexValidity(index);
            return FindAt(index).Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumenator<T>(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Remove(T item)
        {
            CheckDataValidity(item);

            var node = Find(item);
            if (node != null)
            {
                Remove(node);
            }
        }

        private Node<T>? Find(T e)
        {
            Node<T> node = _head;

            while (node != null)
            {
                if (node.Data.Equals(e))
                {
                    return node;
                }
                node = node.Next;
            }

            return null;
        }

        public T RemoveAt(int index)
        {
            CheckIndexValidity(index);

            var node = FindAt(index);
            return Remove(node);
        }

        private void CheckIndexValidity(int index, bool isAdding = false)
        {
            var lastIndex = isAdding ? _length : _length - 1;
            if (index < 0 || index > lastIndex)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void CheckDataValidity(T e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }
        }

        private Node<T> FindAt(int index)
        {
            if (index == 0)
            {
                return _head;
            }

            if (index == _length - 1)
            {
                return _tail;
            }

            int tempIndex = 1;
            Node<T> node = _head.Next;
            while (tempIndex != index)
            {
                node = node.Next;
                tempIndex++;
            }
            return node;
        }

        private T Remove(Node<T> node)
        {
            _length--;
            if (node == _head)
            {
                return RemoveHead();
            }

            if (node == _tail)
            {
               return RemoveTail();
            }

            return RemoveNode(node);
        }

        private T RemoveHead()
        {
            var headNode = _head;
            _head = _head.Next;
            _head.Previous = null;
            return headNode.Data;
        }

        private T RemoveTail()
        {
            var tailNode = _tail;
            _tail.Previous.Next = null;
            _tail = _tail.Previous;
            return tailNode.Data;
        }

        private T RemoveNode(Node<T> node)
        {
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            return node.Data;
        }

        private class Node<V>
        {
            public V Data { get; set; }
            public Node<V> Next { get; set; }
            public Node<V> Previous { get; set; }

            public Node(V data)
            {
                Data = data;
                Next = null;
                Previous = null;
            }
        }

        private class DoublyLinkedListEnumenator<K>: IEnumerator<K>
        {
            private Node<K> _head;
            private Node<K> _current;

            public DoublyLinkedListEnumenator(Node<K> head)
            {
                _head = head;
                _current = null;
            }

            public bool MoveNext()
            {
                if (_current == null)
                {
                    _current = _head;
                }
                else
                {
                    _current = _current.Next;
                }
                return _current != null;
            }

            public void Reset()
            {
                _current = null;
            }

            public K Current => _current.Data;

            object IEnumerator.Current => Current;

            void IDisposable.Dispose()
            {
                _head = null;
                _current = null;
            }
        }
    }
}
