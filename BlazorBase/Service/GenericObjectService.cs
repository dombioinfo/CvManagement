using System.Text.Json;
using AutoMapper;

namespace BlazorBase.Service
{
    public class GenericObjectService<T> where T : new()
    {
        protected readonly IHttpClientFactory _clientFactory;
        protected readonly IMapper _mapper;
        public GenericObjectService(IHttpClientFactory clientFactory, IMapper mapper)
        {
            _clientFactory = clientFactory;
            _mapper = mapper;
        }

        protected async Task<T> GetGenericObjectByIdAsync(int id)
        {
            var result = new T();
            try
            {
                string typeOfObject = typeof(T).Name;//.Replace("Dto", "");
                // var url = $"https://api:7031/api/app/GetObjectList/{typeOfObject}";
                var url = $"https://api:7031/api/{typeOfObject}";

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
            return (result == null) ? new T() : result;
        }

        protected async Task<T[]> GetGenericObjectListAsync()
        {
            var result = new List<T>();

            string typeOfObject = typeof(T).Name;//.Replace("Dto", "");
            // var url = $"https://api:7031/api/app/GetObjectList/{typeOfObject}";
            var url = $"https://api:7031/api/{typeOfObject}";
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
