using System.Net;

namespace KamiYomu.CrawlerAgents.Core.Catalog.Builders;

/// <summary>
/// Provides a fluent builder for constructing <see cref="ChapterBuilder"/>. This class simplifies <see cref="Chapter"/> creation
/// by chaining configuration methods and producing a fully initialized <see cref="ChapterBuilder"/> via <see cref="Build"/>.
/// </summary>
public class ChapterBuilder
{
    private Chapter _chapter = new();
    private ChapterBuilder() { }
    /// <summary>
    /// Creates a new <see cref="ChapterBuilder"/> instance.
    /// </summary>
    /// <param name="chapter">An optional existing <see cref="Chapter"/> to initialize the builder with.</param>
    /// <returns>A new builder for constructing a <see cref="Chapter"/>.</returns>
    public static ChapterBuilder Create()
    {
        return Create(null);
    }
    /// <summary>
    /// Creates a new <see cref="ChapterBuilder"/> instance.
    /// </summary>
    /// <param name="chapter">An optional existing <see cref="Chapter"/> to initialize the builder with.</param>
    /// <returns>A new builder for constructing a <see cref="Chapter"/>.</returns>
    public static ChapterBuilder Create(Chapter chapter = null)
    {
        var builder = new ChapterBuilder();
        if (chapter != null)
        {
            builder._chapter = chapter;
        }
        return builder;
    }
    /// <summary>
    /// Sets the unique identifier for the chapter.
    /// </summary>
    /// <param name="id">The identifier to assign to the chapter.</param>
    /// <returns>The current builder instance.</returns>
    public ChapterBuilder WithId(string id)
    {
        _chapter.Id = id;
        return this;
    }
    /// <summary>
    /// Sets the parent manga for the chapter.
    /// </summary>
    /// <param name="manga">The parent manga.</param>
    /// <returns>The current builder instance.</returns>
    public ChapterBuilder WithParentManga(Manga manga)
    {
        _chapter.ParentManga = manga;
        return this;
    }
    /// <summary>
    /// Sets the volume number for the chapter.
    /// </summary>
    /// <param name="volume">The volume number.</param>
    /// <returns>The current builder instance.</returns>
    public ChapterBuilder WithVolume(decimal volume)
    {
        _chapter.Volume = volume;
        return this;
    }
    /// <summary>
    /// Sets the chapter number within the volume.
    /// </summary>
    /// <param name="number">The chapter number.</param>
    /// <returns>The current builder instance.</returns>
    public ChapterBuilder WithNumber(decimal number)
    {
        _chapter.Number = number;
        return this;
    }
    /// <summary>
    /// Sets the title of the chapter.
    /// </summary>
    /// <param name="title">The title of the chapter.</param>
    /// <returns>The current builder instance.</returns>
    public ChapterBuilder WithTitle(string title)
    {
        _chapter.Title = WebUtility.HtmlDecode(title);
       
        return this;
    }
    /// <summary>
    /// Sets the description of the chapter.
    /// </summary>
    /// <param name="description">The description of the chapter.</param>
    /// <returns>The current builder instance.</returns>
    public ChapterBuilder WithDescription(string description)
    {
        _chapter.Description = WebUtility.HtmlDecode(description);
        return this;
    }
    /// <summary>
    /// Sets the translated language of the chapter.
    /// </summary>
    /// <param name="language">The translated language.</param>
    /// <returns>The current builder instance.</returns>
    public ChapterBuilder WithTranslatedLanguage(string language)
    {
        _chapter.TranslatedLanguage = language;
        return this;
    }
    /// <summary>
    /// Sets the number of pages in the chapter.
    /// </summary>
    /// <param name="pages">The number of pages.</param>
    /// <returns>The current builder instance.</returns>
    public ChapterBuilder WithPages(int pages)
    {
        _chapter.Pages = pages;
        return this;
    }
    /// <summary>
    /// Sets the URI of the chapter.
    /// </summary>
    /// <param name="uri">The URI of the chapter.</param>
    /// <returns>The current builder instance.</returns>
    public ChapterBuilder WithUri(Uri uri)
    {
        _chapter.Uri = uri;
        return this;
    }
    /// <summary>
    /// Sets the release date of the chapter.
    /// </summary>
    /// <param name="releaseDate">The release date of the chapter.</param>
    /// <returns>The current builder instance.</returns>
    public ChapterBuilder WithReleaseDate(DateTime releaseDate)
    {
        _chapter.ReleaseDate = releaseDate;
        return this;
    }
    /// <summary>
    /// Builds and returns the chapter instance.
    /// </summary>
    /// <returns>The constructed chapter.</returns>
    public Chapter Build()
    {
        return _chapter;
    }
}
