using System.Text.Json;
using AutoMapper;
using BlazorBaseModel;
using BlazorBaseModel.Db;
using BlazorBaseModel.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorBase.Service
{
    public class CandidatureService : GenericObjectService<Candidature>
    {
        public CandidatureService(
            IHttpClientFactory clientFactory, IMapper mapper
            ) : base(clientFactory, mapper)
        { }
        public List<CandidatureDto> CandidatureDtoList { get; set; } = new List<CandidatureDto>();

        public async Task<CandidatureDto[]> GetCandidaturesAsync()
        {
            Candidature[] candidatures = await this.GetGenericObjectListAsync();
            List<CandidatureDto> candidatureDtos = new List<CandidatureDto>();
            foreach (Candidature candidature in candidatures)
            {
                CandidatureDto candidatureDto = _mapper.Map<CandidatureDto>(candidature);
                
                candidatureDtos.Add(candidatureDto);
            }
            return candidatureDtos.ToArray();
        }
    }
}
