using GoPress.Mvc.Configurations;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;

namespace GoPress.Mvc.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private readonly ITokenService _tokenService;
        public ApiService(HttpClient httpClient, IOptions<ApiSettings> apiSettings, ITokenService tokenService  )
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
            _httpClient.BaseAddress =
                new Uri(_apiSettings.BaseUrl);
            _tokenService = tokenService;
        }

        //get
        public async Task<T> GetAsync<T>(string url)
        {
            AddBearerToken();
            var response =
                await _httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return default;
            }

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return default;
            }

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }


          //  response.EnsureSuccessStatusCode();

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
            AddBearerToken();

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

        private void AddBearerToken()
        {
            var token = _tokenService.GetToken();

            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer",
                        token);
            }
        }
    }
}
