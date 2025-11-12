using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiYomu.CrawlerAgents.Core.Tests.Builders
{
    public class PageBuilderTests
    {
        [Fact]
        public void Build_ShouldReturnFullyPopulatedPage()
        {
            var chapter = ChapterBuilder.Create().WithId("ch001").WithTitle("Chapter One").Build();
            var imageUrl = new Uri("https://example.com/page1.jpg");

            var page = PageBuilder.Create()
                .WithId("pg001")
                .WithChapterId("ch001")
                .WithPageNumber(1.0m)
                .WithImageUrl(imageUrl)
                .WithParentChapter(chapter)
                .Build();

            Assert.Equal("pg001", page.Id);
            Assert.Equal("ch001", page.ChapterId);
            Assert.Equal(1.0m, page.PageNumber);
            Assert.Equal(imageUrl, page.ImageUrl);
            Assert.Equal(chapter, page.ParentChapter);
        }

        [Fact]
        public void WithId_ShouldSetPageId()
        {
            var page = PageBuilder.Create()
                .WithId("pg123")
                .Build();

            Assert.Equal("pg123", page.Id);
        }

        [Fact]
        public void WithChapterId_ShouldSetChapterId()
        {
            var page = PageBuilder.Create()
                .WithChapterId("ch456")
                .Build();

            Assert.Equal("ch456", page.ChapterId);
        }

        [Fact]
        public void WithPageNumber_ShouldSetPageNumber()
        {
            var page = PageBuilder.Create()
                .WithPageNumber(5.5m)
                .Build();

            Assert.Equal(5.5m, page.PageNumber);
        }

        [Fact]
        public void WithImageUrl_ShouldSetImageUrl()
        {
            var uri = new Uri("https://example.com/image.jpg");
            var page = PageBuilder.Create()
                .WithImageUrl(uri)
                .Build();

            Assert.Equal(uri, page.ImageUrl);
        }

        [Fact]
        public void WithParentChapter_ShouldSetParentChapter()
        {
            var chapter = ChapterBuilder.Create().WithId("ch789").Build();
            var page = PageBuilder.Create()
                .WithParentChapter(chapter)
                .Build();

            Assert.Equal(chapter, page.ParentChapter);
        }

        [Fact]
        public void WithId_ShouldAcceptNull()
        {
            var page = PageBuilder.Create()
                .WithId(null)
                .Build();

            Assert.Null(page.Id);
        }

        [Fact]
        public void WithChapterId_ShouldAcceptEmptyString()
        {
            var page = PageBuilder.Create()
                .WithChapterId("")
                .Build();

            Assert.Equal("", page.ChapterId);
        }

        [Fact]
        public void WithImageUrl_ShouldAcceptNull()
        {
            var page = PageBuilder.Create()
                .WithImageUrl(null)
                .Build();

            Assert.Null(page.ImageUrl);
        }

        [Fact]
        public void WithParentChapter_ShouldAcceptNull()
        {
            var page = PageBuilder.Create()
                .WithParentChapter(null)
                .Build();

            Assert.Null(page.ParentChapter);
        }
    }
}
