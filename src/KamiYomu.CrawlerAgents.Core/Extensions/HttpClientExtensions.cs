using PuppeteerSharp;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KamiYomu.CrawlerAgents.Core.Extensions;

/// <summary>
/// Provides extension methods for configuring HTTP headers on <see cref="HttpClient"/>
/// and <see cref="HttpRequestMessage"/> instances.
/// </summary>
/// <remarks>
/// These extensions simplify the process of adding multiple HTTP headers without validation,
/// useful for custom or non-standard headers that may not pass standard HTTP header validation rules.
/// </remarks>
public static class HttpClientExtensions
{
    /// <summary>
    /// Adds multiple HTTP headers to an <see cref="HttpRequestMessage"/> without validation.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequestMessage"/> to add headers to.</param>
    /// <param name="headers">An enumerable collection of key-value pairs representing header names and values.</param>
    /// <remarks>
    /// Headers are added using <see cref="HttpHeaders.TryAddWithoutValidation(string, string)"/>,
    /// which allows for custom or non-standard headers that may not conform to RFC specifications.
    /// If a header fails to be added, the operation continues silently.
    /// </remarks>
    public static void AddRangeHeaders(this HttpRequestMessage request, params KeyValuePair<string, string>[] headers)
    {
        foreach (KeyValuePair<string, string> header in headers)
        {
            _ = request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }
    }

    /// <summary>
    /// Adds multiple HTTP headers to an <see cref="HttpClient"/>'s default request headers without validation.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> instance to configure.</param>
    /// <param name="headers">An enumerable collection of key-value pairs representing header names and values.</param>
    /// <remarks>
    /// Headers are added to the <see cref="HttpClient.DefaultRequestHeaders"/> using <see cref="HttpHeaders.TryAddWithoutValidation(string, string)"/>,
    /// allowing for custom or non-standard headers that may not conform to RFC specifications.
    /// These headers will be included in all subsequent requests made with this <see cref="HttpClient"/> instance.
    /// If a header fails to be added, the operation continues silently.
    /// </remarks>
    public static void AddRangeHeaders(this HttpClient httpClient, params KeyValuePair<string, string>[] headers)
    {
        foreach (KeyValuePair<string, string> header in headers)
        {
            _ = httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
        }
    }
    /// <summary>
    /// Adds multiple HTTP headers to an <see cref="HttpClient"/>'s default request headers without validation.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> instance to configure.</param>
    /// <param name="headers">An enumerable collection of key-value pairs representing header names and values.</param>
    /// <remarks>
    /// Headers are added to the <see cref="HttpClient.DefaultRequestHeaders"/> using <see cref="HttpHeaders.TryAddWithoutValidation(string, IEnumerable{KeyValuePair{string, string}})"/>,
    /// allowing for custom or non-standard headers that may not conform to RFC specifications.
    /// These headers will be included in all subsequent requests made with this <see cref="HttpClient"/> instance.
    /// If a header fails to be added, the operation continues silently.
    /// </remarks>
    public static void AddRangeHeaders(this HttpClient httpClient, IEnumerable<KeyValuePair<string, string>> headers)
    {
        httpClient.AddRangeHeaders([.. headers]);
    }
    /// <summary>
    /// Adds multiple HTTP headers to an <see cref="HttpRequestMessage"/> without validation.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequestMessage"/> to add headers to.</param>
    /// <param name="headers">An enumerable collection of key-value pairs representing header names and values.</param>
    /// <remarks>
    /// Headers are added using <see cref="HttpHeaders.TryAddWithoutValidation(string, IEnumerable{KeyValuePair{string, string}})"/>,
    /// which allows for custom or non-standard headers that may not conform to RFC specifications.
    /// If a header fails to be added, the operation continues silently.
    /// </remarks>
    public static void AddRangeHeaders(this HttpRequestMessage request, IEnumerable<KeyValuePair<string, string>> headers)
    {
        request.AddRangeHeaders([.. headers]);
    }
}
