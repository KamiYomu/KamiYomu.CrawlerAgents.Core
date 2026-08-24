using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiYomu.CrawlerAgents.Core;
/// <summary>
/// Download headers interface for crawler agents, 
/// providing a mechanism to retrieve default HTTP headers used in requests to the target site.
/// </summary>
public interface IDefaultHeadersCrawlerAgent
{
    /// <summary>
    /// Retrieves the default HTTP headers used by the crawler agent in its requests to the target site.
    /// The download of files will use these headers to ensure proper authentication, content negotiation, and other necessary request configurations.
    /// </summary>
    /// <returns>A collection of key-value pairs representing the default headers.</returns>
    IEnumerable<KeyValuePair<string, string>> GetDefaultHeaders();
}
