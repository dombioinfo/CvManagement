using System.Text.Json;
using BlazorBaseModel;
using BlazorBaseModel.Model;
using BlazorBaseModel.Db;
using Microsoft.AspNetCore.Components;
using AutoMapper;

namespace BlazorBase.Service
{
    public class ListeItemService : GenericObjectService<ListeItem>
    {
        private readonly ListeTypeService _listeTypeService;

        public ListeItemService(
            IHttpClientFactory clientFactory, IMapper mapper,
            ListeTypeService listeTypeService
            ) : base(clientFactory, mapper)
        {
            _listeTypeService = listeTypeService;
        }

        public async Task<List<ListeItemDto>> GetListeItemsAsync()
        {
            List<ListeItem> listeItems = (await this.GetGenericObjectListAsync()).ToList();
            List<ListeItemDto> listeItemDtos = _mapper.Map<List<ListeItemDto>>(listeItems);
            return listeItemDtos;
        }

        public async Task<ListeItem> GetListeItemAsync(long id)
        {
            List<ListeItem> listeItems = (await this.GetGenericObjectListAsync()).ToList();
            ListeItem? listeItem = listeItems.Where(x => x.Id == id).FirstOrDefault();
            
            if (listeItem == null)
            {
                throw new Exception($"Il n'existe pas d'objet ListeItem avec l'Id '{id}'");
            }
            return listeItem;
        }

        public async Task<ListeItem?> GetListeItemAsync(string code)
        {
            List<ListeItem> listeItems = (await this.GetGenericObjectListAsync()).ToList();
            if (!string.IsNullOrWhiteSpace(code)) 
            {
                listeItems = listeItems.Where(x => x.Code == code).ToList();
            }
            
            ListeItem? listeItem = listeItems.FirstOrDefault();

            return listeItem;
        }

        public async Task<List<ListeItemDto>> GetListeItemsByListeTypeAsync(long listeTypeId)
        {
            List<ListeItem> listeItems = (await this.GetGenericObjectListAsync()).ToList();
            List<ListeItemDto> listeItemDtos = _mapper.Map<List<ListeItemDto>>(listeItems.Where(x => x.ListeTypeId == listeTypeId).ToList());
            return listeItemDtos;
        }

        public async Task<List<ListeItemDto>> GetListeItemsByListeTypeAsync(string listeTypeCode)
        {
            //List<ListeItem> listeItems = (await this.GetGenericObjectListAsync()).ToList();
            //List<ListeItemDto> listeItemDtos = _mapper.Map<List<ListeItemDto>>(listeItems.Where(x => x.ListeType!.Code == listeTypeCode).ToList());

            ListeType listeType = default!;

            string typeOfObject = typeof(ListeItem).Name;
            var url = $"/listetype/{listeTypeCode}/{typeOfObject}s";
            Console.WriteLine($"URL : {url}");

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(stringResponse))
                {
                    throw new Exception("La réponse ne doit pas être un objet vide");
                }
                listeType = JsonSerializer.Deserialize<ListeType>(stringResponse,
                    new JsonSerializerOptions()
                    {
                        //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
            }
            else
            {
                listeType = new ListeType();
            }

            List<ListeItemDto> listeItemDtos = _mapper.Map<List<ListeItemDto>>(listeType!.ListeItems);
            
            return listeItemDtos;
        }

        public async Task<int> CreateListeItemAsync(ListeItemDto listeItemToCreate)
        {
            ListeItem listeItem = new ListeItem()
            {
                Id = 0,
                Code = listeItemToCreate.Code,
                ListeTypeId = listeItemToCreate.ListeTypeDtoId,
                DefaultLibelle = listeItemToCreate.DefaultLibelle
            };
            return await this.CreateGenericObjectAsync("listeitem/create-with-data", listeItem);
        }

        public async Task<int> UpdateListeItemAsync(long listeItemId, ListeItemDto listeItemToUpdate)
        {
            ListeItem listeItem = new ListeItem()
            {
                Id = listeItemId,
                Code = listeItemToUpdate.Code,
                ListeTypeId = listeItemToUpdate.ListeTypeDtoId,
                DefaultLibelle = listeItemToUpdate.DefaultLibelle,
                Actif = listeItemToUpdate.Actif
            };
            return await this.UpdateGenericObjectAsync(listeItemId, listeItem);
        }

        public async Task<int> DeleteListeItemAsync(long listeItemId)
        {
            return await this.DeleteGenericObjectAsync(listeItemId);
        }
    }
}
