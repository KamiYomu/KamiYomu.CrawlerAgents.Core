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
}
