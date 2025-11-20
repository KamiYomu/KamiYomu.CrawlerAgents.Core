namespace KamiYomu.CrawlerAgents.Core.Inputs;

/// <summary>
/// Serves as a base class for defining custom input attributes.
/// </summary>
/// <remarks>Inherit from this class to create attributes that can be used to annotate input parameters or
/// properties, providing metadata or behavior customization in derived classes.</remarks>
/// 
public abstract class AbstractInputAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CrawlerTextAttribute"/> class.
    /// </summary>
    /// <param name="name">An identifier of the input.text field (e.g., "Username").</param>
    public AbstractInputAttribute(string name, string legend)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        Name = name;
        Legend = legend;
        DefaultValue = string.Empty;
        Order = 0;
        Required = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CrawlerTextAttribute"/> class.
    /// </summary>
    /// <param name="name">An identifier of the option (e.g., "Password").</param>
    /// <param name="legend">A descriptive label or explanation for the option.</param>
    /// <param name="required">Whether this option is required for processing or validation.</param>
    /// <param name="defaultValue">Set the default value for this option</param>
    public AbstractInputAttribute(string name, string legend, bool required, string defaultValue)
        : this(name, legend)
    {
        Required = required;
        DefaultValue = defaultValue; 
        Order = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CrawlerTextAttribute"/> class.
    /// </summary>
    /// <param name="name">An identifier of the option (e.g., "Password").</param>
    /// <param name="legend">A descriptive label or explanation for the option.</param>
    /// <param name="required">Whether this option is required for processing or validation.</param>
    /// <param name="order">Specifies the display order of this field relative to other fields.</param>
    public AbstractInputAttribute(string name, string legend, bool required, string defaultValue, short order)
        : this(name, legend, required, defaultValue)
    {
        Order = order;
    }

    /// <summary>
    /// Gets the internal identifier for this option.
    /// </summary>
    public string Name { get; protected set; }
    /// <summary>
    /// Gets the descriptive label or explanation for this option.
    /// </summary>
    public string Legend { get; protected set; }
    /// <summary>
    /// Gets a value indicating whether existing value.
    /// </summary>
    public string DefaultValue { get; protected set; }
    /// <summary>
    /// Specifies the display order of this field relative to other options.
    /// </summary>
    public short Order { get; protected set; } = 0;
    /// <summary>
    /// Indicates whether this option is required for processing or validation.
    /// </summary>
    public bool Required { get; protected set; } = false;

    /// <inheritdoc/>
    public override string ToString() => $"Order={Order} | Name=\"{Name}\" | Legend=\"{Legend}\" | Required={Required} | Type=\"{this.GetType().Name}\"";
}
