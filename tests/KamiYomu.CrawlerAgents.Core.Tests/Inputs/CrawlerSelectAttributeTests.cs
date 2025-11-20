using KamiYomu.CrawlerAgents.Core.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiYomu.CrawlerAgents.Core.Tests.Inputs
{
    public class CrawlerSelectAttributeTests
    {
        [Fact]
        public void Constructor_WithNameLegendAndOptions_SetsProperties()
        {
            var options = new[] { "en", "pt", "fr" };
            var attr = new CrawlerSelectAttribute("Language", "Select language", options);

            Assert.Equal("Language", attr.Name);
            Assert.Equal("Select language", attr.Legend);
            Assert.Equal(options, attr.Options);
            Assert.Equal(string.Empty, attr.DefaultValue);
            Assert.Equal(0, attr.Order);
            Assert.False(attr.Required);
        }

        [Fact]
        public void Constructor_WithRequired_SetsRequiredProperty()
        {
            var options = new[] { "en", "pt" };
            var attr = new CrawlerSelectAttribute("Language", "Select language", true, options);

            Assert.Equal("Language", attr.Name);
            Assert.Equal("Select language", attr.Legend);
            Assert.Equal(options, attr.Options);
            Assert.True(attr.Required);
            Assert.Equal(0, attr.Order);
        }

        [Fact]
        public void Constructor_WithOrder_SetsOrderProperty()
        {
            var options = new[] { "low", "medium", "high" };
            var attr = new CrawlerSelectAttribute("Priority", "Select priority", true, 5, options);

            Assert.Equal("Priority", attr.Name);
            Assert.Equal("Select priority", attr.Legend);
            Assert.Equal(options, attr.Options);
            Assert.True(attr.Required);
            Assert.Equal(5, attr.Order);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidName_ThrowsArgumentException(string invalidName)
        {
            var options = new[] { "en" };
            Assert.ThrowsAny<ArgumentException>(() => new CrawlerSelectAttribute(invalidName, "Legend", options));
        }

        [Theory]
        [MemberData(nameof(InvalidOptionsData))]
        public void Constructor_InvalidOptions_ThrowsArgumentException(string[] invalidOptions)
        {
            Assert.Throws<ArgumentException>(() => new CrawlerSelectAttribute("Language", "Legend", invalidOptions));
        }

        public static IEnumerable<object[]> InvalidOptionsData =>
            new List<object[]>
            {
                new object[] { null },
                new object[] { Array.Empty<string>() }
            };

        [Fact]
        public void ToString_ReturnsExpectedFormat()
        {
            var options = new[] { "opt1", "opt2" };
            var attr = new CrawlerSelectAttribute("Select", "Select something", true, 2, options);

            var result = attr.ToString();

            Assert.Contains("Order=2", result);
            Assert.Contains("Name=\"Select\"", result);
            Assert.Contains("Legend=\"Select something\"", result);
            Assert.Contains("Options=[opt1, opt2]", result);
            Assert.Contains("Required=True", result);
            Assert.Contains("Type=\"CrawlerSelectAttribute\"", result);
        }
    }

}
