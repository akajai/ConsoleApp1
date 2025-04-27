using System.Net;
using System.Text.Json;

namespace ClassLibrary1.Service;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

public class ExternalUserService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    
    private readonly ILogger<ExternalUserService> _logger;

    private const string BaseUrl = "https://reqres.in/api"; // Can inject via options

    
    public ExternalUserService(HttpClient httpClient, IMemoryCache cache, ILogger<ExternalUserService> logger)
    {
        _httpClient = httpClient;
        _cache = cache;
        _logger = logger;
        // Add the x-api-key header to all requests
        _httpClient.DefaultRequestHeaders.Add("x-api-key", "reqres-free-v1");

    }

   

    public async Task<RootObjectUser?> GetUserByIdAsync(int userId)
    {
        var cacheKey = $"user_{userId}";
    
        if (_cache.TryGetValue(cacheKey, out RootObjectUser user))
        {
            _logger.LogInformation("Cache hit for user {UserId}", userId);
            return user;
        }
    
        var response = await _httpClient.GetAsync($"{BaseUrl}/users/{userId}");
    
        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;
    
        response.EnsureSuccessStatusCode();
    
        var json = await response.Content.ReadAsStringAsync();
        var wrapper = JsonSerializer.Deserialize<RootObjectUser>(json);
    
        if (wrapper!= null)
        {
            _cache.Set(cacheKey, wrapper, TimeSpan.FromSeconds(60));
        }
    
        return wrapper;
    }

    public async Task<IEnumerable<Data>> GetAllUsersAsync()
    {
        const string cacheKey = "all_users";

        if (_cache.TryGetValue(cacheKey, out List<Data> cachedUsers))
        {
            _logger.LogInformation("Cache hit for all users");
            return cachedUsers;
        }

        var users = new List<Data>();
        int page = 1;
        int totalPages;

        do
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/users?page={page}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<RootObject>(json);

            if (result?.data != null)
                users.AddRange(result.data);

            totalPages = result?.TotalPages ?? 0;
            page++;

        } while (page <= totalPages);

        _cache.Set(cacheKey, users, TimeSpan.FromMinutes(5));
        return users;
    }
}