namespace KamiYomu.CrawlerAgents.Core.Catalog.Builders
{
    /// <summary>
    /// Provides a fluent builder for constructing <see cref="PagedResultBuilder{T}"/>. This class simplifies <see cref="PagedResult{T}"/> creation
    /// by chaining configuration methods and producing a fully initialized <see cref="PagedResultBuilder{T}"/> via <see cref="Build"/>.
    /// </summary>
    public class PagedResultBuilder<T> where T : class
    {
        private readonly PagedResult<T> _result = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResultBuilder{T}"/> class.
        /// </summary>
        private PagedResultBuilder() { }

        /// <summary>
        /// Creates a new <see cref="PagedResultBuilder{T}"/> instance.
        /// </summary>
        /// <returns>A new builder for constructing a <see cref="PagedResult{T}"/>.</returns>
        public static PagedResultBuilder<T> Create()
        {
            return new PagedResultBuilder<T>();
        }

        /// <summary>
        /// Sets the pagination metadata for the result, supporting either offset-based or continuation token-based pagination.
        /// </summary>
        /// <param name="paginationOptions">The pagination options to associate with the result.</param>
        /// <returns>The current builder instance.</returns>
        public PagedResultBuilder<T> WithPaginationOptions(PaginationOptions paginationOptions)
        {
            _result.PaginationOptions = paginationOptions;
            return this;
        }


        /// <summary>
        /// Sets the collection of items for the current page.
        /// </summary>
        /// <param name="data">The items to include in the result.</param>
        /// <returns>The current builder instance.</returns>
        public PagedResultBuilder<T> WithData(IEnumerable<T> data)
        {
            _result.Data = data;
            return this;
        }

        /// <summary>
        /// Builds and returns the configured <see cref="PagedResult{T}"/> instance.
        /// </summary>
        /// <returns>The constructed paged result.</returns>
        public PagedResult<T> Build() => _result;
    }
}
