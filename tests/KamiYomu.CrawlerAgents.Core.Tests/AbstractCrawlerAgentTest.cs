using Microsoft.Extensions.Logging;
using Moq;

namespace KamiYomu.CrawlerAgents.Core.Tests;

/// <summary>
/// Concrete implementation of AbstractCrawlerAgent for testing purposes.
/// Exposes protected members to allow verification of initialization and configuration.
/// </summary>
public class TestCrawlerAgent : AbstractCrawlerAgent
{
    public TestCrawlerAgent(IDictionary<string, object> options) : base(options)
    {
    }

    // Expose protected members for testing
    public IDictionary<string, object> TestOptions => Options;
    public int TestTimeoutMilliseconds => TimeoutMilliseconds;
    public string TestHttpClientDefaultUserAgent => HttpClientDefaultUserAgent;
    public ILogger TestLogger => Logger;
    public HttpClientHandler TestDefaultHttpClientHandler => DefaultHttpClientHandler;
    public HttpClientHandler TestDefaultFlareSolverrHttpHandler => DefaultFlareSolverrHttpHandler;
    public HttpClientHandler TestDefaultChromiumHttpHandler => DefaultChromiumHttpHandler;

    // Expose protected methods for testing
    public string TestGetKamiYomuUserAgent() => GetKamiYomuUserAgent();
    public bool TestIsLikelyUserAgent(string userAgent) => IsLikelyUserAgent(userAgent);
}

