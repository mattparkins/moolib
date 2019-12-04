using System;

namespace moo {
	class Program {

		static void LogResult(bool pass) {
			Console.WriteLine("(" + (pass ? "pass" : "FAIL") + ")");
		}

		static void Main (string[] args) {
			Console.WriteLine("moolibcs test driver\n(c)2019 mooflower ltd\nall rights reserved\n\n");

			TestBinaryHeapMin();
			TestBinaryHeapMax();
		}

		static void TestBinaryHeapMin() {
			Console.Write("BinaryHeapMin test: ");
			int[] arr = { 9, 3, 2, 1, 3, 6, 3, 1, 9, 2, 3 };
			BinaryHeapMin<int> heap = new BinaryHeapMin<int>(arr, 0);
			heap.push(123);
			heap.push(17);
			heap.push(46);
			heap.push(99);
			heap.push(351);

			int last = 0;
			bool pass = true;
			while (heap.Size > 0) {
				int v = heap.pop();
				Console.Write(v.ToString() + " ");
				pass &= v >= last;
			}
			LogResult(pass);
		}


		static void TestBinaryHeapMax () {
			Console.Write("BinaryHeapMax test: ");
			int[] arr = { 9, 3, 2, 1, 3, 6, 3, 1, 9, 2, 3 };
			BinaryHeapMax<int> heap = new BinaryHeapMax<int>(arr, 0);
			heap.push(123);
			heap.push(17);
			heap.push(46);
			heap.push(99);
			heap.push(351);

			int last = Int32.MaxValue;
			bool pass = true;
			while (heap.Size > 0) {
				int v = heap.pop();
				Console.Write(v.ToString() + " ");
				pass &= v <= last;
			}
			LogResult(pass);
		}
	}
}
