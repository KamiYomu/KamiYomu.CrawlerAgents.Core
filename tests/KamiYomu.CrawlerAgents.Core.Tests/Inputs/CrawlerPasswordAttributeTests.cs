using KamiYomu.CrawlerAgents.Core.Inputs;

namespace KamiYomu.CrawlerAgents.Core.Tests.Inputs
{
    public class CrawlerPasswordAttributeTests
    {
        [Fact]
        public void Constructor_WithNameAndLegend_SetsDefaults()
        {
            var attr = new CrawlerPasswordAttribute("Password", "Enter your password");

            Assert.Equal("Password", attr.Name);
            Assert.Equal("Enter your password", attr.Legend);
            Assert.Equal(string.Empty, attr.DefaultValue);
            Assert.Equal(0, attr.Order);
            Assert.False(attr.Required);
        }

        [Fact]
        public void Constructor_WithRequiredAndDefaultValue_SetsProperties()
        {
            var attr = new CrawlerPasswordAttribute("Password", "Enter your password", true, "secret");

            Assert.Equal("Password", attr.Name);
            Assert.Equal("Enter your password", attr.Legend);
            Assert.Equal("secret", attr.DefaultValue);
            Assert.True(attr.Required);
            Assert.Equal(0, attr.Order);
        }

        [Fact]
        public void Constructor_WithOrder_SetsOrderProperty()
        {
            var attr = new CrawlerPasswordAttribute("Password", "Enter your password", true, "secret", 3);

            Assert.Equal("Password", attr.Name);
            Assert.Equal("Enter your password", attr.Legend);
            Assert.Equal("secret", attr.DefaultValue);
            Assert.True(attr.Required);
            Assert.Equal(3, attr.Order);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidName_ThrowsArgumentException(string invalidName)
        {
            Assert.ThrowsAny<ArgumentException>(() => new CrawlerPasswordAttribute(invalidName, "Legend"));
        }
    }

}
