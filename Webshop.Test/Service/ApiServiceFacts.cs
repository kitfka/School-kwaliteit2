using Webshop.Model;
using Webshop.Service;

namespace Webshop.Test.Service;

/// <summary>
/// The https://cith.dev/api/school/list get api call returns the fallowing data:
/// [
///    {
///        "method": "GET",
///        "route": "/api/School/List",
///        "action": "School.GetList"
///    },
///    {
///    "method": "GET",
///        "route": "/api/School",
///        "action": "School.Details"
///    },
///    {
///    "method": "GET",
///        "route": "/api/School/{id}",
///        "action": "School.OneDetail"
///    },
///    {
///    "method": "POST",
///        "route": "/api/School/Create/{id}",
///        "action": "School.Create"
///    },
///    {
///    "method": "PUT",
///        "route": "/api/School/{id}/{newValue}",
///        "action": "School.Edit"
///    },
///    {
///    "method": "DELETE",
///        "route": "/api/School/{id}",
///        "action": "School.Delete"
///    },
///    {
///    "method": "GET",
///        "route": "/api/School/Reset",
///        "action": "School.Reset"
///    }
///]
///
/// This api exists only to test code that calls api's like the ApiService class.
/// The server stores the data in a Dictionary<int, string>. To many entrys will trigger an autoclear method.
/// </summary>
[Collection("ApiService Tests")]
public class ApiServiceFacts
{
    private const string ApiUrl = "https://cith.dev/api/school/";
    private static readonly ApiService ApiService = new();

    private static void SetTestMode()
    {
        ApiService.TestMode = true;
        ApiService.TestBaseUrl = ApiUrl;
    }

    private static async void ResetApi()
    {
        // will clear all data in test api
        _ = await ApiService.GetAsync("Reset");
    }

    // Get/Post/Put/Delete
    #region Get
    [Fact]
    public async void Get_Should_Return()
    {
        // Arrange
        SetTestMode();

        // Act
        var result = await ApiService.GetStringAsync("list");

        // Assert
        Assert.False(string.IsNullOrEmpty(result));
    }
    #endregion

    #region Post
    [Fact]
    public async void Post_Should_Return()
    {
        // Arrange
        SetTestMode();
        string expected = "\"\"";

        // Act
        _ = await ApiService.PostAsync("Create/1", "");
        var responseMessage = await ApiService.GetAsync("1");
        string actual = await responseMessage.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(expected, actual);
        ResetApi();
    }
    #endregion

    #region Put
    [Fact]
    public async void Put_Should_Return()
    {
        // Arrange
        SetTestMode();
        string expected = "hello";

        // Act
        _ = await ApiService.PostAsync("Create/1", "");
        _ = await ApiService.PutAsync($"1/{expected}", new object());
        var result = await ApiService.GetStringAsync("1");

        // Assert
        Assert.Equal(expected, result.Trim('"'));
        ResetApi();
    }
    #endregion

    #region Delete
    [Fact]
    public async void Delete_Should_Return()
    {
        // Arrange
        SetTestMode();

        // Act
        _ = await ApiService.PostAsync("Create/1", "");
        var resultA = await ApiService.GetStringAsync("");
        _ = await ApiService.DeleteAsync("1");
        var resultB = await ApiService.GetStringAsync("");

        // Assert
        Assert.NotEqual("{}", resultA);
        Assert.Equal("{}", resultB);
        ResetApi();
    }
    #endregion

}
