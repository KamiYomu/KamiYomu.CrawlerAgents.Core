using Microsoft.Extensions.Options;

namespace KamiYomu.CrawlerAgents.Core.Inputs
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class CrawlerTextAttribute : AbstractInputAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrawlerTextAttribute"/> class.
        /// </summary>
        /// <param name="name">An identifier of the input.text field (e.g., "Username").</param>
        public CrawlerTextAttribute(string name, string legend)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Legend = legend; 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrawlerTextAttribute"/> class.
        /// </summary>
        /// <param name="name">An identifier of the option (e.g., "Password").</param>
        /// <param name="legend">A descriptive label or explanation for the option.</param>
        /// <param name="required">Whether this option is required for processing or validation.</param>
        public CrawlerTextAttribute(string name, string legend, bool required)
            : this(name, legend)
        {
            Required = required;
            Order = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrawlerTextAttribute"/> class.
        /// </summary>
        /// <param name="name">An identifier of the option (e.g., "Password").</param>
        /// <param name="legend">A descriptive label or explanation for the option.</param>
        /// <param name="required">Whether this option is required for processing or validation.</param>
        /// <param name="order">Specifies the display order of this field relative to other fields.</param>
        public CrawlerTextAttribute(string name, string legend, bool required, short order)
            : this(name, legend)
        {
            Required = required;
            Order = order;
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
        /// Indicates whether this option is required for processing or validation.
        /// </summary>
        public bool Required { get; } = false;

        /// <summary>
        /// Specifies the display order of this field relative to other fields.
        /// </summary>
        public short Order { get; } = 0;

        /// <inheritdoc/>
        public override string ToString() => $"Order={Order} | Name=\"{Name}\" | Legend=\"{Legend}\" | Required={Required} | Type=\"{nameof(CrawlerTextAttribute)}\"";
    }
}
