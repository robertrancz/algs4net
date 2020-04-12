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
        public readonly string Usage;

        /// <summary>
        /// Additional details about the <c>Usage</c>
        /// </summary>
        public readonly string Details;

        private HelpTextAttribute() { }

        /// <summary>
        /// Construct a help text with a usage and optional details texts
        /// </summary>
        /// <param name="usage">The command line format</param>
        /// <param name="details">Description of command line arguments, if any</param>
        public HelpTextAttribute(string usage, string details = "")
        {
            Usage = usage;
            Details = details;
        }
    }
}
