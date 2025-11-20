using KamiYomu.CrawlerAgents.Core.Inputs;

namespace KamiYomu.CrawlerAgents.Core.Tests.Inputs;

public class AbstractInputAttributeTests
{
    public class TestInputAttribute : AbstractInputAttribute
    {
        public TestInputAttribute(string name, string legend)
            : base(name, legend) { }

        public TestInputAttribute(string name, string legend, bool required, string defaultValue)
            : base(name, legend, required, defaultValue) { }

        public TestInputAttribute(string name, string legend, bool required, string defaultValue, short order)
            : base(name, legend, required, defaultValue, order) { }
    }


    [Fact]
    public void Constructor_WithNameAndLegend_SetsDefaults()
    {
        var attr = new TestInputAttribute("Username", "Enter your username");

        Assert.Equal("Username", attr.Name);
        Assert.Equal("Enter your username", attr.Legend);
        Assert.Equal(string.Empty, attr.DefaultValue);
        Assert.Equal(0, attr.Order);
        Assert.False(attr.Required);
    }

    [Fact]
    public void Constructor_WithRequiredAndDefaultValue_SetsProperties()
    {
        var attr = new TestInputAttribute("Password", "Enter your password", true, "secret");

        Assert.Equal("Password", attr.Name);
        Assert.Equal("Enter your password", attr.Legend);
        Assert.Equal("secret", attr.DefaultValue);
        Assert.True(attr.Required);
        Assert.Equal(0, attr.Order);
    }

    [Fact]
    public void Constructor_WithOrder_SetsOrderProperty()
    {
        var attr = new TestInputAttribute("Email", "Enter your email", true, "default@example.com", 5);

        Assert.Equal("Email", attr.Name);
        Assert.Equal("Enter your email", attr.Legend);
        Assert.Equal("default@example.com", attr.DefaultValue);
        Assert.True(attr.Required);
        Assert.Equal(5, attr.Order);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidName_ThrowsArgumentExceptionOrArgumentNullException(string invalidName)
    {
        // Act
        var ex = Record.Exception(() => new TestInputAttribute(invalidName, "Legend"));

        // Assert
        Assert.NotNull(ex);
        Assert.True(
            ex is ArgumentException || ex is ArgumentNullException,
            $"Expected ArgumentException or ArgumentNullException, but got {ex.GetType().Name}"
        );
    }
}