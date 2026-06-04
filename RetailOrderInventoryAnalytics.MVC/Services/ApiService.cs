using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RetailOrderInventoryAnalytics.MVC.Services;

public class ApiService // this class is used to make API calls to the backend, it abstracts away the details of making HTTP requests and handling responses, making it easier for other parts of the application
                        // to interact with the API without worrying about the underlying implementation.
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        var response = await CreateClient().GetAsync(endpoint);
        return await ReadResponse<T>(response);
    }

    public async Task<bool> PostAsync<T>(string endpoint, T model)
    {
        var response = await CreateClient().PostAsync(endpoint, CreateJson(model));
        return response.IsSuccessStatusCode;
    }

    public async Task<TResult?> PostAsync<T, TResult>(string endpoint, T model)
    {
        var response = await CreateClient().PostAsync(endpoint, CreateJson(model));
        return await ReadResponse<TResult>(response);
    }
    // this method is used to send a PUT request to the specified endpoint with the provided model,
    // and it returns a boolean indicating whether the request was successful based on the HTTP status code of the response.
    public async Task<bool> PutAsync<T>(string endpoint, T model)
    {
        var response = await CreateClient().PutAsync(endpoint, CreateJson(model));
        return response.IsSuccessStatusCode;
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient("ApiClient");
        var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");

        if (!string.IsNullOrWhiteSpace(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return client;
    }

    private static StringContent CreateJson<T>(T model)
    {
        var json = JsonSerializer.Serialize(model, JsonOptions);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private static async Task<T?> ReadResponse<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        var content = await response.Content.ReadAsStringAsync();
        return string.IsNullOrWhiteSpace(content) ? default : JsonSerializer.Deserialize<T>(content, JsonOptions);
    }
}
