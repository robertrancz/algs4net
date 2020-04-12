/******************************************************************************
 *  File name :    Quick.cs
 *  Demo test :    Use the algscmd util or Visual Studio IDE
 *            :    Enter algscmd alone for how to use the util
 *  Data files:   http://algs4.cs.princeton.edu/23quicksort/tiny.txt
 *                http://algs4.cs.princeton.edu/23quicksort/words3.txt
 *
 *  Sorts a sequence of strings from standard input using quicksort.
 *
 *  C:\> type tiny.txt
 *  S O R T E X A M P L E
 *
 *  C:\> algscmd Quick < tiny.txt
 *  A E E L M O P R S T X                 [ one string per line ]
 *
 *  C:\> type words3.txt
 *  bed bug dad yes zoo ... all bad yet
 *
 *  C:\> algscmd Quick < words3.txt
 *  all bad bed bug dad ... yes yet zoo    [ one string per line ]
 *
 *
 *  Remark: For a type-safe version that uses static generics, see
 *
 *    http://algs4.cs.princeton.edu/23quicksort/QuickPedantic.java
 *
 ******************************************************************************/
using System;
using System.Diagnostics;

namespace algs4net
{
    /// <summary>
    /// The <c>Quick</c> class provides static methods for sorting an
    /// array and selecting the ith smallest element in an array using quicksort.</summary>
    /// <remarks>
    /// For additional documentation, see <a href="http://algs4.cs.princeton.edu/21elementary">Section 2.1</a> of
    /// <em>Algorithms, 4th Edition</em> by Robert Sedgewick and Kevin Wayne.
    /// <para>This class is a C# port from the original Java class 
    /// <a href="http://algs4.cs.princeton.edu/code/edu/princeton/cs/algs4/Quick.java.html">Quick</a> implementation by
    /// Robert Sedgewick and Kevin Wayne.</para></remarks>
    ///
    public static class Quick
    {
        // This class should not be instantiated.
        static Quick() { }

        // TODO: Add a generic verion

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.</summary>
        /// <param name="a">a the array to be sorted</param>
        ///
        public static void Sort(IComparable[] a)
        {
            if (a == null)
                throw new ArgumentNullException(nameof(a), "The array to be sorted cannot be null");

            StdRandom.Shuffle(a);
            Sort(a, 0, a.Length - 1);
            Debug.Assert(OrderHelper.IsSorted(a));
        }

        // quicksort the subarray from a[lo] to a[hi]
        private static void Sort(IComparable[] a, int lo, int hi)
        {
            if (hi <= lo) return;
            int j = Partition(a, lo, hi);
            Sort(a, lo, j - 1);
            Sort(a, j + 1, hi);
            Debug.Assert(OrderHelper.IsSorted(a, lo, hi));
        }

        // partition the subarray a[lo..hi] so that a[lo..j-1] <= a[j] <= a[j+1..hi]
        // and return the index j.
        private static int Partition(IComparable[] a, int lo, int hi)
        {
            int i = lo;
            int j = hi + 1;
            IComparable v = a[lo];
            while (true)
            {

                // find item on lo to swap
                while (OrderHelper.Less(a[++i], v))
                    if (i == hi) break;

                // find item on hi to swap
                while (OrderHelper.Less(v, a[--j]))
                    if (j == lo) break;      // redundant since a[lo] acts as sentinel

                // check if pointers cross
                if (i >= j) break;

                OrderHelper.Exch(a, i, j);
            }

            // put partitioning item v at a[j]
            OrderHelper.Exch(a, lo, j);

            // now, a[lo .. j-1] <= a[j] <= a[j+1 .. hi]
            return j;
        }

        /// <summary>
        /// Rearranges the array so that a[k] contains the kth smallest key;
        /// a[0] through a[k-1] are OrderHelper.Less than (or equal to) a[k]; and
        /// a[k+1] through a[N-1] are greater than (or equal to) a[k].</summary>
        /// <param name="a">a the array</param>
        /// <param name="k">k find the kth smallest</param>
        /// <returns>the rearranged array</returns>
        /// <exception cref="IndexOutOfRangeException"> if k is out of range</exception>
        ///
        public static IComparable Select(IComparable[] a, int k)
        {
            if (a == null)
                throw new ArgumentNullException(nameof(a), "The array cannot be null");
            if (k < 0 || k >= a.Length)
            {
                throw new IndexOutOfRangeException("Selected element out of bounds");
            }
            StdRandom.Shuffle(a);
            int lo = 0, hi = a.Length - 1;
            while (hi > lo)
            {
                int i = Partition(a, lo, hi);
                if (i > k) hi = i - 1;
                else if (i < k) lo = i + 1;
                else return a[i];
            }
            return a[lo];
        }

        /// <summary>
        /// Reads in a sequence of strings from standard input; quicksorts them;
        /// and prints them to standard output in ascending order.
        /// Shuffles the array and then prints the strings again to
        /// standard output, but this time, using the select method.</summary>
        /// <param name="args">Place holder for user arguments</param>
        /// 
        [HelpText("algscmd Quick < words3.txt", "Input strings to be printed in sorted order")]
        public static void MainTest(string[] args)
        {
            TextInput StdIn = new TextInput();
            string[] a = StdIn.ReadAllStrings();
            OrderHelper.Show(a);

            // shuffle
            StdRandom.Shuffle(a);

            // display results again using select
            Console.WriteLine();
            for (int i = 0; i < a.Length; i++)
            {
                string ith = (string)Quick.Select(a, i);
                Console.WriteLine(ith);
            }
        }
    }
}

