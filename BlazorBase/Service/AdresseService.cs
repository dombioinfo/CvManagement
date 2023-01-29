using System.Text.Json;
using BlazorBaseModel;
using BlazorBaseModel.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorBase.Service
{
    public class AdresseService
    {
        private readonly IHttpClientFactory _clientFactory;

        public AdresseService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        private List<AdresseDto> List { get; set; } = new List<AdresseDto>();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<AdresseDto> GetAdresseDtoByIdAsync(int id)
        {
            var result = new AdresseDto();

            var url = $"https://api:7031/api/app/GetObject/AdresseDto/{id}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");

            HttpClient client = _clientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<AdresseDto>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }

            return result == null ? new AdresseDto() : result;
        }

        public async Task<AdresseDto[]> GetAdresseDtoAsync()
        {
            var result = new List<AdresseDto>();

            var url = $"http://api:7031/api/app/GetObjectList/AdresseDto";

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
                result = JsonSerializer.Deserialize<List<AdresseDto>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                result = Array.Empty<AdresseDto>().ToList();
            }

            return result == null ? Array.Empty<AdresseDto>() : result.ToArray();
        }
    }
}
