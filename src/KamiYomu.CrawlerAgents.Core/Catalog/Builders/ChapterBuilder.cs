using System.Net;

namespace KamiYomu.CrawlerAgents.Core.Catalog.Builders
{
    /// <summary>
    /// Provides a fluent builder for constructing <see cref="ChapterBuilder"/>. This class simplifies <see cref="Chapter"/> creation
    /// by chaining configuration methods and producing a fully initialized <see cref="ChapterBuilder"/> via <see cref="Build"/>.
    /// </summary>
    public class ChapterBuilder
    {
        private readonly Chapter _chapter = new();
        private ChapterBuilder() { }
        public static ChapterBuilder Create()
        {
            return new ChapterBuilder();
        }

        public ChapterBuilder WithId(string id)
        {
            _chapter.Id = id;
            return this;
        }

        public ChapterBuilder WithParentManga(Manga manga)
        {
            _chapter.ParentManga = manga;
            return this;
        }

        public ChapterBuilder WithVolume(int volume)
        {
            _chapter.Volume = volume;
            return this;
        }

        public ChapterBuilder WithNumber(int number)
        {
            _chapter.Number = number;
            return this;
        }

        public ChapterBuilder WithTitle(string title)
        {
            _chapter.Title = WebUtility.HtmlDecode(title);
           
            return this;
        }

        public ChapterBuilder WithTranslatedLanguage(string language)
        {
            _chapter.TranslatedLanguage = language;
            return this;
        }

        public ChapterBuilder WithPages(int pages)
        {
            _chapter.Pages = pages;
            return this;
        }

        public ChapterBuilder WithUri(Uri uri)
        {
            _chapter.Uri = uri;
            return this;
        }

        public ChapterBuilder WithReleaseDate(DateTime releaseDate)
        {
            _chapter.ReleaseDate = releaseDate;
            return this;
        }

        public Chapter Build()
        {
            return _chapter;
        }
    }
}