public class AbstractCrawlerAgentTest
{
    [Fact]
    public void Constructor_WithNullOptions_ShouldInitializeEmptyDictionary()
    {
        // Arrange & Act
        var agent = new TestCrawlerAgent(new Dictionary<string, object>());

        // Assert
        Assert.NotNull(agent.TestOptions);
        Assert.Empty(agent.TestOptions);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithEmptyOptions_ShouldInitializeWithDefaults()
    {
        // Arrange
        var options = new Dictionary<string, object>();

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Equal(60_000, agent.TestTimeoutMilliseconds);
        Assert.NotNull(agent.TestHttpClientDefaultUserAgent);
        Assert.NotEmpty(agent.TestHttpClientDefaultUserAgent);
        Assert.NotNull(agent.TestDefaultHttpClientHandler);
        Assert.NotNull(agent.TestDefaultFlareSolverrHttpHandler);
        Assert.NotNull(agent.TestDefaultChromiumHttpHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithCustomUserAgent_ShouldUseProvidedUserAgent()
    {
        // Arrange
        const string customUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";
        var options = new Dictionary<string, object>
        {
            { "BrowserUserAgent", customUserAgent }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Equal(customUserAgent, agent.TestHttpClientDefaultUserAgent);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithInvalidUserAgent_ShouldIgnoreAndUseDefault()
    {
        // Arrange
        const string invalidUserAgent = "\x00\x01\x02"; // Invalid characters
        var options = new Dictionary<string, object>
        {
            { "BrowserUserAgent", invalidUserAgent }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.NotEqual(invalidUserAgent, agent.TestHttpClientDefaultUserAgent);
        Assert.NotEmpty(agent.TestHttpClientDefaultUserAgent);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithCustomTimeout_ShouldUseProvidedTimeout()
    {
        // Arrange
        const int customTimeout = 30_000;
        var options = new Dictionary<string, object>
        {
            { "HttpClientTimeout", customTimeout.ToString() }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Equal(customTimeout, agent.TestTimeoutMilliseconds);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithCustomLogger_ShouldUseProvidedLogger()
    {
        // Arrange
        var mockLogger = new Mock<ILogger>();
        var options = new Dictionary<string, object>
        {
            { "KamiYomuILogger", mockLogger.Object }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Same(mockLogger.Object, agent.TestLogger);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithSmartCrawlerHttpHandler_ShouldUseSmartCrawlerHandlerForDefault()
    {
        // Arrange
        var smartCrawlerHandler = new HttpClientHandler();
        var options = new Dictionary<string, object>
        {
            { "SmartCrawlerHttpHandler", smartCrawlerHandler }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Same(smartCrawlerHandler, agent.TestDefaultHttpClientHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithFlareSolverrHttpHandler_ShouldUseFlareSolverrHandler()
    {
        // Arrange
        var flareSolverrHandler = new HttpClientHandler();
        var options = new Dictionary<string, object>
        {
            { "FlareSolverrHttpHandler", flareSolverrHandler }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Same(flareSolverrHandler, agent.TestDefaultFlareSolverrHttpHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithChromiumHttpHandler_ShouldUseChromiumHandler()
    {
        // Arrange
        var chromiumHandler = new HttpClientHandler();
        var options = new Dictionary<string, object>
        {
            { "ChromiumHttpHandler", chromiumHandler }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Same(chromiumHandler, agent.TestDefaultChromiumHttpHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithFlareSolverrHttpHandler_ShouldUseFlareSolverrHandlerForDefaultWhenNoneOthersProvided()
    {
        // Arrange
        var flareSolverrHandler = new HttpClientHandler();
        var options = new Dictionary<string, object>
        {
            { "FlareSolverrHttpHandler", flareSolverrHandler }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Same(flareSolverrHandler, agent.TestDefaultHttpClientHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithChromiumHttpHandler_ShouldUseChromiumHandlerForDefaultWhenOthersNotProvided()
    {
        // Arrange
        var chromiumHandler = new HttpClientHandler();
        var options = new Dictionary<string, object>
        {
            { "ChromiumHttpHandler", chromiumHandler }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Same(chromiumHandler, agent.TestDefaultHttpClientHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithMultipleHttpHandlers_ShouldPrioritizeSmartCrawler()
    {
        // Arrange
        var smartCrawlerHandler = new HttpClientHandler();
        var flareSolverrHandler = new HttpClientHandler();
        var chromiumHandler = new HttpClientHandler();

        var options = new Dictionary<string, object>
        {
            { "SmartCrawlerHttpHandler", smartCrawlerHandler },
            { "FlareSolverrHttpHandler", flareSolverrHandler },
            { "ChromiumHttpHandler", chromiumHandler }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Same(smartCrawlerHandler, agent.TestDefaultHttpClientHandler);
        Assert.Same(flareSolverrHandler, agent.TestDefaultFlareSolverrHttpHandler);
        Assert.Same(chromiumHandler, agent.TestDefaultChromiumHttpHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithSmartCrawlerAndFlareSolverr_ShouldPrioritizeSmartCrawlerForDefault()
    {
        // Arrange
        var smartCrawlerHandler = new HttpClientHandler();
        var flareSolverrHandler = new HttpClientHandler();

        var options = new Dictionary<string, object>
        {
            { "SmartCrawlerHttpHandler", smartCrawlerHandler },
            { "FlareSolverrHttpHandler", flareSolverrHandler }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Same(smartCrawlerHandler, agent.TestDefaultHttpClientHandler);
        Assert.Same(flareSolverrHandler, agent.TestDefaultFlareSolverrHttpHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithFlareSolverrAndChromium_ShouldPrioritizeFlareSolverrForDefault()
    {
        // Arrange
        var flareSolverrHandler = new HttpClientHandler();
        var chromiumHandler = new HttpClientHandler();

        var options = new Dictionary<string, object>
        {
            { "FlareSolverrHttpHandler", flareSolverrHandler },
            { "ChromiumHttpHandler", chromiumHandler }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Same(flareSolverrHandler, agent.TestDefaultHttpClientHandler);
        Assert.Same(flareSolverrHandler, agent.TestDefaultFlareSolverrHttpHandler);
        Assert.Same(chromiumHandler, agent.TestDefaultChromiumHttpHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithNoCustomHandler_ShouldCreateDefaultHttpClientHandlers()
    {
        // Arrange
        var options = new Dictionary<string, object>();

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.NotNull(agent.TestDefaultHttpClientHandler);
        Assert.NotNull(agent.TestDefaultFlareSolverrHttpHandler);
        Assert.NotNull(agent.TestDefaultChromiumHttpHandler);
        Assert.IsType<HttpClientHandler>(agent.TestDefaultHttpClientHandler);
        Assert.IsType<HttpClientHandler>(agent.TestDefaultFlareSolverrHttpHandler);
        Assert.IsType<HttpClientHandler>(agent.TestDefaultChromiumHttpHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithOnlyFlareSolverrHandler_ShouldCreateNewDefaultAndChromiumHandlers()
    {
        // Arrange
        var flareSolverrHandler = new HttpClientHandler();
        var options = new Dictionary<string, object>
        {
            { "FlareSolverrHttpHandler", flareSolverrHandler }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        // When FlareSolverr is provided, it should be used for the Default handler
        // but a new handler should be created for Chromium
        Assert.Same(flareSolverrHandler, agent.TestDefaultHttpClientHandler);
        Assert.NotSame(flareSolverrHandler, agent.TestDefaultChromiumHttpHandler);
        Assert.IsType<HttpClientHandler>(agent.TestDefaultChromiumHttpHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithOnlyChromiumHandler_ShouldCreateNewDefaultAndFlareSolverrHandlers()
    {
        // Arrange
        var chromiumHandler = new HttpClientHandler();
        var options = new Dictionary<string, object>
        {
            { "ChromiumHttpHandler", chromiumHandler }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        // When Chromium is provided, it should be used for the Default handler
        // but a new handler should be created for FlareSolverr
        Assert.Same(chromiumHandler, agent.TestDefaultHttpClientHandler);
        Assert.NotSame(chromiumHandler, agent.TestDefaultFlareSolverrHttpHandler);
        Assert.IsType<HttpClientHandler>(agent.TestDefaultFlareSolverrHttpHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void GetKamiYomuUserAgent_ShouldContainPlatformAndArchitecture()
    {
        // Arrange
        var agent = new TestCrawlerAgent(new Dictionary<string, object>());

        // Act
        var userAgent = agent.TestGetKamiYomuUserAgent();

        // Assert
        Assert.Contains("KamiYomu-Agent/1.0", userAgent);
        Assert.Contains(Environment.OSVersion.Platform.ToString(), userAgent);
        var architecture = Environment.Is64BitOperatingSystem ? "x64" : "x86";
        Assert.Contains(architecture, userAgent);

        // Cleanup
        agent.Dispose();
    }

    [Theory]
    [InlineData("Mozilla/5.0 (Windows NT 10.0; Win64; x64)")]
    [InlineData("Chrome/91.0.4472.124")]
    [InlineData("KamiYomu-Agent/1.0 (Win32; x64)")]
    [InlineData("A")] // Single character
    [InlineData("!")] // Special printable character
    public void IsLikelyUserAgent_WithValidUserAgent_ShouldReturnTrue(string validUserAgent)
    {
        // Arrange
        var agent = new TestCrawlerAgent(new Dictionary<string, object>());

        // Act
        var result = agent.TestIsLikelyUserAgent(validUserAgent);

        // Assert
        Assert.True(result);

        // Cleanup
        agent.Dispose();
    }

    [Theory]
    [InlineData("")] // Empty string
    [InlineData("   ")] // Whitespace only
    [InlineData(null)] // Null
    public void IsLikelyUserAgent_WithEmptyOrWhitespaceUserAgent_ShouldReturnFalse(string invalidUserAgent)
    {
        // Arrange
        var agent = new TestCrawlerAgent(new Dictionary<string, object>());

        // Act
        var result = agent.TestIsLikelyUserAgent(invalidUserAgent);

        // Assert
        Assert.False(result);

        // Cleanup
        agent.Dispose();
    }

    [Theory]
    [InlineData("\x00")] // Null character
    [InlineData("\x01")] // Control character
    [InlineData("\x7F")] // DEL character
    public void IsLikelyUserAgent_WithNonPrintableCharacters_ShouldReturnFalse(string invalidUserAgent)
    {
        // Arrange
        var agent = new TestCrawlerAgent(new Dictionary<string, object>());

        // Act
        var result = agent.TestIsLikelyUserAgent(invalidUserAgent);

        // Assert
        Assert.False(result);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void IsLikelyUserAgent_WithExceeding512Characters_ShouldReturnFalse()
    {
        // Arrange
        var agent = new TestCrawlerAgent(new Dictionary<string, object>());
        var longUserAgent = new string('a', 513);

        // Act
        var result = agent.TestIsLikelyUserAgent(longUserAgent);

        // Assert
        Assert.False(result);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void IsLikelyUserAgent_With512Characters_ShouldReturnTrue()
    {
        // Arrange
        var agent = new TestCrawlerAgent(new Dictionary<string, object>());
        var userAgent = new string('a', 512);

        // Act
        var result = agent.TestIsLikelyUserAgent(userAgent);

        // Assert
        Assert.True(result);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void GetCrawlerCoreAssemblyVersion_ShouldReturnValidVersion()
    {
        // Arrange
        var agent = new TestCrawlerAgent(new Dictionary<string, object>());

        // Act
        var version = agent.GetCrawlerCoreAssemblyVersion();

        // Assert
        Assert.NotNull(version);
        Assert.True(version.Major >= 0);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void GetCrawlerCoreInformationalVersion_ShouldReturnNonEmptyString()
    {
        // Arrange
        var agent = new TestCrawlerAgent(new Dictionary<string, object>());

        // Act
        var informationalVersion = agent.GetCrawlerCoreInformationalVersion();

        // Assert
        Assert.NotNull(informationalVersion);
        Assert.NotEmpty(informationalVersion);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public async Task Dispose_ShouldDisposeAllHttpClientHandlersAsync()
    {
        // Arrange
        var smartCrawlerHandler = new HttpClientHandler();
        var flareSolverrHandler = new HttpClientHandler();
        var chromiumHandler = new HttpClientHandler();

        var options = new Dictionary<string, object>
        {
            { "SmartCrawlerHttpHandler", smartCrawlerHandler },
            { "FlareSolverrHttpHandler", flareSolverrHandler },
            { "ChromiumHttpHandler", chromiumHandler }
        };
        var agent = new TestCrawlerAgent(options);

        // Act
        agent.Dispose();

        // Assert – using the handlers should now throw
        using var smartClient = new HttpClient(smartCrawlerHandler, disposeHandler: false);
        using var flareClient = new HttpClient(flareSolverrHandler, disposeHandler: false);
        using var chromiumClient = new HttpClient(chromiumHandler, disposeHandler: false);

        await Assert.ThrowsAsync<ObjectDisposedException>(
            () => smartClient.GetAsync("https://example.com"));

        await Assert.ThrowsAsync<ObjectDisposedException>(
            () => flareClient.GetAsync("https://example.com"));

        await Assert.ThrowsAsync<ObjectDisposedException>(
            () => chromiumClient.GetAsync("https://example.com"));
    }

    [Fact]
    public void Dispose_MultipleTimes_ShouldNotThrow()
    {
        // Arrange
        var options = new Dictionary<string, object>();
        var agent = new TestCrawlerAgent(options);

        // Act & Assert - Should not throw
        agent.Dispose();
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithAllOptionsConfigured_ShouldApplyAllConfigurations()
    {
        // Arrange
        const string customUserAgent = "Mozilla/5.0 (Custom)";
        const int customTimeout = 45_000;
        var smartCrawlerHandler = new HttpClientHandler();
        var flareSolverrHandler = new HttpClientHandler();
        var chromiumHandler = new HttpClientHandler();
        var mockLogger = new Mock<ILogger>();

        var options = new Dictionary<string, object>
        {
            { "BrowserUserAgent", customUserAgent },
            { "HttpClientTimeout", customTimeout.ToString() },
            { "SmartCrawlerHttpHandler", smartCrawlerHandler },
            { "FlareSolverrHttpHandler", flareSolverrHandler },
            { "ChromiumHttpHandler", chromiumHandler },
            { "KamiYomuILogger", mockLogger.Object }
        };

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.Equal(customUserAgent, agent.TestHttpClientDefaultUserAgent);
        Assert.Equal(customTimeout, agent.TestTimeoutMilliseconds);
        Assert.Same(smartCrawlerHandler, agent.TestDefaultHttpClientHandler);
        Assert.Same(flareSolverrHandler, agent.TestDefaultFlareSolverrHttpHandler);
        Assert.Same(chromiumHandler, agent.TestDefaultChromiumHttpHandler);
        Assert.Same(mockLogger.Object, agent.TestLogger);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_CreatesSeparateHttpHandlers_ShouldNotShareHandlers()
    {
        // Arrange
        var options = new Dictionary<string, object>();

        // Act
        var agent = new TestCrawlerAgent(options);

        // Assert
        Assert.NotSame(agent.TestDefaultHttpClientHandler, agent.TestDefaultFlareSolverrHttpHandler);
        Assert.NotSame(agent.TestDefaultHttpClientHandler, agent.TestDefaultChromiumHttpHandler);
        Assert.NotSame(agent.TestDefaultFlareSolverrHttpHandler, agent.TestDefaultChromiumHttpHandler);

        // Cleanup
        agent.Dispose();
    }

    [Fact]
    public void Constructor_WithNullOptions_ShouldCreateDefaultHandlers()
    {
        // Arrange - Pass null instead of empty dictionary
        IDictionary<string, object> nullOptions = null;

        // Act
        var agent = new TestCrawlerAgent(nullOptions);

        // Assert
        Assert.NotNull(agent.TestOptions);
        Assert.NotNull(agent.TestDefaultHttpClientHandler);
        Assert.NotNull(agent.TestDefaultFlareSolverrHttpHandler);
        Assert.NotNull(agent.TestDefaultChromiumHttpHandler);

        // Cleanup
        agent.Dispose();
    }
}