using System.Text.Json;
using BlazorBaseModel;
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

        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            // WeatherForecastList = await this._clientFactory.GetFromJsonAsync<List<WeatherForecast>>("/api/weatherforecast/getweatherlist");
            // return WeatherForecastList;

            // return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            // {
            //     Date = startDate.AddDays(index),
            //     TemperatureC = Random.Shared.Next(-20, 55),
            //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            // }).ToArray());

            var result = new List<WeatherForecast>();

            var url = $"https://localhost:7031/api/weatherforecast/getweatherlist";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

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
