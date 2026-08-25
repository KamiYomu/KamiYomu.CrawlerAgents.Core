namespace KamiYomu.CrawlerAgents.Core.Catalog;

/// <summary>
/// Represents a single page within a chapter of a manga or comic.
/// </summary>
/// <remarks>
/// The <see cref="Page"/> class encapsulates page-specific metadata including
/// identification, ordering, and image resources. Pages are organized hierarchically
/// under chapters and maintain a reference to their parent chapter for context.
/// </remarks>
public class Page
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Page"/> class.
    /// </summary>
    public Page() { }

    /// <summary>
    /// Gets or sets the unique identifier for this page.
    /// </summary>
    /// <value>A string representing the page's unique ID.</value>
    [JsonInclude]
    public string Id { get; internal set; }

    /// <summary>
    /// Gets or sets the identifier of the chapter containing this page.
    /// </summary>
    /// <value>A string representing the parent chapter's ID.</value>
    [JsonInclude]
    public string ChapterId { get; internal set; }

    /// <summary>
    /// Gets or sets the page number within the chapter.
    /// </summary>
    /// <value>A decimal representing the page's sequential position.</value>
    /// <remarks>
    /// Uses decimal to support decimal page numbering systems
    /// (e.g., pages 1.5, 2.5 for cover pages or special content).
    /// </remarks>
    [JsonInclude]
    public decimal PageNumber { get; internal set; }

    /// <summary>
    /// Gets or sets the URL to the page's image resource.
    /// </summary>
    /// <value>A <see cref="Uri"/> pointing to the image location.</value>
    [JsonInclude]
    public Uri ImageUrl { get; internal set; }

    /// <summary>
    /// Gets or sets the parent chapter containing this page.
    /// </summary>
    /// <value>A <see cref="Chapter"/> reference to the owning chapter.</value>
    [JsonInclude]
    public Chapter ParentChapter { get; internal set; }
}
