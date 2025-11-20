using Microsoft.Extensions.Logging;
using System.Reflection;

namespace KamiYomu.CrawlerAgents.Core.Tests
{
    public class TestCrawlerAgent : AbstractCrawlerAgent
    {
        public TestCrawlerAgent(IDictionary<string, object> options) : base(options) { }
    }

    public class AbstractCrawlerAgentTests
    {
        [Fact]
        public void Constructor_UsesDefaultValues_WhenNoOptionsProvided()
        {
            var agent = new TestCrawlerAgent(null);

            Assert.Equal(CrawlerAgentSettings.HttpUserAgent, agent.GetType()
                .GetField("HttpClientDefaultUserAgent", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(agent));

            Assert.Equal(CrawlerAgentSettings.TimeoutMilliseconds, agent.GetType()
                .GetField("TimeoutMilliseconds", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(agent));

            Assert.Null(agent.GetType()
                .GetField("Logger", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(agent));
        }

        [Fact]
        public void Constructor_OverridesUserAgent_WhenValidUserAgentProvided()
        {
            var options = new Dictionary<string, object>
        {
            { CrawlerAgentSettings.DefaultInputs.BrowserUserAgent, "Mozilla/5.0" }
        };

            var agent = new TestCrawlerAgent(options);

            var userAgent = agent.GetType()
                .GetField("HttpClientDefaultUserAgent", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(agent);

            Assert.Equal("Mozilla/5.0", userAgent);
        }

        [Fact]
        public void Constructor_OverridesTimeout_WhenTimeoutProvided()
        {
            var options = new Dictionary<string, object>
        {
            { CrawlerAgentSettings.DefaultInputs.HttpClientTimeout, "5000" }
        };

            var agent = new TestCrawlerAgent(options);

            var timeout = agent.GetType()
                .GetField("TimeoutMilliseconds", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(agent);

            Assert.Equal(5000, timeout);
        }

        [Fact]
        public void Constructor_AssignsLogger_WhenLoggerProvided()
        {
            var logger = new LoggerFactory().CreateLogger("Test");
            var options = new Dictionary<string, object>
        {
            { CrawlerAgentSettings.DefaultInputs.KamiYomuILogger, logger }
        };

            var agent = new TestCrawlerAgent(options);

            var loggerField = agent.GetType()
                .GetField("Logger", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(agent);

            Assert.Equal(logger, loggerField);
        }

        [Fact]
        public void GetCrawlerCoreAssemblyVersion_ReturnsAssemblyVersion()
        {
            var agent = new TestCrawlerAgent(null);
            var version = agent.GetCrawlerCoreAssemblyVersion();

            Assert.NotNull(version);
            Assert.True(version.Major >= 0);
        }

        [Fact]
        public void GetCrawlerCoreInformationalVersion_ReturnsInformationalVersionOrUnknown()
        {
            var agent = new TestCrawlerAgent(null);
            var infoVersion = agent.GetCrawlerCoreInformationalVersion();

            Assert.False(string.IsNullOrWhiteSpace(infoVersion));
        }
    }

}
