using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace algs4net
{
    /// <summary><para>
    /// The <c>TextInput</c> class provides static methods for reading strings
    /// and numbers from standard input. It mimics the Java's Scanner class
    /// and adapt from the <c>StdIn</c> class from the textbook.</para></summary>
    /// <remarks>
    /// For additional documentation,
    /// see <a href="http://introcs.cs.princeton.edu/15inout">Section 1.5</a> of   
    /// <c>Introduction to Programming in Java: An Interdisciplinary Approach</c>
    /// by Robert Sedgewick and Kevin Wayne.</remarks>
    ///
    public sealed class TextInput
    {
        internal static readonly Regex WhiteSpace = new Regex(@"[\s]+", RegexOptions.Compiled);

        private readonly TextReader input = null;
        private string nextToken = "";
        private string buffer = "";
        private bool newBuffer = true;

        /// <summary>
        /// Open a file for reading. Exceptions associated with the file open
        /// operation may be thrown
        /// </summary>
        /// <param name="inputFileName">the name of the file to read from</param>
        public TextInput(string inputFileName)
        {
            input = new StreamReader(File.OpenRead(inputFileName));
        }

        /// <summary>
        /// Connect to the console for reading
        /// </summary>
        public TextInput()
        {
            input = Console.In;
        }

        // get the next white space separated token from the buffer
        private void NextToken()
        {
            if (buffer == "") buffer = input.ReadLine();
            if (buffer != null)
            {
                Match m = Regex.Match(buffer, @"\s*\S+\s*", RegexOptions.Compiled);
                buffer = buffer.Substring(m.Value.Length);
                nextToken = m.Value.Trim();
                if (nextToken.Equals("")) buffer = ""; // blank line
                newBuffer = false;
            }
        }

        // see if there it a token without removing it from the buffer
        private void TryNextToken()
        {
            if (buffer.Equals(""))
            {
                buffer = input.ReadLine();
                newBuffer = true;
            }
            if (buffer != null)
            {
                Match m = Regex.Match(buffer, @"\s*\S+\s*", RegexOptions.Compiled);
                //buffer = buffer.Substring(m.Value.Length);
                nextToken = m.Value.Trim();
            }
        }

        /// <summary>
        /// Check if there is no more input from the character stream
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                TryNextToken();
                return buffer == null;
            }
        }

        /// <summary>
        /// Check if there is an integer token from the coming stream
        /// </summary>
        /// <returns>true if an integer can be obtained from the read next</returns>
        public bool HasNextInt()
        {
            TryNextToken();
            if (buffer == null)
                return false;
            int dummy;
            return int.TryParse(nextToken, out dummy);
        }

        /// <summary>
        /// Reads an integer from the stream
        /// </summary>
        /// <exception cref="FormatException">if the token can't be parsed to an integer</exception>
        /// <returns>an integer</returns>
        public int ReadInt()
        {
            NextToken();
            try
            {
                return int.Parse(nextToken);
            }
            catch (Exception)
            {
                throw new FormatException("error reading int");
            }
        }

        /// <summary>
        /// Check if there is a double token from the coming stream
        /// </summary>
        /// <returns>true if a double can be obtained from the read next</returns>
        public bool HasNextDouble()
        {
            TryNextToken();
            if (buffer == null)
                return false;
            double dummy;
            return double.TryParse(nextToken, out dummy);
        }

        /// <summary>
        /// Reads a double from the stream
        /// </summary>
        /// <exception cref="FormatException">if the token can't be parsed to an double</exception>
        /// <returns>a double</returns>
        public double ReadDouble()
        {
            NextToken();
            try
            {
                return double.Parse(nextToken);
            }
            catch (Exception)
            {
                throw new FormatException("error reading double");
            }
        }

        /// <summary>
        /// Check if there is a char from the coming stream
        /// </summary>
        /// <returns>true if a char can be obtained from the next read</returns>
        public bool HasNextChar()
        {
            if (buffer.Equals(""))
            {
                buffer = input.ReadLine();
                newBuffer = true;
            }
            if (buffer == null)
                return false;
            return (buffer != "");
        }

        /// <summary>
        /// Reads a char from the stream
        /// </summary>
        /// <exception cref="FormatException">if the char can't be obtained</exception>
        /// <returns>a char</returns>
        public char ReadChar()
        {
            if (buffer.Equals(""))
            {
                buffer = input.ReadLine();
                newBuffer = true;
            }
            if (buffer == null)
                throw new FormatException("end of file might have been reached");

            if (buffer.Length > 0)
            {
                char nextChar = buffer[0];
                buffer = buffer.Substring(1);
                return nextChar;
            }
            else
                throw new FormatException("error reading char");
        }

        /// <summary>
        /// Checks if there is a space-delimited string from the stream
        /// </summary>
        /// <returns>true if there is a space-delimited string</returns>
        public bool HasNextString()
        {
            TryNextToken();
            return (nextToken != "");
        }

        /// <summary>
        /// Reads a space-delimited string from the stream
        /// </summary>
        /// <returns>the next space-delimited string</returns>
        public string ReadString()
        {
            NextToken();
            string s = nextToken;
            return s;
        }

        /// <summary>
        /// Checks if there is a bool value (1, 0, true or false) from the input stream
        /// </summary>
        /// <returns>true if there is a bool value, case-insensitive</returns>
        public bool HasNextBool()
        {
            TryNextToken();
            if (nextToken.Equals("true", StringComparison.InvariantCultureIgnoreCase) ||
                nextToken.Equals("false", StringComparison.InvariantCultureIgnoreCase) ||
                nextToken.Equals("1") || nextToken.Equals("0"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Reads a bool value (1, 0, true or false, case-insensitive) from the input stream
        /// </summary>
        /// <returns>the bool value</returns>
        public bool ReadBool()
        {
            NextToken();
            if (nextToken.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                return true;
            else if (nextToken.Equals("false", StringComparison.InvariantCultureIgnoreCase))
                return false;
            else if (nextToken.Equals("1"))
                return true;
            else if (nextToken.Equals("0"))
                return false;
            else
                throw new FormatException("error reading bool");
        }

        /// <summary>
        /// Check if there is a line, which is essentially a sequence of any
        /// characters
        /// </summary>
        /// <returns>true if a string can be read next</returns>
        public bool HasNextLine()
        {
            TryNextToken();
            return (buffer != null);
        }

        /// <summary>
        /// Read a whole new line, discarding unprocessed strings in the previous line
        /// </summary>
        /// <returns>the new line content</returns>
        public string ReadLine()
        {
            string s;
            if (!newBuffer)
                buffer = input.ReadLine();
            s = buffer;
            buffer = "";
            return s;
        }

        /// <summary>
        /// Read all characters from the steam into a string
        /// </summary>
        /// <returns>the rest of the file in a string</returns>
        public string ReadAll()
        {
            return this.ReadLine() + input.ReadToEnd();
        }

        /// <summary>
        /// Reads all space-delimited strings from the input stream as an
        /// array of strings
        /// </summary>
        /// <returns>the array of strings</returns>
        public string[] ReadAllStrings()
        {
            List<string> allStrs = new List<string>();
            string[] ss = null;
            string bigStr = ReadAll();
            ss = WhiteSpace.Split(bigStr.Trim());
            /*
            buffer = input.ReadLine();
            while (buffer != null)
            {
              ss = WhiteSpace.Split(buffer.Trim());
              foreach (string str in ss) allStrs.Add(str);
              buffer = input.ReadLine();
            }
            if (allStrs.Count > 0)
              ss = allStrs.ToArray<string>();
            */
            return ss;
        }

        /// <summary>
        /// Reads all integers from the input stream as an
        /// array of integers
        /// </summary>
        /// <returns>the array of integers</returns>
        public int[] ReadAllInts()
        {
            string[] ss = ReadAllStrings();
            int[] vals = new int[ss.Length];
            try
            {
                for (int i = 0; i < ss.Length; i++)
                    vals[i] = int.Parse(ss[i]);
            }
            catch (Exception)
            {
                throw new FormatException("error reading all ints");
            }
            return vals;
        }

        /// <summary>
        /// Reads all doubles from the input stream as an
        /// array of doubles
        /// </summary>
        /// <returns>the array of doubles</returns>
        public double[] ReadAllDoubles()
        {
            string[] ss = ReadAllStrings();
            double[] vals = new double[ss.Length];
            try
            {
                for (int i = 0; i < ss.Length; i++)
                    vals[i] = double.Parse(ss[i]);
            }
            catch (Exception)
            {
                throw new FormatException("error reading all doubles");
            }
            return vals;
        }

        /// <summary>
        /// Closes the input stream
        /// </summary>
        public void Close()
        {
            if (input != null) input.Close();
        }

        /// <summary>
        /// Closes the input stream
        /// </summary>
        ~TextInput()
        {
            Close();
        }

        /// <summary>
        /// Demo test for the <c>TextInput</c> data type. The test shows
        /// the methods' behavior and how to use them.
        /// </summary>
        /// <param name="args">Place holder for user arguments</param>
        public static void MainTest(string[] args)
        {
            TextInput StdIn = new TextInput();
            Console.Write("Type 3 char, 1 integer, a few strings: ");
            char[] c = { StdIn.ReadChar(), StdIn.ReadChar(), StdIn.ReadChar() };
            int a = StdIn.ReadInt();
            string s = StdIn.ReadLine();
            Console.WriteLine("3 char: {0}, 1 int: {1}, new line: {2}", new string(c), a, s);

            Console.Write("Type a string: ");
            s = StdIn.ReadString();
            Console.WriteLine("Your string was: " + s);
            Console.WriteLine();

            Console.Write("Type an int: ");
            a = StdIn.ReadInt();
            Console.WriteLine("Your int was: " + a);
            Console.WriteLine();

            Console.Write("Type a bool: ");
            bool b = StdIn.ReadBool();
            Console.WriteLine("Your bool was: " + b);
            Console.WriteLine();

            Console.Write("Type a double: ");
            double d = StdIn.ReadDouble();
            Console.WriteLine("Your double was: " + d);
            Console.WriteLine();

            Console.WriteLine("Enter a line:");
            s = StdIn.ReadLine();
            Console.WriteLine("Your line was: " + s);
            Console.WriteLine();

            Console.Write("Type any thing you like, enter Ctrl-Z to finish: ");
            string[] all = StdIn.ReadAllStrings();
            Console.WriteLine("Your remaining input was:");
            foreach (var str in all)
            {
                Console.Write(str + " ");
            }
            Console.WriteLine();
        }
    }
}
