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

        public async Task<List<PersonneDto>> GetPersonnesAsync()
        {
            List<Personne> personnes = (await this.GetGenericObjectListAsync()).ToList();
            List<AdresseDto> adresseDtos = await _adresseService.GetAdressesAsync();
            List<PersonneDto> personneDtos = new List<PersonneDto>();
            foreach (Personne personne in personnes)
            {
                PersonneDto personneDto = _mapper.Map<PersonneDto>(personne);
                List<AdresseDto> adresseDtoFiltres = adresseDtos.Where(a => a.PersonneId == personneDto.Id).ToList();
                personneDto.AdresseDtos = adresseDtoFiltres != null ? adresseDtoFiltres : new List<AdresseDto>();
                personneDtos.Add(personneDto);
            }
            return personneDtos;
        }

        public async Task<PersonneDto> GetPersonneAsync(long personneId)
        {
            List<Personne> personnes = (await this.GetGenericObjectListAsync()).ToList();
            Personne? personne = personnes.Where(x => x.Id == personneId).FirstOrDefault();
            PersonneDto personneDto = new PersonneDto();
            if (personne != null)
            {
                personneDto = _mapper.Map<PersonneDto>(personne);
            }
            else
            {
                throw new Exception($"Il n'existe pas d'objet Personne avec l'Id '{personneId}'");
            }
            return personneDto;
        }

        public async Task<PersonneDto?> GetPersonneAsync(PersonneDto personneForSearch)
        {
            List<Personne> personnes = (await this.GetGenericObjectListAsync()).ToList();
            if (!string.IsNullOrWhiteSpace(personneForSearch.Email)) 
            {
                personnes = personnes.Where(x => x.Email == personneForSearch.Email).ToList();
            }
            if (!string.IsNullOrWhiteSpace(personneForSearch.Nom))
            {
                personnes = personnes.Where(x => x.Nom == personneForSearch.Nom).ToList();
            }
            if (!string.IsNullOrWhiteSpace(personneForSearch.Prenom))
            {
                personnes = personnes.Where(x => x.Prenom == personneForSearch.Prenom).ToList();
            }
            if (!string.IsNullOrWhiteSpace(personneForSearch.Tel))
            {
                personnes = personnes.Where(x => x.Tel == personneForSearch.Tel).ToList();
            }
            Personne? personne = personnes.FirstOrDefault();
            PersonneDto? personneDto = null;
            if (personne != null)
            {
                personneDto = _mapper.Map<PersonneDto>(personne);
            }
            return personneDto;
        }

        public async Task<int> CreatePersonneAsync(PersonneDto personneDto)
        {
            Personne personne = new Personne()
            {
                Id = 0,
                CiviliteId = personneDto.CiviliteId,
                Nom = personneDto.Nom,
                Prenom = personneDto.Prenom,
                DateNaissance = personneDto.DateNaissance,
                Email = personneDto.Email,
                Tel = personneDto.Tel,
                Actif = true
            };
            return await this.CreateGenericObjectAsync("Personne/create-with-data", personne);
        }

        public async Task<int> UpdatePersonneAsync(long personneId, PersonneDto personneDto)
        {
            Personne personne = new Personne()
            {
                Id = personneId,
                Nom = personneDto.Nom,
                Prenom = personneDto.Prenom,
                DateNaissance = personneDto.DateNaissance,
                Email = personneDto.Email,
                Tel = personneDto.Tel,
                CiviliteId = personneDto.CiviliteId
                //Adresses = null
            };
            return await this.UpdateGenericObjectAsync(personneId, personne);
        }

        public async Task<int> DeletePersonneAsync(long personneId)
        {
            return await this.DeleteGenericObjectAsync(personneId);
        }
    }
}
