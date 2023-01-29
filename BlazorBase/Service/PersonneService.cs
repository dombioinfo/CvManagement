using System.Text.Json;
using BlazorBaseModel;
using BlazorBaseModel.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorBase.Service
{
    public class PersonneDtoService
    {
        private readonly IHttpClientFactory _clientFactory;

        public PersonneDtoService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        private List<PersonneDto> PersonneDtoList { get; set; } = new List<PersonneDto>();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<PersonneDto> GetForecastByIdAsync(int id)
        {
            var result = new PersonneDto();

            var url = $"https://api:7031/api/app/GetObject/PersonneDto/{id}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");

            HttpClient client = _clientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<PersonneDto>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }

            return result == null ? new PersonneDto() : result;
        }

        public async Task<PersonneDto[]> GetForecastAsync(DateTime startDate)
        {
            var result = new List<PersonneDto>();

            var url = $"http://api:7031/api/app/GetObjectList/PersonneDto";

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
                result = JsonSerializer.Deserialize<List<PersonneDto>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                result = Array.Empty<PersonneDto>().ToList();
            }

            return result == null ? Array.Empty<PersonneDto>() : result.ToArray();
        }
    }
}
