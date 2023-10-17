using System.Text.Json;
using BlazorBaseModel;
using Microsoft.AspNetCore.Components;
using AutoMapper;

namespace BlazorBase.Service
{
    public class ListeChoixService
    {
        private readonly ListeTypeService _listeTypeService;
        private readonly ListeItemService _listeItemService;

        public ListeChoixService(
            IHttpClientFactory clientFactory, IMapper mapper,
            ListeTypeService listeTypeService, ListeItemService listeItemService
            )
        {
            _listeTypeService = listeTypeService;
            _listeItemService = listeItemService;
        }

        public async Task<List<ListeChoixDto>> GetListeListeChoixAsync()
        {
            List<ListeItemDto> listeItemDtos = await _listeItemService.GetListeItemsAsync();
            List<ListeTypeDto> listeTypeDtos = await _listeTypeService.GetListeTypesAsync();
            List<ListeChoixDto> listeChoixListe = new();
            foreach (ListeTypeDto listeTypeDto in listeTypeDtos)
            {
                ListeChoixDto listeChoixDto = new ListeChoixDto()
                {
                    Entete = listeTypeDto,
                    ItemList = listeItemDtos.Where(x => x.ListeTypeDtoId == listeTypeDto.Id).ToDictionary(x => x.Code, x => x)
                };
            }
            return listeChoixListe;
        }

        public async Task<int> CreateListeChoixAsync(ListeChoixDto listeChoixDtoToCreate) 
        {
            ListeTypeDto entete = listeChoixDtoToCreate.Entete;
            Dictionary<string, ListeItemDto> listeItemList = listeChoixDtoToCreate.ItemList;

            return 0;
        }
    }
}
