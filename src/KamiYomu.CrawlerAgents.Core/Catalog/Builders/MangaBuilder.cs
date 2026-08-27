using KamiYomu.CrawlerAgents.Core.Catalog.Definitions;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace KamiYomu.CrawlerAgents.Core.Catalog.Builders;

/// <summary>
/// Provides a fluent builder for constructing <see cref="MangaBuilder"/>. This class simplifies <see cref="Manga"/> creation
/// by chaining configuration methods and producing a fully initialized <see cref="MangaBuilder"/> via <see cref="Build"/>.
/// </summary>
public class MangaBuilder
{
    private Manga _manga = new();
    private MangaBuilder() { }
    /// <summary>
    /// Creates a new <see cref="MangaBuilder"/> instance.
    /// </summary>
    /// <returns>A new builder for constructing a <see cref="Manga"/>.</returns>
    public static MangaBuilder Create()
    {
        return Create(null);
    }
    /// <summary>
    /// Creates a new <see cref="MangaBuilder"/> instance.
    /// </summary>
    /// <param name="manga">An optional existing <see cref="Manga"/> to initialize the builder with.</param>
    /// <returns>A new builder for constructing a <see cref="Manga"/>.</returns>
    public static MangaBuilder Create(Manga manga = null)
    {
        var builder = new MangaBuilder();
        if (manga != null)
        {
            builder._manga = manga;
        }
        return builder;
    }
    /// <summary>
    /// Sets the unique identifier for the manga.
    /// </summary>
    /// <param name="id">The identifier to assign to the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithId(string id)
    {
        _manga.Id = id;
        return this;
    }
    /// <summary>
    /// Sets the title of the manga.
    /// </summary>
    /// <param name="title">The title of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithTitle(string title)
    {
        _manga.Title = HttpUtility.HtmlDecode(title);
        WithFolderName(_manga.Title);
        return this;
    }
    /// <summary>
    /// Sets the alternative titles for the manga.
    /// </summary>
    /// <param name="alternativeTitles">A dictionary of alternative titles with their corresponding languages.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithAlternativeTitles(Dictionary<string, string> alternativeTitles)
    {
        _manga.AlternativeTitles = alternativeTitles?.Select(p => new KeyValuePair<string, string>(p.Key, HttpUtility.HtmlDecode(p.Value)))
                                                     .ToDictionary(p => p.Key, p => p.Value);

        return this;
    }
    /// <summary>
    /// Sets the description of the manga.
    /// </summary>
    /// <param name="description">The description of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithDescription(string description)
    {
        _manga.Description = HttpUtility.HtmlDecode(description);
        return this;
    }
    /// <summary>
    /// Sets the alternative descriptions for the manga.
    /// </summary>
    /// <param name="alternativeDescriptions">A dictionary of alternative descriptions with their corresponding languages.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithAlternativeDescriptions(Dictionary<string, string> alternativeDescriptions)
    {
        _manga.AlternativeDescriptions = alternativeDescriptions?.Select(p => new KeyValuePair<string, string>(p.Key, HttpUtility.HtmlDecode(p.Value)))
                                                           .ToDictionary(p => p.Key, p => p.Value);

        return this;
    }
    /// <summary>
    /// Sets the authors of the manga.
    /// </summary>
    /// <param name="authors">The authors of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithAuthors(params string[] authors)
    {
        _manga.Authors = authors.Select(HttpUtility.HtmlDecode);
        return this;
    }
    /// <summary>
    /// Sets the artists of the manga.
    /// </summary>
    /// <param name="artists">The artists of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithArtists(params string[] artists)
    {
        _manga.Artists = artists.Select(HttpUtility.HtmlDecode);
        return this;
    }
    /// <summary>
    /// Sets the tags of the manga.
    /// </summary>
    /// <param name="tags">The tags of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithTags(params string[] tags)
    {
        _manga.Tags = tags.Select(HttpUtility.HtmlDecode);
        return this;
    }
    /// <summary>
    /// Sets the cover URL of the manga.
    /// </summary>
    /// <param name="coverUrl">The cover URL of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithCoverUrl(Uri coverUrl)
    {
        _manga.CoverUrl = coverUrl;
        return this;
    }
    /// <summary>
    /// Sets the links of the manga.
    /// </summary>
    /// <param name="links">A dictionary of links with their corresponding descriptions.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithLinks(Dictionary<string, string> links)
    {
        _manga.Links = links;
        return this;
    }
    /// <summary>
    /// Sets the year of the manga.
    /// </summary>
    /// <param name="year">The year of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithYear(int? year)
    {
        _manga.Year = year;
        return this;
    }
    /// <summary>
    /// Sets the original language of the manga.
    /// </summary>
    /// <param name="language">The original language of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithOriginalLanguage(string language)
    {
        _manga.OriginalLanguage = language;
        return this;
    }
    /// <summary>
    /// Sets the release status of the manga.
    /// </summary>
    /// <param name="releaseStatus">The release status of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithReleaseStatus(ReleaseStatus releaseStatus)
    {
        _manga.ReleaseStatus = releaseStatus;
        return this;
    }
    /// <summary>
    /// Sets the folder name of the manga.
    /// </summary>
    /// <param name="folderName">The folder name of the manga.</param>
    /// <returns>The current builder instance.</returns>
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
    /// <summary>
    /// Sets the last volume available of the manga.
    /// </summary>
    /// <param name="volume">The last volume available of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithLastVolumeAvailable(decimal volume)
    {
        _manga.LastVolumeAvailable = volume;
        return this;
    }
    /// <summary>
    /// Sets the latest chapter available of the manga.
    /// </summary>
    /// <param name="chapter">The latest chapter available of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithLatestChapterAvailable(decimal chapter)
    {
        _manga.LatestChapterAvailable = chapter;
        return this;
    }
    /// <summary>
    /// Sets the website URL of the manga.
    /// </summary>
    /// <param name="url">The website URL of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithWebsiteUrl(string url)
    {
        _manga.WebSiteUrl = url;
        return this;
    }
    /// <summary>
    /// Sets whether the manga is family safe.
    /// </summary>
    /// <param name="isFamilySafe">Indicates if the manga is family safe.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithIsFamilySafe(bool isFamilySafe)
    {
        _manga.IsFamilySafe = isFamilySafe;
        return this;
    }
    /// <summary>
    /// Sets the cover file name of the manga.
    /// </summary>
    /// <param name="fileName">The cover file name of the manga.</param>
    /// <returns>The current builder instance.</returns>
    public MangaBuilder WithCoverFileName(string fileName)
    {
        _manga.CoverFileName = fileName;
        return this;
    }
    /// <summary>
    /// Builds and returns the manga instance.
    /// </summary>
    /// <returns>The constructed manga instance.</returns>
    public Manga Build()
    {
        return _manga;
    }
}
