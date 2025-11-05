namespace KamiYomu.CrawlerAgents.Core.Catalog;

/// <summary>
/// Defines pagination metadata for data retrieval operations, supporting both traditional paging
/// (via page number and page size) and continuation token-based pagination for cursor-driven workflows.
/// This class enables flexible pagination strategies for APIs, UI components, and data sources that
/// require either offset-based or token-based navigation.
/// </summary>
public class PaginationOptions
{
    public PaginationOptions() { }


    /// <summary>
    /// Initializes a new instance of <see cref="PaginationOptions"/> using a continuation token and total item count.
    /// </summary>
    /// <param name="continuationToken">A token that identifies the position in a paginated data stream.</param>
    /// <param name="total">The total number of items available across all pages.</param>
    public PaginationOptions(string continuationToken, int? total = 30)
    {
        ContinuationToken = continuationToken;
        Total = total;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="PaginationOptions"/> using offset-based pagination parameters.
    /// </summary>
    /// <param name="offset">The zero-based index of the current page.</param>
    /// <param name="limit">The maximum number of items per page.</param>
    /// <param name="total">The total number of items available across all pages.</param>
    public PaginationOptions(int? offset, int? limit, int? total = 30)
    {
        OffSet = offset;
        Limit = limit;
        Total = total;
    }

    /// <summary>
    /// Gets or sets the zero-based index of the current page for offset-based pagination. Optional.
    /// </summary>
    [JsonInclude]
    public int? OffSet { get; internal set; }

    /// <summary>
    /// Gets or sets the maximum number of items per page for offset-based pagination. Optional.
    /// </summary>
    [JsonInclude]
    public int? Limit { get; internal set; }

    /// <summary>
    /// Gets or sets the total number of items available across all pages. Optional.
    /// </summary>
    [JsonInclude]
    public int? Total { get; internal set; }

    /// <summary>
    /// Gets or sets the continuation token used to resume paginated queries. Optional.
    /// </summary>
    [JsonInclude] 
    public string ContinuationToken { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether continuation token-based pagination is being used.
    /// </summary>
    public bool IsContinuation() => !string.IsNullOrEmpty(ContinuationToken);

    /// <summary>
    /// Gets a value indicating whether traditional pagination <see cref="OffSet"/>, <see cref="Limit"/> is being used.
    /// </summary>
    public bool IsTraditional() => OffSet.HasValue && Limit.HasValue;

    /// <summary>
    /// Returns a human-readable description of the pagination mode and its associated parameters.
    /// </summary>
    public override string ToString()
    {
        if (IsContinuation())
            return ContinuationToken;
        if (IsTraditional())
            return $"{OffSet}-{Limit}";
        return "pagination-not-configured";
    }
}
