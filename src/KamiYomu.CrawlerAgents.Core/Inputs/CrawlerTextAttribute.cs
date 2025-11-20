namespace KamiYomu.CrawlerAgents.Core.Inputs;

/// <summary>
/// Represents a configurable capability or toggleable option exposed by an agent class.
/// Each <see cref="CrawlerTextAttribute"/> defines a input-text type
/// that can influence the crawler's behavior, execution mode, or diagnostic output.
/// Multiple instances of this attribute may be applied to a single agent to describe its complete set of supported features.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class CrawlerTextAttribute : AbstractInputAttribute
{
    /// <inheritdoc/>
    public CrawlerTextAttribute(string name, string legend) : base(name, legend)
    {
    }
    /// <inheritdoc/>
    public CrawlerTextAttribute(string name, string legend, bool required, string defaultValue) : base(name, legend, required, defaultValue)
    {
    }
    /// <inheritdoc/>
    public CrawlerTextAttribute(string name, string legend, bool required, string defaultValue, short order) : base(name, legend, required, defaultValue, order)
    {
    }
}
