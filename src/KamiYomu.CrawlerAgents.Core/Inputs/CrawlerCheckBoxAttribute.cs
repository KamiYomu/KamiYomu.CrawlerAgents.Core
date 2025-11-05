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
        /// Initializes a new instance of the <see cref="CrawlerCheckBoxAttribute"/> class, representing a configurable checkbox option
        /// used in crawler metadata or filtering. This attribute supports labeled grouping and multiple selectable flags.
        /// </summary>
        /// <param name="name">The internal key or identifier for this option (e.g., "contentRating").</param>
        /// <param name="legend">A human-readable label or description that explains the purpose of the checkbox group.</param>
        /// <param name="options">
        /// A set of selectable values or flags associated with this option (e.g., "general", "safe", "14").
        /// The final value will be formatted as list of <c>Name.OptionSelected</c> to support structured identification.
        /// </param>
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
