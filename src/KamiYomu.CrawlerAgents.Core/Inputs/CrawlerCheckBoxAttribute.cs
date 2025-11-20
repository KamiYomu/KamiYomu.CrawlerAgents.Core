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
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class CrawlerCheckBoxAttribute : AbstractInputAttribute
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
        public CrawlerCheckBoxAttribute(string name, string legend, string[] options) : base(name, legend)
        {
            Options = options?.Length > 0 ? options : throw new ArgumentException("Must contains items", nameof(options));
        }

        /// <inheritdoc/>
        /// <summary>
        /// Initializes a new instance of the <see cref="CrawlerCheckBoxAttribute"/> class.
        /// </summary>
        /// <param name="name">An identifier of the option (e.g., "Password").</param>
        /// <param name="legend">A descriptive label or explanation for the option.</param>
        /// <param name="required">Whether this option is required for processing or validation.</param>
        /// <param name="options">One or more  flags or identifiers associated with this option (e.g.,  "en", "pt" "fr").</param>
        /// <param name="defaultValue">Set the default value for this option</param>
        public CrawlerCheckBoxAttribute(string name, string legend, bool required, string defaultValue, string[] options)
            : this(name, legend, options)
        {
            Required = required;
            Order = 0;
            DefaultValue = defaultValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrawlerCheckBoxAttribute"/> class.
        /// </summary>
        /// <param name="name">An identifier of the option (e.g., "Password").</param>
        /// <param name="legend">A descriptive label or explanation for the option.</param>
        /// <param name="required">Whether this option is required for processing or validation.</param>
        /// <param name="order">Specifies the display order of this field relative to other fields.</param>
        /// <param name="options">One or more  flags or identifiers associated with this option (e.g.,  "en", "pt" "fr").</param>
        /// <param name="defaultValue">Set the default value for this option</param>
        public CrawlerCheckBoxAttribute(string name, string legend, bool required, string defaultValue, short order, string[] options)
            : this(name, legend, required, defaultValue, options)
        {
            Required = required;
            Order = order;
        }

        /// <summary>
        /// Gets the options or identifiers that activate this option in the agent's runtime.
        /// </summary>
        public string[] Options { get; }


        /// <inheritdoc/>
        public override string ToString() => $"Order={Order} | Name=\"{Name}\" | Legend=\"{Legend}\" | Options=[{string.Join(", ", Options)}] | Required={Required} | Type=\"{nameof(CrawlerCheckBoxAttribute)}\"";
    }
}
