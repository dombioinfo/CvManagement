using System.Text.Json;
using BlazorBaseModel;
using BlazorBaseModel.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorBase.Service
{
    public class CandidatureDtoService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CandidatureDtoService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        private List<CandidatureDto> CandidatureDtoList { get; set; } = new List<CandidatureDto>();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<CandidatureDto> GetForecastByIdAsync(int id)
        {
            var result = new CandidatureDto();

            var url = $"https://172.26.0.2:7031/api/app/GetObject/CandidatureDto/{id}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");

            HttpClient client = _clientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<CandidatureDto>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }

            return result == null ? new CandidatureDto() : result;
        }

        public async Task<CandidatureDto[]> GetForecastAsync(DateTime startDate)
        {
            var result = new List<CandidatureDto>();

            var url = $"http://api:7031/api/app/GetObjectList/CandidatureDto";

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
                result = JsonSerializer.Deserialize<List<CandidatureDto>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                result = Array.Empty<CandidatureDto>().ToList();
            }

            return result == null ? Array.Empty<CandidatureDto>() : result.ToArray();
        }
    }
}
