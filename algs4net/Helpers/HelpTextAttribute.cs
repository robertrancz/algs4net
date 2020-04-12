using System;

namespace algs4net
{
    /// <summary>
    /// Provides help text for a method, mainly used for decorating the demo tests
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class HelpTextAttribute : Attribute
    {

        /// <summary>
        /// Usage of a test method
        /// </summary>
        public string Usage { get; }

        /// <summary>
        /// Additional details about the <c>Usage</c>
        /// </summary>
        public string Details { get; }

        private HelpTextAttribute() { }

        /// <summary>
        /// Construct a help text with a usage and optional details texts
        /// </summary>
        /// <param name="usage">The command line format</param>
        /// <param name="details">Description of command line arguments, if any</param>
        public HelpTextAttribute(string usage, string details = "")
        {
            Details = details;
            Usage = usage;
        }
    }
}
