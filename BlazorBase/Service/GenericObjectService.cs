using System.Net.Http.Headers;
using System.Text.Json;
using AutoMapper;

namespace BlazorBase.Service
{
    public class GenericObjectService<T> where T : new()
    {
        // protected string baseUrl = "https://api:7031/api";
        protected readonly HttpClient _httpClient;
        protected readonly IMapper _mapper;
        public GenericObjectService(IHttpClientFactory client, IMapper mapper)
        {
            _httpClient = client.CreateClient("HttpClientWithSSLUntrusted");
            _mapper = mapper;
        }

        protected async Task<T> GetGenericObjectByIdAsync(int id)
        {
            var result = new T();
            try
            {
                string typeOfObject = typeof(T).Name;
                // var url = $"{baseUrl}/{typeOfObject}";
                var url = $"{typeOfObject}";
                var response = await _httpClient.GetAsync(url);

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

            string typeOfObject = typeof(T).Name;
            var url = $"/{typeOfObject}";
            Console.WriteLine($"URL : {url}");

            var response = await _httpClient.GetAsync(url);

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

        protected async Task<int> CreateGenericObjectAsync(T objectToCreate) {
            string typeOfObject = typeof(T).Name;
            var url = $"{typeOfObject}";
            return await this.CreateGenericObjectAsync(objectToCreate);
        } 

        protected async Task<int> CreateGenericObjectAsync(string url, T objectToCreate)
        {
            int result = 0;
            Console.WriteLine($"URL : {url}");
            
            var response = await _httpClient.PostAsJsonAsync(url, objectToCreate);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(stringResponse))
                {
                    throw new Exception("La réponse ne doit pas être un objet vide");
                }
                result = JsonSerializer.Deserialize<int>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                result = 0;
            }

            return result;
        }

        protected async Task<int> UpdateGenericObjectAsync(long id, T objectToUpdate) {
            string typeOfObject = typeof(T).Name;
            string url = $"{typeOfObject}/{id}";
            return await this.UpdateGenericObjectAsync(url, objectToUpdate);
        }

        protected async Task<int> UpdateGenericObjectAsync(string url, T objectToUpdate)
        {
            int result = 0;
            Console.WriteLine($"URL : {url}");

            var response = await _httpClient.PutAsJsonAsync(url, objectToUpdate);

            if (response.IsSuccessStatusCode)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }

            return result;
        }

        protected async Task<int> DeleteGenericObjectAsync(long id) {
            string typeOfObject = typeof(T).Name;
            string url = $"{typeOfObject}/{id}";
            return await this.DeleteGenericObjectAsync(url);
        }

        protected async Task<int> DeleteGenericObjectAsync(string url)
        {
            int result = 0;
            Console.WriteLine($"URL : {url}");

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                /*
                var stringResponse = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(stringResponse))
                {
                    throw new Exception("La réponse ne doit pas être un objet vide");
                }
                result = JsonSerializer.Deserialize<int>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                */
                result = 1;
            }
            else
            {
                result = 0;
            }

            return result;
        }
    }
}
