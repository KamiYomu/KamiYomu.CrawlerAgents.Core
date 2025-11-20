namespace KamiYomu.CrawlerAgents.Core.Tests;

public class CrawlerAgentSettingsTests
{
    [Fact]
    public void HttpUserAgent_ShouldContainPlatformAndArchitecture()
    {
        var ua = CrawlerAgentSettings.HttpUserAgent;

        Assert.StartsWith("KamiYomu-Agent/1.0", ua);
        Assert.Contains(Environment.OSVersion.Platform.ToString(), ua);
        Assert.Contains(Environment.Is64BitOperatingSystem ? "x64" : "x86", ua);
    }

    [Fact]
    public void TimeoutMilliseconds_ShouldBe60000()
    {
        Assert.Equal(60000, CrawlerAgentSettings.TimeoutMilliseconds);
    }

    [Theory]
    [InlineData("Mozilla/5.0 (Windows NT 10.0; Win64; x64)")]
    [InlineData("CustomAgent/1.0 (Linux; x86)")]
    public void IsLikelyUserAgent_ValidString_ReturnsTrue(string ua)
    {
        var result = CrawlerAgentSettings.IsLikelyUserAgent(ua);
        Assert.True(result);
    }

    public static IEnumerable<object[]> InvalidUserAgentData =>
    new List<object[]>
    {
                new object[] { null },
                new object[] { "" },
                new object[] { "   " },    
                new object[] { "\t\n\r" },
                new object[] { "A" + new string('B', 600) } 
    };

    [Theory]
    [MemberData(nameof(InvalidUserAgentData))]
    public void IsLikelyUserAgent_InvalidString_ReturnsFalse(string ua)
    {
        var result = CrawlerAgentSettings.IsLikelyUserAgent(ua);
        Assert.False(result);
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void IsLikelyUserAgent_NullOrWhitespace_ReturnsFalse(string ua)
    {
        var result = CrawlerAgentSettings.IsLikelyUserAgent(ua);
        Assert.False(result);
    }

    [Fact]
    public void DefaultInputs_ShouldContainExpectedConstants()
    {
        Assert.Equal("BrowserUserAgent", CrawlerAgentSettings.DefaultInputs.BrowserUserAgent);
        Assert.Equal("HttpClientTimeout", CrawlerAgentSettings.DefaultInputs.HttpClientTimeout);
        Assert.Equal("KamiYomuILogger", CrawlerAgentSettings.DefaultInputs.KamiYomuILogger);
    }

    [Theory]
    [InlineData("BrowserUserAgent")]
    [InlineData("HttpClientTimeout")]
    [InlineData("KamiYomuILogger")]
    [InlineData("browseruseragent")] 
    public void IsDefaultInput_ValidNames_ReturnsTrue(string inputName)
    {
        Assert.True(CrawlerAgentSettings.DefaultInputs.IsDefaultInput(inputName));
    }

    [Theory]
    [InlineData("NotExisting")]
    [InlineData("AnotherInput")]
    public void IsDefaultInput_InvalidNames_ReturnsFalse(string inputName)
    {
        Assert.False(CrawlerAgentSettings.DefaultInputs.IsDefaultInput(inputName));
    }

}