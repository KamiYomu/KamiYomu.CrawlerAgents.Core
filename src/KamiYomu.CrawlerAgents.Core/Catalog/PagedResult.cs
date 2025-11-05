namespace KamiYomu.CrawlerAgents.Core.Catalog;

/// <summary>
/// Represents a paginated result set containing metadata and a collection of items.
/// </summary>
/// <typeparam name="T">The type of items contained in the result set.</typeparam>
public class PagedResult<T> where T : class
{
    public PagedResult() { }

    /// <summary>
    /// Gets the pagination settings that describe the current page <see cref="PaginationOptions.OffSet">, <see cref="PaginationOptions.Limit">, and <see cref="PaginationOptions.t"> item count or .
    /// </summary>
    [JsonInclude] 
    public PaginationOptions PaginationOptions { get; internal set; }

    /// <summary>
    /// Gets the collection of items in the current page.
    /// </summary>
    [JsonInclude]
    public IEnumerable<T> Data { get; internal set; }
}
