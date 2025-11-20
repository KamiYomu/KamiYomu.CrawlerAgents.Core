using KamiYomu.CrawlerAgents.Core.Inputs;

namespace KamiYomu.CrawlerAgents.Core.Tests.Inputs;

public class CrawlerTextAttributeTests
{
    [Fact]
    public void Constructor_WithNameAndLegend_SetsDefaults()
    {
        var attr = new CrawlerTextAttribute("Username", "Enter your username");

        Assert.Equal("Username", attr.Name);
        Assert.Equal("Enter your username", attr.Legend);
        Assert.Equal(string.Empty, attr.DefaultValue);
        Assert.Equal(0, attr.Order);
        Assert.False(attr.Required);
    }

    [Fact]
    public void Constructor_WithRequiredAndDefaultValue_SetsProperties()
    {
        var attr = new CrawlerTextAttribute("Email", "Enter your email", true, "default@example.com");

        Assert.Equal("Email", attr.Name);
        Assert.Equal("Enter your email", attr.Legend);
        Assert.Equal("default@example.com", attr.DefaultValue);
        Assert.True(attr.Required);
        Assert.Equal(0, attr.Order);
    }

    [Fact]
    public void Constructor_WithOrder_SetsOrderProperty()
    {
        var attr = new CrawlerTextAttribute("Password", "Enter your password", true, "secret", 5);

        Assert.Equal("Password", attr.Name);
        Assert.Equal("Enter your password", attr.Legend);
        Assert.Equal("secret", attr.DefaultValue);
        Assert.True(attr.Required);
        Assert.Equal(5, attr.Order);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidName_ThrowsArgumentException(string invalidName)
    {
        Assert.ThrowsAny<ArgumentException>(() => new CrawlerTextAttribute(invalidName, "Legend"));
    }
}
