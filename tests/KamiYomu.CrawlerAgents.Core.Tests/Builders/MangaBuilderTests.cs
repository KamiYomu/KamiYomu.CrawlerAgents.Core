namespace KamiYomu.CrawlerAgents.Core.Tests.Builders;

public class MangaBuilderTests
{
    [Fact]
    public void WithTitle_ShouldDecodeHtmlAndSetFolderName()
    {
        var manga = MangaBuilder.Create()
            .WithTitle("My &amp; Manga")
            .Build();

        Assert.Equal("My & Manga", manga.Title);
        Assert.Equal("My  Manga", manga.FolderName); 
    }

    [Fact]
    public void WithAlternativeTitles_ShouldHandleNull()
    {
        var manga = MangaBuilder.Create()
            .WithAlternativeTitles(null)
            .Build();

        Assert.Null(manga.AlternativeTitles);
    }

    [Fact]
    public void WithAuthors_ShouldHandleEmptyArray()
    {
        var manga = MangaBuilder.Create()
            .WithAuthors()
            .Build();

        Assert.Empty(manga.Authors);
    }

    [Fact]
    public void WithTags_ShouldHandleHtmlEncodedTags()
    {
        var manga = MangaBuilder.Create()
            .WithTags("Action", "Drama &amp; Romance")
            .Build();

        Assert.Contains("Drama & Romance", manga.Tags);
    }

    [Fact]
    public void WithFolderName_ShouldHandleNullAndWhitespace()
    {
        var manga1 = MangaBuilder.Create().WithFolderName(null).Build();
        var manga2 = MangaBuilder.Create().WithFolderName("   ").Build();

        Assert.Equal(string.Empty, manga1.FolderName);
        Assert.Equal(string.Empty, manga2.FolderName);
    }

    [Fact]
    public void WithCoverUrl_ShouldAcceptNull()
    {
        var manga = MangaBuilder.Create()
            .WithCoverUrl(null)
            .Build();

        Assert.Null(manga.CoverUrl);
    }

    [Fact]
    public void Build_ShouldReturnFullyPopulatedManga()
    {
        var altTitles = new Dictionary<string, string> { { "en", "Title &amp; One" }, { "jp", "タイトル" } };
        var altDescriptions = new Dictionary<string, string> { { "en", "Desc &lt;b&gt;bold&lt;/b&gt;" } };
        var authors = new[] { "Author &amp; A", "Author B" };
        var tags = new[] { "Action", "Drama" };
        var links = new Dictionary<string, string> { { "site", "https://example.com" } };
        var coverUri = new Uri("https://example.com/cover.jpg");

        var manga = MangaBuilder.Create()
            .WithId("m001")
            .WithTitle("My &amp; Manga")
            .WithAlternativeTitles(altTitles)
            .WithDescription("A &lt;i&gt;great&lt;/i&gt; story")
            .WithAlternativeDescriptions(altDescriptions)
            .WithAuthors(authors)
            .WithTags(tags)
            .WithCoverUrl(coverUri)
            .WithLinks(links)
            .WithYear(2023)
            .WithOriginalLanguage("ja")
            .WithReleaseStatus(ReleaseStatus.Continuing)
            .WithLastVolumeAvailable(5.5m)
            .WithLatestChapterAvailable(42.1m)
            .WithWebsiteUrl("https://manga.com")
            .WithContentRating("PG-13")
            .WithCoverFileName("cover.jpg")
            .Build();

        Assert.Equal("m001", manga.Id);
        Assert.Equal("My & Manga", manga.Title);
        Assert.Equal("My  Manga", manga.FolderName);
        Assert.Equal("Title & One", manga.AlternativeTitles["en"]);
        Assert.Equal("タイトル", manga.AlternativeTitles["jp"]);
        Assert.Equal("A <i>great</i> story", manga.Description);
        Assert.Equal("Desc <b>bold</b>", manga.AlternativeDescriptions["en"]);
        Assert.Contains("Author & A", manga.Authors);
        Assert.Contains("Author B", manga.Authors);
        Assert.Contains("Action", manga.Tags);
        Assert.Equal(coverUri, manga.CoverUrl);
        Assert.Equal(links, manga.Links);
        Assert.Equal(2023, manga.Year);
        Assert.Equal("ja", manga.OriginalLanguage);
        Assert.Equal(ReleaseStatus.Continuing, manga.ReleaseStatus);
        Assert.Equal(5.5m, manga.LastVolumeAvailable);
        Assert.Equal(42.1m, manga.LatestChapterAvailable);
        Assert.Equal("https://manga.com", manga.WebSiteUrl);
        Assert.Equal("PG-13", manga.ContentRating);
        Assert.Equal("cover.jpg", manga.CoverFileName);
    }
}
