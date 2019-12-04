// Binary Heap Array Min
//
// (c)2019 Mooflower Ltd.
//
// A standard binary heap array, with min value at root.
//

using System;
using System.Diagnostics;

namespace moo {
	public class BinaryHeapMin<T> where T : IComparable<T> {

		private T[] _heap;

        public int Size { get; private set; }

        // Standard constructor.  Set the initial capacity, which will grow by
        // doubling capacity when exceeded.  

        public BinaryHeapMin(int initialCapacity = 0) {
			_heap = new T[initialCapacity +1];
			Size = 0;
		}

		// Construct by cloning array

		public BinaryHeapMin (T[] initial, int additionalCapacity = 0, int heapSize = -1) {
			Debug.Assert(heapSize <= initial.Length);

			_heap = new T[initial.Length +additionalCapacity +1];
			Size = heapSize == -1 ? initial.Length : (heapSize +1);

			_cloneFrom(initial, Size);
			_heapifyAll();
		}


		// isEmpty returns true if there are no elements in the heap.
		// A descriptive shortcut function.

		public bool isEmpty() {
			return Size <= 0;
		}


		// Peek returns the root element without removing it.

		public T peek () {
			Debug.Assert(Size > 0);
			return _heap[1];
		}


		// Pop removes the root element, swaps the last element into the root
		// and then sinks it

		public T pop() {
			Debug.Assert(Size > 0);

			T r = _heap[1];

			if (Size > 1) {
				_heap[1] = _heap[Size];
				_sink(1);
			}

			Size--;

			return r;
		}


		// Push places T into the binary heap at the end of the stack
		// and bubbles up

		public void push (T t) {
			if (Size +1 >= _heap.Length) {
				_resizeArray();
			}
			_heap[++Size] = t;
			_bubble(Size);
		}


		// Resize array doubles the capacity of the array - for best performance
		// set the initial capacity correctly.  Can't reuse _cloneFrom as
		// both heaps are one-indexed

		private void _resizeArray() {
			T[] oldHeap = _heap;
			_heap = new T[_heap.Length << 1];

			int count = Size;
			while (count > 0) {
				_heap[count] = oldHeap[count--];
			}
		}


		// Raise the node at index up through the tree while it is smaller than
		// its parent.

		private void _bubble(int index) {
			while (index > 1) {

				// Quit if the node at index is >= parent
				int parent = index >> 1;
				if (_heap[index].CompareTo(_heap[parent]) >= 0) {
					return;
				}

				// Swap index with parent
				T t = _heap[index];
				_heap[index] = _heap[parent];
				_heap[parent] = t;

				index = parent;
			}
		}


		// Drop the node at index through the tree to the right place.  Defers
		// to the appropriate heapify or drops out index has no kids.

		private void _sink(int index) {
			int firstChild = index << 1;

			if (Size > firstChild) {
				_heapifyWithTwoChildren(index);
			} else if (Size == firstChild) {
				_heapifyWithOneChild(index);
			}
		}

		// Clone <count> elements of the the passed array <from> into _heap
		// nb: <from> is a zero-indexed array, _heap is one-indexed

		private void _cloneFrom (T[] from, int count) {
			while (count > 0) {
				_heap[count--] = from[count];
			} 
		}


		// Heapify All calls heapify on every element that has a child since
		// heapify compares a node with its child.
		// Given the nature of a binary heap, the index of the last parent
		// is: (heapSize / 2)

		private void _heapifyAll () {
			int parentIndex = Size >> 1;
			if (parentIndex > 0) {
				
				if ((Size & 1) == 0) {
					_heapifyWithOneChild(parentIndex--);
				}

				while (parentIndex > 0) {
					_heapifyWithTwoChildren(parentIndex--);
				}
			}
		}


		// Heapify compares a node with its smallest child and, if it's smaller,
		// swaps it with that, and repeat.  This function operates on the left
		// child only and does not recurse.  This function must only be called
		// on a node that has precisely one child.

		private void _heapifyWithOneChild(int index) {
			int indexLeftChild = (index << 1);

			if (_heap[indexLeftChild].CompareTo(_heap[index]) < 0) {
				T t = _heap[index];
				_heap[index] = _heap[indexLeftChild];
				_heap[indexLeftChild] = t;
			}			
		}


		// Heapify compares a node with its smallest child and, if it's smaller,
		// swaps it with that, and then repeat (assuming it is still a parent).
		// This function must only be called with two children.

		private void _heapifyWithTwoChildren(int index) {

			int indexSmallestChild;
			int minParent = Size >> 1;
			int indexLeftChild = index << 1;
			
			while (index <= minParent) {

				// Select the smallest child for comparison
				int indexRightChild = indexLeftChild + 1;
				indexSmallestChild = _heap[indexLeftChild].CompareTo(_heap[indexRightChild]) < 0 ? indexLeftChild : indexRightChild;

				// If the smallest child is >= parent then we're done
				if (_heap[indexSmallestChild].CompareTo(_heap[index]) >= 0) {
					return;
				}

				// Swap the parent with the child
				T t = _heap[index];
				_heap[index] = _heap[indexSmallestChild];
				_heap[indexSmallestChild] = t;

				// Is index a parent of precisely one child?
				index = indexSmallestChild;
				indexLeftChild = index << 1;
				if (indexLeftChild == Size) {
					_heapifyWithOneChild(index);
					return;
				}
			}
 		}
	}
}
