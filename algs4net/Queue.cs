using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace algs4net
{
    /// <summary>
    /// <para>
    /// The <c>Queue</c> class represents a first-in-first-out (FIFO) queue of generic items.
    /// It supports the usual <em>enqueue</em> and <em>dequeue</em> operations,
    /// along with methods for peeking at the first item, testing if the queue is empty,
    /// and iterating through the items in FIFO order.
    /// </para>
    /// <para>
    /// This implementation uses a singly linked list with a nested, non-static class Node.
    /// See <em>ResizingArrayQueue</em> for a version that uses a resizing array.
    /// The <em>Enqueue</em>, <em>Dequeue</em>, <em>Peek</em>, <em>Size</em>, and <em>IsEmpty</em>
    /// operations all take constant time in the worst case.
    /// </para>
    /// </summary>
    /// <remarks>
    /// For additional documentation, see <a href="https://algs4.cs.princeton.edu/13stacks">Section 1.3</a> of
    /// <em>Algorithms, 4th Edition</em> by Robert Sedgewick and Kevin Wayne.
    /// </remarks>
    /// <typeparam name="Item"></typeparam>
    /// <author>Robert Rancz</author>
    public class Queue<Item> : IEnumerable<Item>
    {
        private Node first;    // beginning of queue
        private Node last;     // end of queue
        private int n;         // number of elements on queue

        /// <summary>Is this queue empty?</summary>
        /// <returns>true if this queue is empty; false otherwise</returns>
        public bool IsEmpty { get => first == null; }

        /// <summary>Returns the number of items in this queue.</summary>
        /// <returns>the number of items in this queue</returns>
        public int Size { get => n; }

        /// <summary>
        /// Helper linked list class
        /// </summary>
        private class Node
        {
            public Item item;
            public Node next;
        }

        /// <summary>Initializes an empty queue.</summary>
        public Queue()
        {
            first = null;
            last = null;
            n = 0;
        }

        /// <summary>Returns the item least recently added to this queue.</summary>        
        /// <exception cref="InvalidOperationException">Queue underflow</exception>
        public Item Peek()
        {
            if (IsEmpty) throw new InvalidOperationException("Queue underflow");
            return first.item;
        }

        /// <summary>Adds the item to this queue.</summary>
        /// <param name="item">The item.</param>
        public void Enqueue(Item item)
        {
            Node oldlast = last;
            last = new Node();
            last.item = item;
            last.next = null;
            if (IsEmpty)
                first = last;
            else
                oldlast.next = last;
            n++;
            Debug.Assert(check());
        }

        /// <summary>
        /// Removes and returns the item on this queue that was least recently added.</summary>
        /// <returns>the item on this queue that was least recently added</returns>
        /// <exception cref="InvalidOperationException">if this queue is empty</exception>
        ///
        public Item Dequeue()
        {
            if (IsEmpty) throw new InvalidOperationException("LinkedQueue underflow");
            Item item = first.item;
            first = first.next;
            n--;
            if (IsEmpty)
                last = null;   // to avoid loitering
            Debug.Assert(check());
            return item;
        }

        /// <summary>
        /// Returns a string representation of this queue.</summary>
        /// <returns>the sequence of items in FIFO order, separated by spaces</returns>
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            foreach (Item item in this)
                s.Append(item + " ");
            return s.ToString();
        }

        // check internal invariants
        private bool check()
        {
            if (n < 0)
            {
                return false;
            }
            else if (n == 0)
            {
                if (first != null) return false;
                if (last != null) return false;
            }
            else if (n == 1)
            {
                if (first == null || last == null) return false;
                if (first != last) return false;
                if (first.next != null) return false;
            }
            else
            {
                if (first == null || last == null) return false;
                if (first == last) return false;
                if (first.next == null) return false;
                if (last.next != null) return false;

                // check internal consistency of instance variable N
                int numberOfNodes = 0;
                for (Node x = first; x != null && numberOfNodes <= n; x = x.next)
                {
                    numberOfNodes++;
                }
                if (numberOfNodes != n) return false;

                // check internal consistency of instance variable last
                Node lastNode = first;
                while (lastNode.next != null)
                {
                    lastNode = lastNode.next;
                }
                if (last != lastNode) return false;
            }

            return true;
        }

        /// <summary>
        /// Returns an iterator that iterates over the items in this queue in FIFO order.</summary>
        public IEnumerator<Item> GetEnumerator()
        {
            return new ListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class ListEnumerator : IEnumerator<Item>
        {
            private Node current = null;
            private Queue<Item> queue = null;
            private bool firstCall = true;

            public ListEnumerator(Queue<Item> collection)
            {
                queue = collection;
            }

            public Item Current
            {
                get
                {
                    if (current == null)
                        throw new InvalidOperationException("Past end of collection!");
                    return current.item;
                }
            }

            object IEnumerator.Current => current as object;

            public bool MoveNext()
            {
                if (firstCall)
                {
                    current = queue.first;
                    firstCall = false;
                    return current != null;
                }
                if (current != null)
                {
                    current = current.next;
                    return current != null;
                }
                return false;
            }

            public void Reset()
            {
                current = null;
                firstCall = true;
            }

            public void Dispose() { }
        }

        /// <summary>
        /// Demo test the <c>Queue</c> data type.</summary>
        /// <param name="args">Place holder for user arguments</param>
        ///
        [HelpText("algscmd Queue < tobe.txt", "Items separated by space or new line")]
        public static void MainTest(string[] args)
        {
            var q = new Queue<string>();
            TextInput StdIn = new TextInput();
            while (!StdIn.IsEmpty)
            {
                string item = StdIn.ReadString();
                if (!item.Equals("-"))
                    q.Enqueue(item);
                else if (!q.IsEmpty)
                    Console.Write(q.Dequeue() + " ");
            }
            Console.WriteLine("(" + q.Size + " left on queue)");
        }
    }
}
