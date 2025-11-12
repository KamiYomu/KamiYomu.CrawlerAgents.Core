namespace KamiYomu.CrawlerAgents.Core.Catalog.Builders
{
    /// <summary>
    /// Provides a fluent builder for constructing <see cref="PageBuilder"/>. This class simplifies <see cref="Page"/> creation
    /// by chaining configuration methods and producing a fully initialized <see cref="PageBuilder"/> via <see cref="Build"/>.
    /// </summary>
    public class PageBuilder
    {
        private readonly Page _page = new();
        private PageBuilder() { }
        public static PageBuilder Create()
        {
            return new PageBuilder();
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
}
