using System;
using System.Collections.Generic;
using System.Text;

namespace KamiYomu.CrawlerAgents.Core.Inputs
{
    /// <summary>
    /// Defines a configurable capability or option exposed by an agent class.
    /// Each <see cref="CrawlerCheckBoxAttribute"/> represents an input selector, runtime toggle,
    /// or metadata flag that can influence the crawler's behavior, execution mode, or diagnostic output.
    /// Multiple attributes can be applied to a single agent to describe its complete set of supported features.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class CrawlerSelectAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrawlerSelectAttribute"/> class.
        /// </summary>
        /// <param name="name">An identifier of the option (e.g., "Language").</param>
        /// <param name="legend">A descriptive label or explanation for the option.</param>
        /// <param name="options">One or more  flags or identifiers associated with this option (e.g.,  "en", "pt" "fr").</param>
        public CrawlerSelectAttribute(string name, string legend, string[] options)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Legend = legend;
            Options = options?.Length > 0 ? options : throw new ArgumentException("Must contains items", nameof(options));
        }
        /// <summary>
        /// Gets the key for this option, used as internal identifier.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the descriptive label or explanation for this option.
        /// </summary>
        public string Legend { get; }

        /// <summary>
        /// Gets the associated flags or identifiers that activate this option in the agent's runtime.
        /// </summary>
        public string[] Options { get; }

        /// <inheritdoc/>
        public override string ToString() => $"{Name} ({string.Join(", ", Options)})";
    }
}
