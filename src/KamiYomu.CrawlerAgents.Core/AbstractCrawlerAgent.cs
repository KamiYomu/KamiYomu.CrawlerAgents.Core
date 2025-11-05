using System.Reflection;

namespace KamiYomu.CrawlerAgents.Core;

public abstract class AbstractCrawlerAgent
{
    protected readonly IDictionary<string, object> Options;

    public AbstractCrawlerAgent(IDictionary<string, object> options)
    {
        Options = options ?? new Dictionary<string, object>();
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
