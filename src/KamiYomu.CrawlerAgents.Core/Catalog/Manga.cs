using KamiYomu.CrawlerAgents.Core.Catalog.Definitions;

namespace KamiYomu.CrawlerAgents.Core.Catalog;

public class Manga
{
    public Manga() { }

    [JsonInclude]
    public string Id { get; internal set; }
    
    [JsonInclude]
    public string Title { get; internal set; }
    
    [JsonInclude]
    public string Description { get; internal set; }
    
    [JsonInclude]
    public IEnumerable<string> Tags { get; internal set; }
    
    [JsonInclude]
    public Uri CoverUrl { get; internal set; }
    
    [JsonInclude]
    public Dictionary<string, string> Links { get; internal set; }
    
    [JsonInclude]
    public int? Year { get; internal set; }
    
    [JsonInclude]
    public string OriginalLanguage { get; internal set; }

    [JsonInclude]
    public ReleaseStatus ReleaseStatus { get; internal set; }
    
    [JsonInclude]
    public string FolderName { get; internal set; }
    
    [JsonInclude]
    public decimal LastVolumeAvailable { get; internal set; }
    
    [JsonInclude]
    public decimal LatestChapterAvailable { get; internal set; }
    
    [JsonInclude]
    public string WebsiteUrl { get; internal set; }
    
    [JsonInclude]
    public string ContentRating { get; internal set; }
    
    [JsonInclude]
    public string CoverFileName { get; internal set; }
}
