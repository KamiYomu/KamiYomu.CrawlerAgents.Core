using System.Text.Json.Serialization;

namespace KamiYomu.CrawlerAgents.Core.Catalog
{
    public class Page
    {
        public Page() { } 

        [JsonInclude]
        public string Id { get; internal set; }

        [JsonInclude]
        public string ChapterId { get; internal set; }

        [JsonInclude]
        public int PageNumber { get; internal set; }

        [JsonInclude]
        public Uri ImageUrl { get; internal set; }

        [JsonInclude]
        public Chapter ParentChapter { get; internal set; }
    }

}
