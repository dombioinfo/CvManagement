using System.Text.Json;
using BlazorBaseModel;
using BlazorBaseModel.Model;
using BlazorBaseModel.Db;
using Microsoft.AspNetCore.Components;
using AutoMapper;

namespace BlazorBase.Service
{
    public class PersonneService : GenericObjectService<Personne>
    {
        private readonly AdresseService _adresseService;

        public PersonneService(
            IHttpClientFactory clientFactory, IMapper mapper,
            AdresseService adresseService
            ) : base(clientFactory, mapper)
        {
            _adresseService = adresseService;
        }
        public List<PersonneDto> PersonneDtoList { get; set; } = new List<PersonneDto>();

        public async Task<PersonneDto[]> GetPersonnesAsync()
        {
            Personne[] personnes = await this.GetGenericObjectListAsync();
            AdresseDto[] adresseDtos = await _adresseService.GetAdresseAsync();
            List<PersonneDto> personneDtos = new List<PersonneDto>();
            foreach (Personne personne in personnes)
            {
                PersonneDto personneDto = _mapper.Map<PersonneDto>(personne);
                AdresseDto? adresseDto = adresseDtos.FirstOrDefault(a => a.PersonneDto.Id == personneDto.Id);
                personneDto.AdresseDto = adresseDto != null ? adresseDto : new AdresseDto();
                personneDtos.Add(personneDto);
            }
            return personneDtos.ToArray();
        }
    }
}
