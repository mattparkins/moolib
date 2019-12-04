using System;
using System.Collections;
using System.Collections.Generic;

namespace moo {
    public class Bag<T> : IEnumerable<T> {

        public Bag() {
            _first = null;
            _count = 0;
        }

        public void Add(T t) {
            Node node = new Node {
                value = t, 
                next = _first
            };

            _first = node;
            _count++;
        }

        public bool IsEmpty() {
            return _count == 0;
        }

        public int Size() {
            return _count;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return new BagEnumerator(_first);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return new BagEnumerator(_first);
        }

        private Node _first;
        private int _count;

        private class Node {
            public T value;
            public Node next;
        }

        private class BagEnumerator : IEnumerator<T> {

            public BagEnumerator(Node first) {
                _current = _first = first;
            }

            bool IEnumerator.MoveNext() {
                _current = _current?.next;
                return _current != null;
            }

            void IEnumerator.Reset() {
                _current = _first;
            }

            void IDisposable.Dispose() {
                throw new NotImplementedException();
            }

            T IEnumerator<T>.Current => EqualityComparer<T>.Default.Equals(_current) ? _current.value : default;
            object IEnumerator.Current => EqualityComparer<T>.Default.Equals(_current) ? _current.value : default;

            private Node _first;
            private Node _current;
        }
    }
}