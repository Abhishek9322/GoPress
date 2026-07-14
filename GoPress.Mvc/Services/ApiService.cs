using GoPress.Mvc.Configurations;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace GoPress.Mvc.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        public ApiService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
            _httpClient.BaseAddress =
                new Uri(_apiSettings.BaseUrl);
        }

        //get
        public async Task<T> GetAsync<T>(string url)
        {
            var response =
                await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var content =
                await response.Content
                    .ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(
                content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }


        public async Task<TResponse>PostAsync<TRequest, TResponse>(
                string url,
                TRequest data)
        {
            var json =
                JsonSerializer.Serialize(data);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            var response =
                await _httpClient
                    .PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var responseContent =
                await response.Content
                    .ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(
                responseContent,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }


        //add jwt if neede here 
    }
}
