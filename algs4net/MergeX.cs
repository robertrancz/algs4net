/******************************************************************************
 *  File name :    MergeX.cs
 *  Demo test :    Use the algscmd util or Visual Studio IDE
 *            :    Enter algscmd alone for how to use the util
 *  Data files:   http://algs4.cs.princeton.edu/22mergesort/tiny.txt
 *                http://algs4.cs.princeton.edu/22mergesort/words3.txt
 *   
 *  Sorts a sequence of strings from standard input using an
 *  optimized version of mergesort.
 *   
 *  C:\> type tiny.txt
 *  S O R T E X A M P L E
 *
 *  C:\> algscmd MergeX < tiny.txt
 *  A E E L M O P R S T X                 [ one string per line ]
 *    
 *  C:\> type words3.txt
 *  bed bug dad yes zoo ... all bad yet
 *  
 *  C:\> algscmd MergeX < words3.txt
 *  all bad bed bug dad ... yes yet zoo    [ one string per line ]
 *
 ******************************************************************************/

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algs4net
{
    /// <summary>
    /// The <c>MergeX</c> class provides static methods for sorting an array using an optimized version of mergesort.</summary>
    /// <remarks><para>
    /// For additional documentation, see <a href="http://algs4.cs.princeton.edu/22mergesort">Section 2.2</a> of
    /// <i>Algorithms, 4th Edition</i> by Robert Sedgewick and Kevin Wayne.</para>
    /// <para>This class is a C# port from the original Java class
    /// <a href="http://algs4.cs.princeton.edu/code/edu/princeton/cs/algs4/MergeX.java.html">MergeX</a>
    /// implementation by the respective authors.</para></remarks>
    ///
    public static class MergeX
    {
        private const int CUTOFF = 7;  // cutoff to insertion sort

        // This class should not be instantiated.
        static MergeX() { }

        private static void Merge(IComparable[] src, IComparable[] dst, int lo, int mid, int hi)
        {
            // precondition: src[lo .. mid] and src[mid+1 .. hi] are sorted subarrays
            Debug.Assert(OrderHelper.IsSorted(src, lo, mid));
            Debug.Assert(OrderHelper.IsSorted(src, mid + 1, hi));

            int i = lo, j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid) dst[k] = src[j++];
                else if (j > hi) dst[k] = src[i++];
                else if (OrderHelper.Less(src[j], src[i])) dst[k] = src[j++];   // to ensure stability
                else dst[k] = src[i++];
            }

            // postcondition: dst[lo .. hi] is sorted subarray
            Debug.Assert(OrderHelper.IsSorted(dst, lo, hi));
        }

        private static void Sort(IComparable[] src, IComparable[] dst, int lo, int hi)
        {
            // if (hi <= lo) return;
            if (hi <= lo + CUTOFF)
            {
                Insertion.Sort(dst, lo, hi);
                return;
            }
            int mid = lo + (hi - lo) / 2;
            Sort(dst, src, lo, mid);
            Sort(dst, src, mid + 1, hi);

            if (!OrderHelper.Less(src[mid + 1], src[mid]))
            {
                Array.Copy(src, lo, dst, lo, hi - lo + 1);
                return;
            }
            Merge(src, dst, lo, mid, hi);
        }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.</summary>
        /// <param name="a">the array to be sorted</param>
        ///
        public static void Sort(IComparable[] a)
        {
            if (a == null)
                throw new ArgumentNullException(nameof(a), "The array to be sorted cannot be null");

            IComparable[] aux = new IComparable[a.Length];
            a.CopyTo(aux, 0);
            Sort(aux, a, 0, a.Length - 1);
            Debug.Assert(OrderHelper.IsSorted(a));
        }

        /*******************************************************************
         *  Version that takes Comparator as argument.
         *******************************************************************/

        /// <summary>
        /// Rearranges the array in ascending order, using the provided order.</summary>
        /// <param name="a">the array to be sorted</param>
        /// <param name="comparator">the user comparator</param>
        ///
        public static void Sort<T>(T[] a, Comparer<T> comparator)
        {
            if (a == null)
                throw new ArgumentNullException(nameof(a), "The array to be sorted cannot be null");

            T[] aux = new T[a.Length];
            a.CopyTo(aux, 0);
            Sort(aux, a, 0, a.Length - 1, comparator);
            Debug.Assert(OrderHelper.IsSorted(a, comparator));
        }

        private static void Merge<T>(T[] src, T[] dst, int lo, int mid, int hi, Comparer<T> comparator)
        {
            // precondition: src[lo .. mid] and src[mid+1 .. hi] are sorted subarrays
            Debug.Assert(OrderHelper.IsSorted(src, lo, mid, comparator));
            Debug.Assert(OrderHelper.IsSorted(src, mid + 1, hi, comparator));

            int i = lo, j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid) dst[k] = src[j++];
                else if (j > hi) dst[k] = src[i++];
                else if (OrderHelper.Less(src[j], src[i], comparator)) dst[k] = src[j++];
                else dst[k] = src[i++];
            }

            // postcondition: dst[lo .. hi] is sorted subarray
            Debug.Assert(OrderHelper.IsSorted(dst, lo, hi, comparator));
        }

        private static void Sort<T>(T[] src, T[] dst, int lo, int hi, Comparer<T> comparator)
        {
            // if (hi <= lo) return;
            if (hi <= lo + CUTOFF)
            {
                Insertion.Sort(dst, lo, hi, comparator);
                return;
            }
            int mid = lo + (hi - lo) / 2;
            Sort(dst, src, lo, mid, comparator);
            Sort(dst, src, mid + 1, hi, comparator);

            if (!OrderHelper.Less(src[mid + 1], src[mid], comparator))
            {
                Array.Copy(src, lo, dst, lo, hi - lo + 1);
                return;
            }

            Merge(src, dst, lo, mid, hi, comparator);
        }

        /// <summary>
        /// Reads in a sequence of strings from standard input; mergesorts them
        /// (using an optimized version of mergesort);
        /// and prints them to standard output in ascending order.
        /// </summary>
        /// <param name="args">Place holder for user arguments</param>
        ///
        [HelpText("algscmd MergeX < words3.txt", "Input strings to be printed in sorted order")]
        public static void MainTest(string[] args)
        {
            TextInput StdIn = new TextInput();
            string[] a = StdIn.ReadAllStrings();
            MergeX.Sort(a);
            OrderHelper.Show(a);
        }
    }
}
