using System.Text.Json;
using BlazorBaseModel;
using BlazorBaseModel.Model;
using BlazorBaseModel.Db;
using Microsoft.AspNetCore.Components;
using AutoMapper;

namespace BlazorBase.Service
{
    public class ListeTypeService : GenericObjectService<ListeType>
    {
        public ListeTypeService(
            IHttpClientFactory clientFactory, IMapper mapper
            ) : base(clientFactory, mapper)
        { }

        public async Task<List<ListeTypeDto>> GetListeTypesAsync()
        {
            List<ListeType> listeTypes = (await this.GetGenericObjectListAsync()).ToList();
            List<ListeTypeDto> listeTypeDtos = _mapper.Map<List<ListeTypeDto>>(listeTypes);
            return listeTypeDtos;
        }

        public async Task<ListeTypeDto> GetListeTypeAsync(long id)
        {
            List<ListeType> listeTypes = (await this.GetGenericObjectListAsync()).ToList();
            ListeType? listeType = listeTypes.Where(x => x.Id == id).FirstOrDefault();
            if (listeType == null)
            {
                throw new Exception($"Il n'existe pas d'objet ListeType avec l'Id '{id}'");
            }
            return _mapper.Map<ListeTypeDto>(listeType);
        }

        public async Task<ListeType?> GetListeTypeAsync(string code)
        {
            List<ListeType> listeTypes = (await this.GetGenericObjectListAsync()).ToList();
            if (!string.IsNullOrWhiteSpace(code)) 
            {
                listeTypes = listeTypes.Where(x => x.Code == code).ToList();
            }
            
            return listeTypes.FirstOrDefault();
        }

        public async Task<int> CreateListeTypeAsync(ListeTypeDto listeTypeToCreateDto)
        {
            ListeType listeType = new ListeType()
            {
                Id = 0,
                Code = listeTypeToCreateDto.Code,
                DefaultLibelle = listeTypeToCreateDto.DefaultLibelle,
                Actif = true,
                ListeItems = new List<ListeItem>()
            };
            return await this.CreateGenericObjectAsync("ListeType/create-with-data", listeType);
        }

        public async Task<int> UpdateListeTypeAsync(long listeTypeId, ListeType listeTypeToUpdate)
        {
            ListeType listeType = new ListeType()
            {
                Id = listeTypeId,
                Code = listeTypeToUpdate.Code,
                DefaultLibelle = listeTypeToUpdate.DefaultLibelle,
                Actif = listeTypeToUpdate.Actif
            };
            return await this.UpdateGenericObjectAsync(listeTypeId, listeType);
        }

        public async Task<int> DeleteListeTypeAsync(long listeTypeId)
        {
            return await this.DeleteGenericObjectAsync(listeTypeId);
        }
    }
}
