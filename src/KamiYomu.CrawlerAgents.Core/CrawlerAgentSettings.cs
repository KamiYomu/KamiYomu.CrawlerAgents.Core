using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace KamiYomu.CrawlerAgents.Core;

/// <summary>
/// Provides configuration constants for the crawler agent, including a standardized HTTP User-Agent string
/// that reflects the operating system platform and architecture (x64 or x86).
/// </summary>
public static class CrawlerAgentSettings
{
    /// <summary>
    /// Gets the default HTTP User-Agent string used by the crawler agent, including the OS platform and architecture (x64 or x86).
    /// </summary>
    public static readonly string HttpUserAgent = $"KamiYomu-Agent/1.0 ({Environment.OSVersion.Platform}; {(Environment.Is64BitOperatingSystem ? "x64" : "x86")})";
    /// <summary>
    /// Maximum navigation time in milliseconds, defaults to 60 seconds.
    /// </summary>
    public static readonly int TimeoutMilliseconds = 60000;

    internal static readonly Regex UserAgentRegex = new Regex(@"^[ -~]{1,512}$", RegexOptions.Compiled);

    public static bool IsLikelyUserAgent(string userAgent)
    {
        return !string.IsNullOrWhiteSpace(userAgent) && UserAgentRegex.IsMatch(userAgent);
    }

    public static class DefaultInputs
    {
        public const string BrowserUserAgent = nameof(BrowserUserAgent);
        public const string HttpClientTimeout = nameof(HttpClientTimeout);
        public const string KamiYomuILogger = nameof(KamiYomuILogger);

        private static string[] ExistingInputs =
        [
            BrowserUserAgent,
            HttpClientTimeout,
            KamiYomuILogger
        ];
    }
}
