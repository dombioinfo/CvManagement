using AutoMapper;
using BlazorBaseModel.Db;
using BlazorBaseModel.Model;

namespace BlazorBase.Service
{
    public class AdresseService : GenericObjectService<Adresse>
    {
        public AdresseService(
            IHttpClientFactory clientFactory, IMapper mapper
            ) : base(clientFactory, mapper) { }

        public async Task<List<AdresseDto>> GetAdressesAsync()
        {
            List<Adresse> adresses = (await this.GetGenericObjectListAsync()).ToList();
            List<AdresseDto> adresseDtos = new List<AdresseDto>();
            foreach (Adresse adresse in adresses)
            {
                AdresseDto adresseDto = _mapper.Map<AdresseDto>(adresse);
                adresseDtos.Add(adresseDto);
            }
            return adresseDtos;
        }

        public async Task<List<AdresseDto>> GetAdressesByPersonneAsync(long personneId)
        {
            List<Adresse> adresses = (await this.GetGenericObjectListAsync()).ToList(); // TODO: passer personneId en paramètre
            List<AdresseDto> adresseDtos = new List<AdresseDto>();
            foreach (Adresse adresse in adresses.Where(x => x.PersonneId == personneId).ToList())
            {
                AdresseDto adresseDto = _mapper.Map<AdresseDto>(adresse);
                adresseDtos.Add(adresseDto);
            }
            return adresseDtos;
        }

        public async Task<AdresseDto> GetAdresseAsync(int adresseId)
        {
            Adresse adresse= await this.GetGenericObjectByIdAsync(adresseId);
            AdresseDto adresseDto = new AdresseDto();
            if (adresse != null)
            {
                adresseDto = _mapper.Map<AdresseDto>(adresse);
            }
            else
            {
                throw new Exception($"Il n'existe pas d'objet Adresse avec l'Id '{adresseId}'");
            }
            return adresseDto;
        }

        public async Task<int> CreateAdresseAsync(AdresseDto adresseDto)
        {
            Adresse adresse = new Adresse()
            {
                Id = 0,
                Rue = adresseDto.Rue,
                Complement = adresseDto.Complement,
                Ville = adresseDto.Ville,
                CodePostal = adresseDto.CodePostal,
                PersonneId = adresseDto.PersonneId,
                Actif = true
            };
            return await this.CreateGenericObjectAsync("Adresse/create-with-data", adresse);
        }

        public async Task<int> UpdateAdresseAsync(long adresseId, AdresseDto adresseDto)
        {
            Adresse adresse = new Adresse()
            {
                Id = adresseId,
                Rue = adresseDto.Rue,
                Complement = adresseDto.Complement,
                Ville = adresseDto.Ville,
                CodePostal = adresseDto.CodePostal,
                PersonneId = 0
            };
            return await this.UpdateGenericObjectAsync(adresseId, adresse);
        }

        public async Task<long> DeleteAdresseAsync(long adresseId)
        {
            return await this.DeleteGenericObjectAsync(adresseId);
        }
    }
}
