using KamiYomu.CrawlerAgents.Core.Catalog.Definitions;

namespace KamiYomu.CrawlerAgents.Core.Catalog;

/// <summary>
/// Represents a manga title with comprehensive metadata information.
/// This class serves as a data transfer object (DTO) for manga catalog information
/// and is designed for JSON serialization/deserialization.
/// </summary>
/// <remarks>
/// The Manga class contains essential information about a manga series including
/// title variants, descriptions, creator information, media links, and availability status.
/// All properties are decorated with [JsonInclude] to ensure proper serialization behavior.
/// </remarks>
public class Manga
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Manga"/> class.
    /// </summary>
    public Manga() { }

    /// <summary>
    /// Gets or sets the unique identifier for the manga.
    /// </summary>
    [JsonInclude]
    public string Id { get; internal set; }
    
    /// <summary>
    /// Gets or sets the primary title of the manga.
    /// </summary>
    [JsonInclude]
    public string Title { get; internal set; }

    /// <summary>
    /// Gets or sets alternative titles for the manga, keyed by language or source.
    /// </summary>
    [JsonInclude]
    public Dictionary<string, string> AlternativeTitles { get; internal set; } = [];

    /// <summary>
    /// Gets or sets the primary description or synopsis of the manga.
    /// </summary>
    [JsonInclude]
    public string Description { get; internal set; }

    /// <summary>
    /// Gets or sets alternative descriptions for the manga, keyed by language or source.
    /// </summary>
    [JsonInclude]
    public Dictionary<string, string> AlternativeDescriptions { get; internal set; } = [];

    /// <summary>
    /// Gets or sets the collection of author names for the manga.
    /// </summary>
    [JsonInclude]
    public IEnumerable<string> Authors { get; internal set; } = [];

    /// <summary>
    /// Gets or sets the collection of artist names for the manga.
    /// </summary>
    [JsonInclude]
    public IEnumerable<string> Artists { get; internal set; } = [];

    /// <summary>
    /// Gets or sets the collection of genre or content tags associated with the manga.
    /// </summary>
    [JsonInclude]
    public IEnumerable<string> Tags { get; internal set; } = [];
    
    /// <summary>
    /// Gets or sets the URL to the manga's cover image.
    /// </summary>
    [JsonInclude]
    public Uri CoverUrl { get; internal set; }

    /// <summary>
    /// Gets or sets external links related to the manga, keyed by source or link type.
    /// </summary>
    [JsonInclude]
    public Dictionary<string, string> Links { get; internal set; } = [];
    
    /// <summary>
    /// Gets or sets the year the manga was first published.
    /// </summary>
    [JsonInclude]
    public int? Year { get; internal set; }
    
    /// <summary>
    /// Gets or sets the original language of the manga.
    /// </summary>
    [JsonInclude]
    public string OriginalLanguage { get; internal set; }

    /// <summary>
    /// Gets or sets the current publication status of the manga.
    /// </summary>
    [JsonInclude]
    public ReleaseStatus ReleaseStatus { get; internal set; }
    
    /// <summary>
    /// Gets or sets the folder name used for organizing the manga locally.
    /// </summary>
    [JsonInclude]
    public string FolderName { get; internal set; }
    
    /// <summary>
    /// Gets or sets the last volume number available for this manga.
    /// </summary>
    [JsonInclude]
    public decimal LastVolumeAvailable { get; internal set; }
    
    /// <summary>
    /// Gets or sets the latest chapter number available for this manga.
    /// </summary>
    [JsonInclude]
    public decimal LatestChapterAvailable { get; internal set; }
    
    /// <summary>
    /// Gets or sets the URL to the manga's primary web page or source.
    /// </summary>
    [JsonInclude]
    public string WebSiteUrl { get; internal set; }

    /// <summary>
    /// Gets or sets a value indicating whether the manga content is appropriate for all ages.
    /// Defaults to <c>true</c>.
    /// </summary>
    [JsonInclude]
    public bool IsFamilySafe { get; internal set; } = true;
    
    /// <summary>
    /// Gets or sets the file name of the stored cover image.
    /// </summary>
    [JsonInclude]
    public string CoverFileName { get; internal set; }
}
