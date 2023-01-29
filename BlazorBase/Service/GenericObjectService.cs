using System.Text.Json;
using BlazorBaseModel;
using Microsoft.AspNetCore.Components;

namespace BlazorBase.Service
{
    public class GenericObjectService<T> where T : new()
    {
        private readonly IHttpClientFactory _clientFactory;

        public GenericObjectService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        private List<T> GenericObjectList { get; set; } = new List<T>();

        public async Task<T> GetGenericObjectByIdAsync(int id)
        {
            var result = new T();
            try
            {
                var url = $"http://api:7031/api/app/GetObjectList/{nameof(T)}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");

                HttpClient client = _clientFactory.CreateClient("HttpClientWithSSLUntrusted");

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    result = JsonSerializer.Deserialize<T>(stringResponse,
                        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Exception: {e.Message}");
                throw e;
            }
            return result;
        }

        public async Task<T[]> GetGenericObjectListAsync()
        {
            var result = new List<T>();
            
            var url = $"http://api:7031/api/app/GetObjectList/{nameof(T).Replace("Dto", "")}";
            Console.WriteLine($"URL : {url}");
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
                result = JsonSerializer.Deserialize<List<T>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                result = Array.Empty<T>().ToList();
            }

            return result == null ? Array.Empty<T>() : result.ToArray();
        }
    }
}
