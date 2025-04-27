// See https://aka.ms/new-console-template for more information

using ClassLibrary1.Service;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

HttpClient _httpClient= new HttpClient();
IMemoryCache _cache= new MemoryCache(new MemoryCacheOptions());
    
//ILogger _logger= new Logger();
ExternalUserService externalUserService = new ExternalUserService(_httpClient,_cache,null);
var users=await externalUserService.GetAllUsersAsync();
var user = await externalUserService.GetUserByIdAsync(1);

Console.WriteLine("Hello, World!");
