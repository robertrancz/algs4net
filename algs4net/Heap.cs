/******************************************************************************
 *  File name :    Heap.cs
 *  Demo test :    Use the algscmd util or Visual Studio IDE
 *            :    Enter algscmd alone for how to use the util
 *  Data files:   http://algs4.cs.princeton.edu/24pq/tiny.txt
 *                http://algs4.cs.princeton.edu/24pq/words3.txt
 *
 *  Sorts a sequence of strings from standard input using heapsort.
 *
 *  C:\> type tiny.txt
 *  S O R T E X A M P L E
 *
 *  C:\> algscmd Heap < tiny.txt
 *  A E E L M O P R S T X                 [ one string per line ]
 *
 *  C:\> type words3.txt
 *  bed bug dad yes zoo ... all bad yet
 *
 *  C:\> algscmd Heap < words3.txt
 *  all bad bed bug dad ... yes yet zoo   [ one string per line ]
 *
 ******************************************************************************/

using System;

namespace algs4net
{
    /// <summary>
    /// The <c>Heap</c> class provides a static methods for heapsorting an array.</summary>
    /// <remarks>
    /// For additional documentation, see <a href="http://algs4.cs.princeton.edu/24pq">Section 2.4</a> of
    /// <em>Algorithms, 4th Edition</em> by Robert Sedgewick and Kevin Wayne.
    /// <para>This class is a C# port from the original Java class
    /// <a href="http://algs4.cs.princeton.edu/code/edu/princeton/cs/algs4/Heap.java.html">Heap</a> implementation by
    /// Robert Sedgewick, and Kevin Wayne.
    /// </para></remarks>
    ///
    public static class Heap
    {
        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.</summary>
        /// <param name="pq">pq the array to be sorted</param>
        ///
        public static void Sort(IComparable[] pq)
        {
            int N = pq.Length;
            for (int k = N / 2; k >= 1; k--)
                Sink(pq, k, N);
            while (N > 1)
            {
                Exch(pq, 1, N--);
                Sink(pq, 1, N);
            }
        }

        /***************************************************************************
         * Helper functions to restore the heap invariant.
         ***************************************************************************/

        private static void Sink(IComparable[] pq, int k, int N)
        {
            while (2 * k <= N)
            {
                int j = 2 * k;
                if (j < N && Less(pq, j, j + 1)) j++;
                if (!Less(pq, k, j)) break;
                Exch(pq, k, j);
                k = j;
            }
        }

        /***************************************************************************
         * Helper functions for comparisons and swaps.
         * Indices are "off-by-one" to support 1-based indexing.
         ***************************************************************************/
        private static bool Less(IComparable[] pq, int i, int j)
        {
            return pq[i - 1].CompareTo(pq[j - 1]) < 0;
        }

        private static void Exch(Object[] pq, int i, int j)
        {
            Object swap = pq[i - 1];
            pq[i - 1] = pq[j - 1];
            pq[j - 1] = swap;
        }

        // is v < w ?
        private static bool Less(IComparable v, IComparable w)
        {
            return v.CompareTo(w) < 0;
        }

        /***************************************************************************
         *  Check if array is sorted - useful for debugging.
         ***************************************************************************/
        private static bool IsSorted(IComparable[] a)
        {
            for (int i = 1; i < a.Length; i++)
                if (Less(a[i], a[i - 1])) return false;
            return true;
        }

        /// <summary>
        /// Reads in a sequence of strings from standard input; heapsorts them;
        /// and prints them to standard output in ascending order.</summary>
        /// <param name="args">Place holder for user arguments</param>
        ///
        [HelpText("algscmd Heap < words3.txt", "Input strings to be printed in sorted order")]
        public static void MainTest(string[] args)
        {
            TextInput StdIn = new TextInput();
            string[] a = StdIn.ReadAllStrings();
            Heap.Sort(a);
            OrderHelper.Show(a);
        }
    }
}
