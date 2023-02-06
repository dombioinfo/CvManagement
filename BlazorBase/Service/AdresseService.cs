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

        public List<AdresseDto> AdresseDtoList { get; set; } = new List<AdresseDto>();

        public async Task<AdresseDto[]> GetAdresseAsync()
        {
            Adresse[] adresses = await this.GetGenericObjectListAsync();
            List<AdresseDto> adresseDtos = new List<AdresseDto>();
            foreach (Adresse adresse in adresses)
            {
                AdresseDto adresseDto = _mapper.Map<AdresseDto>(adresse);
                adresseDtos.Add(adresseDto);
            }
            return adresseDtos.ToArray();
        }
    }
}
