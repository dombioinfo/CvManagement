using System.Text.Json;
using BlazorBaseModel;
using BlazorBaseModel.Db;
using Microsoft.AspNetCore.Components;

namespace BlazorBase.Service
{
    public class WeatherForecastService
    {
        private readonly IHttpClientFactory _clientFactory;

        public WeatherForecastService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        private List<WeatherForecast> WeatherForecastList { get; set; } = new List<WeatherForecast>();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<WeatherForecast> GetForecastByIdAsync(int id)
        {
            var result = new WeatherForecast();

            var url = $"https://172.26.0.2:7031/api/app/GetObject/WeatherForecast/{id}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");

            HttpClient client = _clientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<WeatherForecast>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }

            return result == null ? new WeatherForecast() : result;
        }

        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var result = new List<WeatherForecast>();

            var url = $"http://api:7031/api/app/GetObjectList/WeatherForecast";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");

            HttpClient client = _clientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(stringResponse))
                {
                    throw new Exception("La réponse ne doit pas être un objet vide");
                }
                result = JsonSerializer.Deserialize<List<WeatherForecast>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                result = Array.Empty<WeatherForecast>().ToList();
            }

            return result == null ? Array.Empty<WeatherForecast>() : result.ToArray();
        }
    }
}
