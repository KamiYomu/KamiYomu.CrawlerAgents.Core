namespace KamiYomu.CrawlerAgents.Core.Catalog;

public class Chapter
{
    public Chapter() { }

    [JsonInclude]
    public string Id { get; internal set; }
    
    [JsonInclude]
    public decimal Volume { get; internal set; }
    
    [JsonInclude]
    public decimal Number { get; internal set; }
    
    [JsonInclude]
    public string Title { get; internal set; }

    [JsonInclude]
    public string Description { get; internal set; }

    [JsonInclude]
    public string TranslatedLanguage { get; internal set; }
    
    [JsonInclude]
    public decimal Pages { get; internal set; }
    
    [JsonInclude]
    public Uri Uri { get; internal set; }
    
    [JsonInclude]
    public DateTime ReleaseDate { get; internal set; }
    
    [JsonInclude]
    public Manga ParentManga { get; internal set; }
}
