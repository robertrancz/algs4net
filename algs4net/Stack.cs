using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace algs4net
{
    /// <summary><para>
    /// The <c>Stack</c> class represents a last-in-first-out (LIFO) stack of
    /// generic items. The implementation is effectively the same as the
    /// <a href="http://algs4.cs.princeton.edu/code/edu/princeton/cs/algs4/Stack.java.html">Stack</a>
    /// class implementation.</para><para>
    /// It supports the usual <c>Push</c> and <c>Pop</c> operations, along with methods
    /// for peeking at the top item, testing if the stack is empty, and iterating through
    /// the items in LIFO order.
    /// </para><para>
    /// This implementation uses a singly-linked list with a nested, non-static class Node.
    /// The <c>Push</c>, <c>Pop</c>, <c>Peek</c>, <c>Count</c>, and <c>IsEmpty</c>
    /// operations all take constant time in the worst case.
    /// </para></summary>
    /// <remarks>
    /// For additional documentation,
    /// see <a href="http://algs4.cs.princeton.edu/13stacks">Section 1.3</a> of
    /// <em>Algorithms, 4th Edition</em> by Robert Sedgewick and Kevin Wayne.
    /// <para>This class is a C# port from the original Java class
    /// <a href="http://algs4.cs.princeton.edu/code/edu/princeton/cs/algs4/LinkedStack.java.html">LinkedStack</a> implementation by
    /// Robert Sedgewick and Kevin Wayne.</para></remarks>
    ///
    public class Stack<Item> : IEnumerable<Item>
    {
        private int N;          // size of the stack
        private Node first;     // top of stack

        // helper linked list class
        private class Node
        {
            public Item item;
            public Node next;
        }

        /// <summary>
        /// Initializes an empty stack.</summary>
        ///
        public Stack()
        {
            first = null;
            N = 0;
            Debug.Assert(Check());
        }

        /// <summary>
        /// Is this stack empty?</summary>
        /// <returns>true if this queue is empty; false otherwise</returns>
        ///
        public bool IsEmpty
        {
            get { return first == null; }
        }

        /// <summary>
        /// Returns the number of items in the stack.</summary>
        /// <returns>the number of items in the stack</returns>
        ///
        public int Size
        {
            get { return N; }
        }

        /// <summary>
        /// Adds the item to this stack.</summary>
        /// <param name="item">item the item to add</param>
        ///
        public void Push(Item item)
        {
            Node oldfirst = first;
            first = new Node
            {
                item = item,
                next = oldfirst
            };
            N++;
            Debug.Assert(Check());
        }

        /// <summary>
        /// Removes and returns the item most recently added to this stack.</summary>
        /// <returns>the item most recently added</returns>
        /// <exception cref="InvalidOperationException">if this stack is empty</exception>
        ///
        public Item Pop()
        {
            if (IsEmpty) throw new InvalidOperationException("LinkedStack underflow");
            Item item = first.item;
            first = first.next;
            N--;
            Debug.Assert(Check());
            return item;
        }

        /// <summary>
        /// Returns (but does not remove) the item most recently added to this stack.</summary>
        /// <returns>the item most recently added to this stack</returns>
        /// <exception cref="InvalidOperationException">if this stack is empty</exception>
        ///
        public Item Peek()
        {
            if (IsEmpty) throw new InvalidOperationException("LinkedStack underflow");
            return first.item;
        }

        /// <summary>
        /// Returns a string representation of this stack.</summary>
        /// <returns>the sequence of items in the stack in LIFO order, separated by spaces</returns>
        ///
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            foreach (Item item in this)
                s.Append(item).Append(' ');
            return s.ToString();
        }

        /// <summary>
        /// Returns an iterator that iterates over the items in this queue in FIFO order.</summary>
        /// <returns>an iterator that iterates over the items in this queue in FIFO order</returns>
        ///
        public IEnumerator<Item> GetEnumerator()
        {
            return new ListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        // an iterator, doesn't implement remove() since it's optional
        private class ListEnumerator : IEnumerator<Item>
        {
            private Node current = null;
            private readonly Stack<Item> stack = null;
            private bool firstCall = true;

            public ListEnumerator(Stack<Item> collection)
            {
                stack = collection;
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

            object IEnumerator.Current
            {
                get { return Current as object; }
            }

            public bool MoveNext()
            {
                if (firstCall)
                {
                    current = stack.first;
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

        // check internal invariants
        private bool Check()
        {
            // check a few properties of instance variable 'first'
            if (N < 0)
            {
                return false;
            }
            if (N == 0)
            {
                if (first != null) return false;
            }
            else if (N == 1)
            {
                if (first == null) return false;
                if (first.next != null) return false;
            }
            else
            {
                if (first == null) return false;
                if (first.next == null) return false;
            }

            // check internal consistency of instance variable N
            int numberOfNodes = 0;
            for (Node x = first; x != null && numberOfNodes <= N; x = x.next)
            {
                numberOfNodes++;
            }
            return numberOfNodes == N;
        }

        /// <summary>
        /// Test client for the <c>Stack</c> data type.</summary>
        /// <param name="args">Place holder for user arguments</param>
        ///
        [HelpText("algscmd Stack < tobe.txt", "Items separated by space or new line")]
        public static void MainTest(string[] args)
        {
            var stack = new Stack<string>();

            TextInput StdIn = new TextInput();
            while (!StdIn.IsEmpty)
            {
                string item = StdIn.ReadString();

                if (!item.Equals("-"))
                    stack.Push(item);
                else if (!stack.IsEmpty)
                    Console.Write(stack.Pop() + " ");
            }
            Console.WriteLine("(" + stack.Size + " left on stack)");
        }
    }
}
