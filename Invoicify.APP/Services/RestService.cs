using System.Net.Http.Json;

namespace Invoicify.APP.Services
{
    public class RestService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private readonly string _baseUrl;

        public RestService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _baseUrl = _configuration.GetValue<string>("API");
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(_baseUrl + url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<bool> GetAsync(string url)
        {
            var response = await _httpClient.GetAsync(_baseUrl + url);
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PostAsync<T>(string url, T data)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + url, data);
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PutAsync<T>(string url, T? data)
        {
            var response = await _httpClient.PutAsJsonAsync(_baseUrl + url, data);
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
    }
}
