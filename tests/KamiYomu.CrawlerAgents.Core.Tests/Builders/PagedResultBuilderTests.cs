namespace KamiYomu.CrawlerAgents.Core.Tests.Builders;

public class PagedResultBuilderTests
{
    private class DummyItem
    {
        public string Name { get; set; }
    }

    [Fact]
    public void Create_ShouldReturnNewBuilderInstance()
    {
        var builder = PagedResultBuilder<DummyItem>.Create();
        Assert.NotNull(builder);
    }

    [Fact]
    public void WithPaginationOptions_ShouldSetPaginationOptions()
    {
        var options = new PaginationOptions(20, 10);
        var result = PagedResultBuilder<DummyItem>.Create()
            .WithPaginationOptions(options)
            .Build();

        Assert.Equal(10, result.PaginationOptions.Limit);
        Assert.Equal(20, result.PaginationOptions.OffSet);
    }

    [Fact]
    public void WithData_ShouldSetDataCollection()
    {
        var data = new List<DummyItem>
    {
        new() { Name = "Item1" },
        new() { Name = "Item2" }
    };

        var result = PagedResultBuilder<DummyItem>.Create()
            .WithData(data)
            .Build();

        Assert.Equal(2, result.Data.Count());
        Assert.Contains(result.Data, d => d.Name == "Item1");
        Assert.Contains(result.Data, d => d.Name == "Item2");
    }

    [Fact]
    public void Build_ShouldReturnEmptyResult_WhenNoDataOrOptionsSet()
    {
        var result = PagedResultBuilder<DummyItem>.Create().Build();

        Assert.Null(result.PaginationOptions);
        Assert.Null(result.Data);
    }

    [Fact]
    public void WithData_ShouldHandleNullData()
    {
        var result = PagedResultBuilder<DummyItem>.Create()
            .WithData(null)
            .Build();

        Assert.Null(result.Data);
    }

    [Fact]
    public void WithPaginationOptions_ShouldHandleNullOptions()
    {
        var result = PagedResultBuilder<DummyItem>.Create()
            .WithPaginationOptions(null)
            .Build();

        Assert.Null(result.PaginationOptions);
    }

    [Fact]
    public void Build_ShouldReturnFullyPopulatedPagedResult()
    {
        var data = new List<DummyItem> { new() { Name = "Test" } };
        var options = new PaginationOptions(0, 5);

        var result = PagedResultBuilder<DummyItem>.Create()
            .WithData(data)
            .WithPaginationOptions(options)
            .Build();

        Assert.Equal(data, result.Data);
        Assert.Equal(options, result.PaginationOptions);
    }
}
