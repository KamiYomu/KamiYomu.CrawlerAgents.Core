using Microsoft.Extensions.Logging;
using System.Reflection;

namespace KamiYomu.CrawlerAgents.Core;

public abstract class AbstractCrawlerAgent
{
    protected readonly IDictionary<string, object> Options = new Dictionary<string, object>();
    protected readonly string HttpClientDefaultUserAgent  = CrawlerAgentSettings.HttpUserAgent;
    protected readonly int TimeoutMilliseconds  = CrawlerAgentSettings.TimeoutMilliseconds;
    protected readonly ILogger Logger;
    protected AbstractCrawlerAgent(IDictionary<string, object> options)
    {
        Options = options ?? new Dictionary<string, object>();

        if (Options.TryGetValue(CrawlerAgentSettings.DefaultInputs.BrowserUserAgent, out var userAgentObj))
        {
            if (CrawlerAgentSettings.IsLikelyUserAgent(userAgentObj as string))
            {
                HttpClientDefaultUserAgent = userAgentObj as string;
            }
        }

        if (Options.TryGetValue(CrawlerAgentSettings.DefaultInputs.HttpClientTimeout, out var timeoutObj))
        {
            TimeoutMilliseconds = int.Parse(timeoutObj as string);
        }

        if (Options.TryGetValue(CrawlerAgentSettings.DefaultInputs.KamiYomuILogger, out var loggerObj))
        {
            Logger = loggerObj as ILogger;
        }
    }

    public Version GetCrawlerCoreAssemblyVersion()
    {
        var assembly = this.GetType().Assembly;
        return assembly.GetName().Version;
    }

    public string GetCrawlerCoreInformationalVersion()
    {
        var attr = this.GetType().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        return attr?.InformationalVersion ?? "unknown";
    }
}
