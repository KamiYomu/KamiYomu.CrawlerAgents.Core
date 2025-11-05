namespace KamiYomu.CrawlerAgents.Core.Catalog;

public class Chapter
{
    public Chapter() { }

    [JsonInclude]
    public string Id { get; internal set; }
    
    [JsonInclude]
    public int Volume { get; internal set; }
    
    [JsonInclude]
    public int Number { get; internal set; }
    
    [JsonInclude]
    public string Title { get; internal set; }
    
    [JsonInclude]
    public string TranslatedLanguage { get; internal set; }
    
    [JsonInclude]
    public int Pages { get; internal set; }
    
    [JsonInclude]
    public Uri Uri { get; internal set; }
    
    [JsonInclude]
    public DateTime ReleaseDate { get; internal set; }
    
    [JsonInclude]
    public Manga ParentManga { get; internal set; }
}
