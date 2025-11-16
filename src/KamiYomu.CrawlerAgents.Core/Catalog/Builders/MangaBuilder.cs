using KamiYomu.CrawlerAgents.Core.Catalog.Definitions;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace KamiYomu.CrawlerAgents.Core.Catalog.Builders
{
    /// <summary>
    /// Provides a fluent builder for constructing <see cref="MangaBuilder"/>. This class simplifies <see cref="Manga"/> creation
    /// by chaining configuration methods and producing a fully initialized <see cref="MangaBuilder"/> via <see cref="Build"/>.
    /// </summary>
    public class MangaBuilder
    {
        private readonly Manga _manga = new();
        private MangaBuilder() { }
        public static MangaBuilder Create()
        {
            return new MangaBuilder();
        }
        public MangaBuilder WithId(string id)
        {
            _manga.Id = id;
            return this;
        }

        public MangaBuilder WithTitle(string title)
        {
            _manga.Title = HttpUtility.HtmlDecode(title);
            WithFolderName(_manga.Title);
            return this;
        }

        public MangaBuilder WithAlternativeTitles(Dictionary<string, string> alternativeTitles)
        {
            _manga.AlternativeTitles = alternativeTitles?.Select(p => new KeyValuePair<string, string>(p.Key, HttpUtility.HtmlDecode(p.Value)))
                                                         .ToDictionary(p => p.Key, p => p.Value);

            return this;
        }

        public MangaBuilder WithDescription(string description)
        {
            _manga.Description = HttpUtility.HtmlDecode(description);
            return this;
        }
        public MangaBuilder WithAlternativeDescriptions(Dictionary<string, string> alternativeTitles)
        {
            _manga.AlternativeDescriptions = alternativeTitles?.Select(p => new KeyValuePair<string, string>(p.Key, HttpUtility.HtmlDecode(p.Value)))
                                                               .ToDictionary(p => p.Key, p => p.Value);

            return this;
        }

        public MangaBuilder WithAuthors(params string[] authors)
        {
            _manga.Authors = authors.Select(HttpUtility.HtmlDecode);
            return this;
        }

        public MangaBuilder WithArtists(params string[] artists)
        {
            _manga.Artists = artists.Select(HttpUtility.HtmlDecode);
            return this;
        }

        public MangaBuilder WithTags(params string[] tags)
        {
            _manga.Tags = tags.Select(HttpUtility.HtmlDecode);
            return this;
        }

        public MangaBuilder WithCoverUrl(Uri coverUrl)
        {
            _manga.CoverUrl = coverUrl;
            return this;
        }

        public MangaBuilder WithLinks(Dictionary<string, string> links)
        {
            _manga.Links = links;
            return this;
        }

        public MangaBuilder WithYear(int? year)
        {
            _manga.Year = year;
            return this;
        }

        public MangaBuilder WithOriginalLanguage(string language)
        {
            _manga.OriginalLanguage = language;
            return this;
        }

        public MangaBuilder WithReleaseStatus(ReleaseStatus releaseStatus)
        {
            _manga.ReleaseStatus = releaseStatus;
            return this;
        }

        public MangaBuilder WithFolderName(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
            {
                _manga.FolderName = string.Empty;
                return this;
            }

            var decoded = WebUtility.HtmlDecode(folderName);
            var matches = ValidationRules.DisplayNamePattern.Matches(decoded);

            var builder = new StringBuilder();
            foreach (Match match in matches)
            {
                builder.Append(match.Value);
            }

            _manga.FolderName = builder.ToString().TrimEnd('.');

            return this;
        }

        public MangaBuilder WithLastVolumeAvailable(decimal volume)
        {
            _manga.LastVolumeAvailable = volume;
            return this;
        }

        public MangaBuilder WithLatestChapterAvailable(decimal chapter)
        {
            _manga.LatestChapterAvailable = chapter;
            return this;
        }

        public MangaBuilder WithWebsiteUrl(string url)
        {
            _manga.WebSiteUrl = url;
            return this;
        }

        public MangaBuilder WithIsFamilySafe(bool isFamilySafe)
        {
            _manga.IsFamilySafe = isFamilySafe;
            return this;
        }

        public MangaBuilder WithCoverFileName(string fileName)
        {
            _manga.CoverFileName = fileName;
            return this;
        }

        public Manga Build()
        {
            return _manga;
        }
    }
}
