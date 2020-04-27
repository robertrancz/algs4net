using algs4net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algs4NetUnitTests
{
    [TestClass]
    public class BagTests
    {
        [TestMethod]
        public void BagTest1()
        {
            string expected = "";
            Bag<int> bag = new Bag<int>();
            StringBuilder sb = new StringBuilder();
            foreach (var s in bag)
            {
                sb.Append(string.Format("{0} ", s));
            }
            Assert.IsTrue(bag.IsEmpty);
            Assert.AreEqual(expected, bag.ToString());
            Assert.AreEqual(expected, sb.ToString());

            bag.Add(3);
            expected = "3";
            Assert.AreEqual(expected, bag.ToString());

            bag.Add(2);
            bag.Add(3);
            Assert.IsFalse(bag.IsEmpty);
            Assert.AreEqual(3, bag.Count);
            expected = "3 2 3";
            Assert.AreEqual(expected, bag.ToString());

            Bag<int> bag2 = new Bag<int>();
            for (int i = 0; i < 10; i++)
            {
                bag.Add(i);
            }
            expected = "9 8 7 6 5 4 3 2 1 0 3 2 3";
            Assert.AreEqual(expected, bag.ToString());

            sb = new StringBuilder();
            sb.Append("[ ");
            foreach (var s in bag)
            {
                sb.Append(string.Format("{0} ", s));
            }
            sb.Append("]");
            Assert.AreEqual("[ " + expected + " ]", sb.ToString());
        }
    }
}