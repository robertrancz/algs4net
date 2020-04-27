using algs4net;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algs4NetUnitTests
{
    [TestClass]
    public class QuickTests
    {
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void QuickTest1()
        {
            string[] a = new string[] { "aba" };
            string s;

            Quick.Sort(a);
            s = (string)Quick.Select(a, 0);
            Assert.AreEqual(s, a[0]);

            a = new string[] { "zoo", "able", "after", "cury", "aba", "bed", "bug", "boy", "bing", " " };
            s = (string)Quick.Select(a, a.Length - 1);
            Assert.AreEqual(s, "zoo");

            Quick.Sort(a);
            Assert.AreEqual("aba", a[1]);

            Quick.Select(a, a.Length); // generate exception
        }
    }
}
