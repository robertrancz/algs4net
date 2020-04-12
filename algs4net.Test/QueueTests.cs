using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace algs4net.Test
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void QueueTest1()
        {
            Queue<int> queue = new Queue<int>();

            Assert.AreEqual(0, queue.Size, "New queue has zero size");
            Assert.AreEqual(true, queue.IsEmpty, "New queue is empty");
            Assert.AreEqual("", IteratorToString(queue));
        }

        [TestMethod]
        public void QueueTest2()
        {
            int[] input = { 3, 2, 4, 6, 9 };
            Queue<int> queue = new Queue<int>();

            queue.Enqueue(3);
            Assert.AreEqual("3 ", IteratorToString(queue));
            queue.Enqueue(4);

            Assert.AreEqual("3 4 ", IteratorToString(queue));
            Assert.AreEqual(3, queue.Dequeue());
            Assert.AreEqual(4, queue.Dequeue());
            Assert.AreEqual(0, queue.Size, "New queue has zero size");

            foreach (int i in input) queue.Enqueue(i);
            Assert.AreEqual(input.Length, queue.Size);
            Assert.AreEqual("3 2 4 6 9 ", IteratorToString(queue));
        }

        private string IteratorToString(Queue<int> input)
        {
            var sb = new StringBuilder();
            foreach (int i in input)
                sb.Append(i + " ");
            return sb.ToString();
        }
    }
}
