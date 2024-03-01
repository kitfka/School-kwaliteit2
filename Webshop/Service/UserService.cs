using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Webshop.Data;
using Webshop.Model;

namespace Webshop.Service;

// to keep track of authentication.
public class UserService
{
    private readonly ApiService ApiService;

    public UserService(ApiService ApiService)
    {
        this.ApiService = ApiService;
    }

    public async Task LoginAsync(LoginUser loginUser)
    {
        var result = await ApiService.PostAsync("api/auth/login", loginUser);
        var r = await ApiService.HttpResponseToObjectAsync<ApplicationUser>(result);

        if (r.Token != string.Empty)
        {
            ApiService.Token = r.Token;
        }
    }

    public async Task RegisterAsync(RegisterUser registerUser)
    {
        var result = await ApiService.PostAsync("api/auth/register", registerUser);
        var r = await ApiService.HttpResponseToObjectAsync<ApplicationUser>(result);

        if (r.Token != string.Empty)
        {
            ApiService.Token = r.Token;
        }
    }


    public async Task LoginAsync(string username, string password)
    {
        // api/auth/login
        LoginUser loginUser = new() { Email = username, Password = password };
        var result = await ApiService.PostAsync("api/auth/login", loginUser);

    }

    public bool IsLoggedIn()
    {
        return !string.IsNullOrEmpty(ApiService.Token);
    }

    public void Logout()
    {
        ApiService.Token = string.Empty;
    }
}
