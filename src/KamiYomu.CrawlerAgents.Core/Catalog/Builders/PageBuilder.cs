namespace KamiYomu.CrawlerAgents.Core.Catalog.Builders;

/// <summary>
/// Provides a fluent builder for constructing <see cref="PageBuilder"/>. This class simplifies <see cref="Page"/> creation
/// by chaining configuration methods and producing a fully initialized <see cref="PageBuilder"/> via <see cref="Build"/>.
/// </summary>
public class PageBuilder
{
    private Page _page = new();
    private PageBuilder() { }
    /// <summary>
    /// Creates a new <see cref="PageBuilder"/> instance.
    /// </summary>
    /// <param name="page">An optional existing <see cref="Page"/> to initialize the builder with.</param>
    /// <returns>A new builder for constructing a <see cref="Page"/>.</returns>
    public static PageBuilder Create(Page page = null)
    {
        var builder = new PageBuilder();
        if (page != null)
        {
            builder._page = page;
        }
        return builder;
    }

    public PageBuilder WithId(string id)
    {
        _page.Id = id;
        return this;
    }

    public PageBuilder WithChapterId(string chapterId)
    {
        _page.ChapterId = chapterId;
        return this;
    }

    public PageBuilder WithPageNumber(decimal pageNumber)
    {
        _page.PageNumber = pageNumber;
        return this;
    }

    public PageBuilder WithImageUrl(Uri imageUrl)
    {
        _page.ImageUrl = imageUrl;
        return this;
    }

    public PageBuilder WithParentChapter(Chapter chapter)
    {
        _page.ParentChapter = chapter;
        return this;
    }

    public Page Build()
    {
        return _page;
    }
}
