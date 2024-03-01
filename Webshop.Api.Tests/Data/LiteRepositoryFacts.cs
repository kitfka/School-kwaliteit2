using Microsoft.Extensions.Configuration;
using Webshop.Api.Data;
using Webshop.Model;
using Webshop.Model.Interfaces;

namespace Webshop.Api.Tests.Data;
public class LiteRepositoryFacts
{
    private readonly IConfiguration configuration = new MockConfiguration();

    [Fact]
    public void Add()
    {// Arrange
        LiteDBService liteDBService = new(configuration);
        LiteRepository<TestItem> repository = new(liteDBService);

        // Act
        repository.Add(new TestItem() { Name = "name" });
        var result = repository.List();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Delete()
    {
        // Arrange
        LiteDBService liteDBService = new(configuration);
        LiteRepository<TestItem> repository = new(liteDBService);


        // Act
        repository.Add(new TestItem() { Name = "name" });

        var item = repository.List().First();

        repository.Delete(item);

        var result = repository.List();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void EditAndGetById()
    {
        // Arrange
        LiteDBService liteDBService = new(configuration);
        LiteRepository<TestItem> repository = new(liteDBService);
        string expected = "newName";
        string actual;

        // Act
        repository.Add(new TestItem() { Name = "name" });

        var item = repository.List().First();
        item.Name = expected;
        repository.Edit(item);

        var result = repository.GetById(item.Id);
        actual = result.Name;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void List()
    {
        // Arrange
        LiteDBService liteDBService = new(configuration);
        LiteRepository<TestItem> repository = new(liteDBService);
        int actual;
        int expected = 3;

        // Act
        repository.Add(new TestItem() { Name = "name1" });
        repository.Add(new TestItem() { Name = "name2" });
        repository.Add(new TestItem() { Name = "name3" });

        var item = repository.List();
        actual = item.Count();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ListWithExpression()
    {
        // Arrange
        LiteDBService liteDBService = new(configuration);
        LiteRepository<TestItem> repository = new(liteDBService);
        int actual;
        int expected = 1;

        // Act
        repository.Add(new TestItem() { Name = "name1" });
        repository.Add(new TestItem() { Name = "name2" });
        repository.Add(new TestItem() { Name = "name3" });

        var item = repository.List(x => x.Name == "name2");
        actual = item.Count();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ListWithLimit()
    {
        // Arrange
        LiteDBService liteDBService = new(configuration);
        LiteRepository<TestItem> repository = new(liteDBService);
        int actual;
        int expected = 2;

        // Act
        repository.Add(new TestItem() { Name = "name1" });
        repository.Add(new TestItem() { Name = "name2" });
        repository.Add(new TestItem() { Name = "name3" });
        repository.Add(new TestItem() { Name = "name4" });
        repository.Add(new TestItem() { Name = "name5" });

        var item = repository.List(1, 2);
        actual = item.Count();

        // Assert
        Assert.Equal(expected, actual);
    }
}

public class TestItem : BaseEntity
{
    public string Name { get; set; } = "";
}
