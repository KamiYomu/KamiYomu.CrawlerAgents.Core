using KamiYomu.CrawlerAgents.Core.Inputs;

namespace KamiYomu.CrawlerAgents.Core.Tests.Inputs;

public class CrawlerCheckBoxAttributeTests
{
    [Fact]
    public void Constructor_WithNameLegendAndOptions_SetsProperties()
    {
        var options = new[] { "general", "safe", "14" };
        var attr = new CrawlerCheckBoxAttribute("contentRating", "Content Rating", options);

        Assert.Equal("contentRating", attr.Name);
        Assert.Equal("Content Rating", attr.Legend);
        Assert.Equal(options, attr.Options);
        Assert.Equal(string.Empty, attr.DefaultValue);
        Assert.Equal(0, attr.Order);
        Assert.False(attr.Required);
    }

    [Fact]
    public void Constructor_WithRequiredAndDefaultValue_SetsProperties()
    {
        var options = new[] { "en", "pt", "fr" };
        var attr = new CrawlerCheckBoxAttribute("language", "Language", true, "en", options);

        Assert.Equal("language", attr.Name);
        Assert.Equal("Language", attr.Legend);
        Assert.Equal(options, attr.Options);
        Assert.Equal("en", attr.DefaultValue);
        Assert.True(attr.Required);
        Assert.Equal(0, attr.Order);
    }

    [Fact]
    public void Constructor_WithOrder_SetsOrderProperty()
    {
        var options = new[] { "low", "medium", "high" };
        var attr = new CrawlerCheckBoxAttribute("priority", "Priority", true, "medium", 5, options);

        Assert.Equal("priority", attr.Name);
        Assert.Equal("Priority", attr.Legend);
        Assert.Equal(options, attr.Options);
        Assert.Equal("medium", attr.DefaultValue);
        Assert.True(attr.Required);
        Assert.Equal(5, attr.Order);
    }

    public static IEnumerable<object[]> InvalidOptionsData =>
    [
        [null],
        [Array.Empty<string>()]
    ];

    [Theory]
    [MemberData(nameof(InvalidOptionsData))]
    public void Constructor_InvalidOptions_ThrowsArgumentException(string[] invalidOptions)
    {
        Assert.Throws<ArgumentException>(() => new CrawlerCheckBoxAttribute("test", "Legend", invalidOptions));
    }


    [Fact]
    public void ToString_ReturnsExpectedFormat()
    {
        var options = new[] { "opt1", "opt2" };
        var attr = new CrawlerCheckBoxAttribute("checkbox", "Checkbox Legend", true, "opt1", 2, options);

        var result = attr.ToString();

        Assert.Contains("Order=2", result);
        Assert.Contains("Name=\"checkbox\"", result);
        Assert.Contains("Legend=\"Checkbox Legend\"", result);
        Assert.Contains("Options=[opt1, opt2]", result);
        Assert.Contains("Required=True", result);
        Assert.Contains("Type=\"CrawlerCheckBoxAttribute\"", result);
    }
}
