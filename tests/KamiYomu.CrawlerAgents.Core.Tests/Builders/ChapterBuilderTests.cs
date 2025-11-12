namespace KamiYomu.CrawlerAgents.Core.Tests.Builders;

public class ChapterBuilderTests
{
    [Fact]
    public void Create_ShouldReturnNewInstance()
    {
        var builder = ChapterBuilder.Create();
        Assert.NotNull(builder);
    }

    [Fact]
    public void WithId_ShouldSetChapterId()
    {
        var chapter = ChapterBuilder.Create()
            .WithId("ch001")
            .Build();

        Assert.Equal("ch001", chapter.Id);
    }

    [Fact]
    public void WithParentManga_ShouldSetParentManga()
    {
        var manga = new Manga();
        var chapter = ChapterBuilder.Create()
            .WithParentManga(manga)
            .Build();

        Assert.Equal(manga, chapter.ParentManga);
    }

    [Fact]
    public void WithVolume_ShouldSetVolume()
    {
        var chapter = ChapterBuilder.Create()
            .WithVolume(3.5m)
            .Build();

        Assert.Equal(3.5m, chapter.Volume);
    }

    [Fact]
    public void WithNumber_ShouldSetNumber()
    {
        var chapter = ChapterBuilder.Create()
            .WithNumber(12.1m)
            .Build();

        Assert.Equal(12.1m, chapter.Number);
    }

    [Fact]
    public void WithTitle_ShouldDecodeHtmlAndSetTitle()
    {
        var encodedTitle = "Chapter &amp; One";
        var chapter = ChapterBuilder.Create()
            .WithTitle(encodedTitle)
            .Build();

        Assert.Equal(WebUtility.HtmlDecode(encodedTitle), chapter.Title);
    }

    [Fact]
    public void WithDescription_ShouldDecodeHtmlAndSetDescription()
    {
        var encodedDescription = "A &lt;b&gt;bold&lt;/b&gt; move";
        var chapter = ChapterBuilder.Create()
            .WithDescription(encodedDescription)
            .Build();

        Assert.Equal(WebUtility.HtmlDecode(encodedDescription), chapter.Description);
    }

    [Fact]
    public void WithTranslatedLanguage_ShouldSetLanguage()
    {
        var chapter = ChapterBuilder.Create()
            .WithTranslatedLanguage("en")
            .Build();

        Assert.Equal("en", chapter.TranslatedLanguage);
    }

    [Fact]
    public void WithPages_ShouldSetPageCount()
    {
        var chapter = ChapterBuilder.Create()
            .WithPages(42)
            .Build();

        Assert.Equal(42, chapter.Pages);
    }

    [Fact]
    public void WithUri_ShouldSetUri()
    {
        var uri = new Uri("https://example.com/chapter");
        var chapter = ChapterBuilder.Create()
            .WithUri(uri)
            .Build();

        Assert.Equal(uri, chapter.Uri);
    }

    [Fact]
    public void WithReleaseDate_ShouldSetReleaseDate()
    {
        var date = new DateTime(2023, 10, 1);
        var chapter = ChapterBuilder.Create()
            .WithReleaseDate(date)
            .Build();

        Assert.Equal(date, chapter.ReleaseDate);
    }

    [Fact]
    public void WithTitle_ShouldHandleNullTitle()
    {
        var chapter = ChapterBuilder.Create()
            .WithTitle(null)
            .Build();

        Assert.Null(chapter.Title);
    }

    [Fact]
    public void WithDescription_ShouldHandleEmptyString()
    {
        var chapter = ChapterBuilder.Create()
            .WithDescription("")
            .Build();

        Assert.Equal("", chapter.Description);
    }

    [Fact]
    public void WithTranslatedLanguage_ShouldAcceptEmptyLanguageCode()
    {
        var chapter = ChapterBuilder.Create()
            .WithTranslatedLanguage("")
            .Build();

        Assert.Equal("", chapter.TranslatedLanguage);
    }

    [Fact]
    public void WithUri_ShouldHandleNullUri()
    {
        var chapter = ChapterBuilder.Create()
            .WithUri(null)
            .Build();

        Assert.Null(chapter.Uri);
    }

    [Fact]
    public void WithReleaseDate_ShouldAcceptMinValue()
    {
        var chapter = ChapterBuilder.Create()
            .WithReleaseDate(DateTime.MinValue)
            .Build();

        Assert.Equal(DateTime.MinValue, chapter.ReleaseDate);
    }

    [Fact]
    public void Build_ShouldReturnFullyPopulatedChapter()
    {
        var manga = MangaBuilder.Create().WithTitle("Sample Manga").Build();
        var uri = new Uri("https://example.com/chapter/1");
        var releaseDate = new DateTime(2023, 11, 11);

        var chapter = ChapterBuilder.Create()
            .WithId("ch001")
            .WithParentManga(manga)
            .WithVolume(1.5m)
            .WithNumber(3.2m)
            .WithTitle("Chapter &amp; One")
            .WithDescription("A &lt;b&gt;bold&lt;/b&gt; move")
            .WithTranslatedLanguage("en")
            .WithPages(25)
            .WithUri(uri)
            .WithReleaseDate(releaseDate)
            .Build();

        Assert.Equal("ch001", chapter.Id);
        Assert.Equal(manga, chapter.ParentManga);
        Assert.Equal(1.5m, chapter.Volume);
        Assert.Equal(3.2m, chapter.Number);
        Assert.Equal("Chapter & One", chapter.Title);
        Assert.Equal("A <b>bold</b> move", chapter.Description);
        Assert.Equal("en", chapter.TranslatedLanguage);
        Assert.Equal(25, chapter.Pages);
        Assert.Equal(uri, chapter.Uri);
        Assert.Equal(releaseDate, chapter.ReleaseDate);
    }
}
