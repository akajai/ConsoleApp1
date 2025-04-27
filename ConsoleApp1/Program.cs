// See https://aka.ms/new-console-template for more information

using ClassLibrary1;
using ClassLibrary1.Service;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

HttpClient _httpClient= new HttpClient();
IMemoryCache _cache= new MemoryCache(new MemoryCacheOptions());
    
//ILogger _logger= new Logger();
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory) // Important: base directory
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Bind the "USL" section
var urlConfig = new URLConfig();
config.GetSection("URL").Bind(urlConfig);

// Use the config values
Console.WriteLine($"ApiUrl: {urlConfig.ApiUrl}");
Console.WriteLine($"ApiKey: {urlConfig.ApiKey}");
Console.WriteLine($"RetryCount: {urlConfig.RetryCount}");
ExternalUserService externalUserService = new ExternalUserService(_httpClient,_cache,null,urlConfig);
var users=await externalUserService.GetAllUsersAsync();
var user = await externalUserService.GetUserByIdAsync(1);

Console.WriteLine("Hello, World!");
