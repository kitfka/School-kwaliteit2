using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Webshop.Api.Controllers;
using Webshop.Data;
using Webshop.Model;

namespace Webshop.Api.Tests.Controllers;

public class ProductControllerFacts
{

    private static ProductController GetProductController() => new ProductController(new MockProductRepository(), new MockAuthService());

    private readonly Pagination DefaultPagination = new();

    #region Get
    [Fact]
    public void Get_Should_Return_NotNull()
    {
        // Arrange
        ProductController controller = GetProductController();

        // Act
        var result = (JsonResult)controller.Get(DefaultPagination);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Get_Should_Return_ProductList()
    {
        // Arrange
        ProductController controller = GetProductController();

        // Act
        var rawResult = controller.Get(DefaultPagination);

        // Assert
        var jsonResult = Assert.IsType<JsonResult>(rawResult);
        var finalResult = Assert.IsType<List<Product>>(jsonResult.Value);
        Assert.True(finalResult.Count != 0);
    }
    #endregion


    #region GetOne
    [Fact]
    public void GetOne_Should_Return_Product()
    {
        // Arrange
        ProductController controller = GetProductController();

        // Act
        var result = (JsonResult)controller.GetOne(Guid.Parse("46295400-FA7E-46BC-8A8B-E70ECECD5692"));
        Product? result2 = result.Value as Product;

        // Assert
        Assert.NotNull(result2);
    }
    #endregion


    #region GetVariantsOfProduct

    #endregion


    #region GetSearchProduct
    [Fact]
    public void GetSearchProduct_Should_Return_Koffie()
    {
        // Arrange
        ProductController controller = GetProductController();

        // Act
        var ControllerResult = (JsonResult)controller.GetSearchProduct("kopje");
        object? controllerValue = ControllerResult.Value;
        Assert.NotNull(controllerValue);
        List<Product> results = (List<Product>)controllerValue;
        Product result = results.First();

        // Assert
        Assert.Equal("koffie kopje", result.Name);
    }

    [Fact]
    public void GetSearchProduct_Blank_Should_Return_SameAsGet()
    {
        // Arrange
        ProductController controller = GetProductController();
        JsonResult result1;
        JsonResult result2;

        // Act
        result1 = (JsonResult)controller.Get(DefaultPagination);
        result2 = (JsonResult)controller.GetSearchProduct("");

        // Assert
        Assert.Equal(result1.Value, result2.Value);
    }
    #endregion


    #region Post
    //[Fact]
    //public void Post_Should_ShoudIncreaseProducts()
    //{
    //    // Arrange
    //    ProductController controller = GetProductController();
    //    Product product = new()
    //    {
    //        Description = "description",
    //        ImageSource = "image",
    //        Name = "name",
    //        Price = 1,
    //    };

    //    // Act
    //    var actionResult = controller.Post(product);

    //    // Assert
    //    //Assert.IsType

    //}

    [Theory]
    [InlineData("", "description", "source", 12)]
    [InlineData("prodName", "", "source", 12)]
    //[InlineData("prodName", "description", "", 12)] // This is fine
    [InlineData("prodName", "description", "source", 0)]
    [InlineData("prodName", "description", "source", -1)]
    public void Post_Invalid_ReturnsBadRequestObjectResult(string name, string description, string imageSource, decimal price)
    {
        // Arrange
        ProductController controller = GetProductController();

        // Act
        var actionResult = controller.Post(new() 
        {
            Description = description,
            ImageSource = imageSource,
            Name = name,
            Price = price,
        });

        // Assert
        Assert.IsType<BadRequestObjectResult>(actionResult);
    }

    [Fact]
    public void Post_Valid_ReturnsOkObjectResult()
    {
        // Arrange
        ProductController controller = GetProductController();
        Product product = new()
        {
            Description = "description",
            ImageSource = "image",
            Name = "name",
            Price = 1,
        };

        // Act
        var actionResult = controller.Post(product);

        // Assert
        Assert.IsType<OkObjectResult>(actionResult);
    }
    #endregion

    #region Delete
    [Fact]
    public void Delete_Should_NoLongerBeRetrievable()
    {
        // Arrange
        ProductController controller = GetProductController();
        Product product = MockData.Products.First();

        // Act
        var actionResult = controller.Delete(product);
        var actionResult2 = controller.Get(DefaultPagination);

        // Assert
        Assert.IsType<OkResult>(actionResult);
        var jsonResult = Assert.IsType<JsonResult>(actionResult2);
        var result = Assert.IsType<List<Product>>(jsonResult.Value);
        Assert.False(result.Where(x => x.Id == product.Id).Any());
    }

    // Not 100% sure if we should tell the user this. Also not worth the time for now.
    //[Fact]
    //public void Delete_Invalid_ShouldRetrieveBadRequest()
    //{
    //    // Arrange
    //    ProductController controller = GetProductController();
    //    Product product = new();

    //    // Act
    //    var actionResult = controller.Delete(product);

    //    // Assert
    //    Assert.IsType<BadRequest>(actionResult);
    //}
    #endregion

    #region Patch
    [Fact]
    public void Patch_Should_ReturnChangedProduct()
    {
        // Arrange
        ProductController controller = GetProductController();
        Product product = MockData.Products.First();
        string notExpected = product.Name;

        // Act
        product.Name = "NewProductName";
        var actionResult = controller.Patch(product.Id, product);
        var actionResult2 = controller.Get(DefaultPagination);

        // Assert
        Assert.IsType<OkResult>(actionResult);
        var jsonResult = Assert.IsType<JsonResult>(actionResult2);
        var result = Assert.IsType<List<Product>>(jsonResult.Value);
        var newProduct = result.Where(x => x.Id == product.Id).First();
        Assert.NotEqual(notExpected, newProduct.Name);
    }

    //[Fact]
    //public void Patch_ChangeName_ReturnsOtherProductFieldsAreTheSame()
    //{
    //    // Arrange
    //    ProductController controller = GetProductController();
    //    Product product = MockData.Products.First();
    //    string expected = product.Description;

    //    // Act
    //    _ = controller.Patch(product.Id, new() { Name = "NewProductName" });
    //    var actionResult = controller.GetOne(product.Id);

    //    // Assert
    //    Assert.IsType<OkResult>(actionResult);
    //    var jsonResult = Assert.IsType<JsonResult>(actionResult);
    //    var actualProduct = Assert.IsType<Product>(jsonResult.Value);
    //    Assert.Equal(expected, actualProduct.Description);
    //}
    #endregion
}
