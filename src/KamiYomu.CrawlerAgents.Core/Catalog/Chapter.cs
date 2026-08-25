namespace KamiYomu.CrawlerAgents.Core.Catalog;

/// <summary>
/// Represents a chapter of a manga series.
/// </summary>
/// <remarks>
/// This class encapsulates metadata about a specific chapter, including volume and chapter numbers,
/// title, description, and publication information. It maintains a reference to its parent manga
/// and includes localization information through the translated language property.
/// </remarks>
public class Chapter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Chapter"/> class.
    /// </summary>
    public Chapter() { }

    /// <summary>
    /// Gets or sets the unique identifier for this chapter.
    /// </summary>
    /// <value>A string representing the unique identifier.</value>
    [JsonInclude]
    public string Id { get; internal set; }
    
    /// <summary>
    /// Gets or sets the volume number this chapter belongs to.
    /// </summary>
    /// <value>A decimal representing the volume number.</value>
    [JsonInclude]
    public decimal Volume { get; internal set; }
    
    /// <summary>
    /// Gets or sets the chapter number within its volume.
    /// </summary>
    /// <value>A decimal representing the chapter number.</value>
    [JsonInclude]
    public decimal Number { get; internal set; }
    
    /// <summary>
    /// Gets or sets the title of this chapter.
    /// </summary>
    /// <value>A string containing the chapter title.</value>
    [JsonInclude]
    public string Title { get; internal set; }

    /// <summary>
    /// Gets or sets the description or synopsis of this chapter.
    /// </summary>
    /// <value>A string containing the chapter description.</value>
    [JsonInclude]
    public string Description { get; internal set; }

    /// <summary>
    /// Gets or sets the language in which this chapter is translated.
    /// </summary>
    /// <value>A string representing the translation language code or name.</value>
    [JsonInclude]
    public string TranslatedLanguage { get; internal set; }
    
    /// <summary>
    /// Gets or sets the number of pages in this chapter.
    /// </summary>
    /// <value>A decimal representing the page count.</value>
    [JsonInclude]
    public decimal Pages { get; internal set; }
    
    /// <summary>
    /// Gets or sets the URI where this chapter can be accessed.
    /// </summary>
    /// <value>A <see cref="Uri"/> pointing to the chapter resource.</value>
    [JsonInclude]
    public Uri Uri { get; internal set; }
    
    /// <summary>
    /// Gets or sets the release date of this chapter.
    /// </summary>
    /// <value>A <see cref="DateTime"/> representing when the chapter was released.</value>
    [JsonInclude]
    public DateTime ReleaseDate { get; internal set; }
    
    /// <summary>
    /// Gets or sets the parent manga series this chapter belongs to.
    /// </summary>
    /// <value>A <see cref="Manga"/> instance representing the parent series.</value>
    [JsonInclude]
    public Manga ParentManga { get; internal set; }
}
