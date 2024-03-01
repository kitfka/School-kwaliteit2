using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Webshop.Model;

namespace Webshop.Service;

// https://stackoverflow.com/questions/45711428/download-file-with-webclient-or-httpclient
// https://learn.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

/// <summary>
/// A service for Api calls. Should not be used directly but be consumed in other services!
/// </summary>
public class ApiService
{
    public static readonly HttpClient client = new();
    private const string BaseUrlTest = "https://localhost:7090";
    private const string OnlineUrl = BaseUrlTest; // change this to your server address to make the application work outside of local development.

    public static bool TestMode = false;

    public static string TestBaseUrl = "";

    public string Token { get; set; } = "";

    private static string BaseUrl
    {
        get
        {
            
            if (TestMode) { return  TestBaseUrl; }
            switch (Model.Config.Config.BaseUrlOption)
            {
                default:
                case Data.BaseUrlOption.Unspecified: //we could remove this line, but it is better to be descriptive like this.
#if DEBUG
                    return BaseUrlTest;
#else
                    
                    return OnlineUrl;
#endif

                case Data.BaseUrlOption.Local:
                    return BaseUrlTest;
                case Data.BaseUrlOption.Online:
                case Data.BaseUrlOption.VM:
                    return OnlineUrl;
            }
        }
    }


    public ApiService()
    {
        client.BaseAddress = new Uri(BaseUrl); // remove this later!
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }


    // BUG: this can cause problems!
    public static T JsonToObject<T>(string json) where T : new()
    {
        //if (!string.IsNullOrEmpty(json))
        //{
        //    T result = JsonConvert.DeserializeObject<T>(json);
        //    return result;
        //}
        return !string.IsNullOrEmpty(json) ? JsonConvert.DeserializeObject<T>(json) : new T();
    }

    public async Task<T> GetAndDeserializeAsync<T>(string path) where T : new()
    {
        try
        {
            string json_data = await GetStringAsync(path).ConfigureAwait(false);
            return JsonToObject<T>(json_data);
        }
        catch (Exception)
        {
            return new T();
        }

    }

    public static async Task<T> HttpResponseToObjectAsync<T>(HttpResponseMessage response) where T : new()
    {
        return JsonToObject<T>(await response.Content.ReadAsStringAsync());
    }

    /// <summary>
    /// Will use a Get request and return the result as a string.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="returnEmptyOnFail">Will return an empty string if the request was not a success.</param>
    /// <returns></returns>
    public async Task<string> GetStringAsync(string path, bool returnEmptyOnFail = true)
    {
        var result = await GetAsync(path).ConfigureAwait(false);
        if (!returnEmptyOnFail && !result.IsSuccessStatusCode)
        {
            return "";
        }
        return await result.Content.ReadAsStringAsync().ConfigureAwait(false);
    }


    #region Get/Post/Put/Delete methods
    // https://learn.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
    // TODO: Methods will hang application if the request fails, find better solution.
    // TODO: The rest of the models will fail hard. Give them also a try catch statement if needed.
    // FOR HEADERS https://stackoverflow.com/questions/12022965/adding-http-headers-to-httpclient
    public async Task<HttpResponseMessage> GetAsync(string path)
    {
        HttpRequestMessage message = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{BaseUrl}/{path}"),
        };
        if (!string.IsNullOrEmpty(Token))
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        }

        return await client.SendAsync(message).ConfigureAwait(false);
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string path, T ObjectToPost)
    {
        HttpRequestMessage message = new()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{BaseUrl}/{path}"),
            Content = JsonContent.Create(ObjectToPost),
        };
        if (!string.IsNullOrEmpty(Token))
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        }

        HttpResponseMessage response = await client.SendAsync(message).ConfigureAwait(false);

        return response;
    }

    public async Task<HttpResponseMessage> PutAsync<T>(string path, T ObjectToPut) where T : new()
    {
        HttpRequestMessage message = new()
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri($"{BaseUrl}/{path}"),
            Content = JsonContent.Create(ObjectToPut),
        };
        if (!string.IsNullOrEmpty(Token))
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        }

        HttpResponseMessage response = await client.SendAsync(message).ConfigureAwait(false);

        return response;
    }

    public async Task<HttpResponseMessage> PatchAsync<T>(string path, T ObjectToPatch)
    {
        HttpRequestMessage message = new()
        {
            Method = HttpMethod.Patch,
            RequestUri = new Uri($"{BaseUrl}/{path}"),
            Content = JsonContent.Create(ObjectToPatch),
        };

        if (!string.IsNullOrEmpty(Token))
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        }

        HttpResponseMessage response = await client.SendAsync(message).ConfigureAwait(false);

        return response;
    }

    public static async Task<HttpStatusCode> DeleteAsync(string path)
    {
        HttpResponseMessage response = await client.DeleteAsync(path).ConfigureAwait(false);
        return response.StatusCode;
    }
    #endregion
}