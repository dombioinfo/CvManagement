using AutoMapper;
using BlazorBaseModel.Db;
using BlazorBaseModel.Model;

namespace BlazorBase.Service
{
    public class CandidatureService : GenericObjectService<Candidature>
    {
        public CandidatureService(
            IHttpClientFactory clientFactory, IMapper mapper
            ) : base(clientFactory, mapper) { }

        public async Task<List<CandidatureDto>> GetCandidaturesAsync()
        {
            List<Candidature> candidatures = (await this.GetGenericObjectListAsync()).ToList();
            List<CandidatureDto> candidatureDtos = new List<CandidatureDto>();
            foreach (Candidature candidature in candidatures)
            {
                CandidatureDto candidatureDto = _mapper.Map<CandidatureDto>(candidature);
                candidatureDtos.Add(candidatureDto);
            }
            return candidatureDtos;
        }

        public async Task<List<CandidatureDto>> GetCandidaturesByPersonneAsync(long personneId)
        {
            List<Candidature> candidatures = (await this.GetGenericObjectListAsync()).ToList(); // TODO: passer personneId en paramètre
            List<CandidatureDto> candidatureDtos = new List<CandidatureDto>();
            foreach (Candidature candidature in candidatures.Where(x => x.PersonneId == personneId).ToList())
            {
                CandidatureDto candidatureDto = _mapper.Map<CandidatureDto>(candidature);
                candidatureDtos.Add(candidatureDto);
            }
            return candidatureDtos;
        }

        public async Task<CandidatureDto> GetCandidatureAsync(int candidatureId)
        {
            Candidature candidature = await this.GetGenericObjectByIdAsync(candidatureId);
            CandidatureDto candidatureDto = new CandidatureDto();
            if (candidature != null)
            {
                candidatureDto = _mapper.Map<CandidatureDto>(candidature);
            }
            else
            {
                throw new Exception($"Il n'existe pas d'objet Candidature avec l'Id '{candidatureId}'");
            }
            return candidatureDto;
        }

        public async Task<int> CreateCandidatureAsync(CandidatureDto candidatureDto)
        {
            Candidature candidature = new Candidature()
            {
                Id = 0,
                DateCandidature = candidatureDto.DateCandidature,
                DateEntretien = candidatureDto.DateEntretien ?? default,
                DateEntree = candidatureDto.DateEntree ?? default,
                DateSortie = candidatureDto.DateSortie ?? default,
                ModaliteOrientation = candidatureDto.ModaliteOrientation,
                PrescripteurLibelle = candidatureDto.PrescripteurLibelle,
                PrescripteurNom = candidatureDto.PrescripteurNom,
                SourceCandidature = candidatureDto.SourceCandidature,
                MotifRefus = candidatureDto.MotifRefus,
                Annotation = candidatureDto.Annotation,
                PersonneId = candidatureDto.PersonneId,
                MetierId = candidatureDto.MetierId,
                StatutId = candidatureDto.StatutId,
                ResultatEntretienId = candidatureDto.ResultatEntretienId,
                Actif = true
            };
            return await this.CreateGenericObjectAsync("Candidature/create-with-data", candidature);
        }

        public async Task<int> UpdateCandidatureAsync(long candidatureId, CandidatureDto candidatureDto)
        {
            Candidature candidature = new Candidature()
            {
                Id = candidatureId,
                DateCandidature = candidatureDto.DateCandidature,
                DateEntretien = candidatureDto.DateEntretien ?? default,
                DateEntree = candidatureDto.DateEntree ?? default,
                DateSortie = candidatureDto.DateSortie ?? default,
                ModaliteOrientation = candidatureDto.ModaliteOrientation,
                PrescripteurLibelle = candidatureDto.PrescripteurLibelle,
                PrescripteurNom = candidatureDto.PrescripteurNom,
                SourceCandidature = candidatureDto.SourceCandidature,
                MotifRefus = candidatureDto.MotifRefus,
                Annotation = candidatureDto.Annotation,
                PersonneId = candidatureDto.PersonneId,
                MetierId = candidatureDto.MetierId,
                StatutId = candidatureDto.StatutId,
                ResultatEntretienId = candidatureDto.ResultatEntretienId
            };
            return await this.UpdateGenericObjectAsync(candidatureId, candidature);
        }

        public async Task<long> DeleteCandidatureAsync(long candidatureId)
        {
            return await this.DeleteGenericObjectAsync(candidatureId);
        }
    }
}
