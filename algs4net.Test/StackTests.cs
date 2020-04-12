using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace algs4net.Test
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void StackTest1()
        {
            var stack = new Stack<int>();

            Assert.AreEqual(0, stack.Size, "New stack has zero size");
            Assert.AreEqual(true, stack.IsEmpty, "New stack is empty");
            Assert.AreEqual("", IteratorToString(stack));
        }

        [TestMethod]
        public void StackTest2()
        {
            int[] input = { 3, 2, 4, 6, 9 };
            var stack = new Stack<int>();

            stack.Push(3);
            Assert.AreEqual("3 ", IteratorToString(stack));
            stack.Push(4);

            Assert.AreEqual("4 3 ", IteratorToString(stack));
            Assert.AreEqual(4, stack.Pop());
            Assert.AreEqual(3, stack.Pop());
            Assert.AreEqual(0, stack.Size, "New stack has zero size");

            foreach (int i in input) stack.Push(i);
            Assert.AreEqual(input.Length, stack.Size);
            Assert.AreEqual("9 6 4 2 3 ", IteratorToString(stack));
        }

        private string IteratorToString(Stack<int> input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in input) sb.Append(i + " ");
            return sb.ToString();
        }
    }
}
