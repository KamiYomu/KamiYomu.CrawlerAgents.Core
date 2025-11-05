using System;
using System.ComponentModel;

namespace KamiYomu.CrawlerAgents.Core.Inputs
{
    /// <summary>
    /// Represents a configurable capability or toggleable option exposed by an agent class.
    /// Each <see cref="CrawlerCheckBoxAttribute"/> defines a checkbox-driven setting or metadata flag
    /// that can influence the crawler's behavior, execution mode, or diagnostic output.
    /// Multiple instances of this attribute may be applied to a single agent to describe its complete set of supported features.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class CrawlerCheckBoxAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrawlerCheckBoxAttribute"/> class.
        /// </summary>
        /// <param name="name">The internal identifier for this option (e.g., "contentRating").</param>
        /// <param name="legend">A descriptive label or explanation for the option.</param>
        /// <param name="options">One or more flags or identifiers associated with this option (e.g., "general", "safe", "14") keep in mind you will receive the format value as "<see cref="Name"/>.<see cref="Options"/>" .</param>
        public CrawlerCheckBoxAttribute(string name, string legend, string[] options)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Legend = legend;
            Options = options?.Length > 0 ? options : throw new ArgumentException("Must contains items", nameof(options));
        }

        /// <summary>
        /// Gets the internal identifier for this option.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the descriptive label or explanation for this option.
        /// </summary>
        public string Legend { get; }

        /// <summary>
        /// Gets the options or identifiers that activate this option in the agent's runtime.
        /// </summary>
        public string[] Options { get; }

        /// <inheritdoc/>
        public override string ToString() => $"{Name} ({string.Join(", ", Options)}) {Legend}";
    }
}
