namespace BlazorBase.Service
{
    public class CvService : GenericObjectService<Cv>
    {
        public CvService(
            IHttpClientFactory clientFactory, IMapper mapper
            ) : base(clientFactory, mapper) { }

        public async Task<List<CvDto>> GetCvsAsync()
        {
            List<Cv> Cvs = (await this.GetGenericObjectListAsync()).ToList();
            List<CvDto> CvDtos = new List<CvDto>();
            foreach (Cv Cv in Cvs)
            {
                CvDto CvDto = _mapper.Map<CvDto>(Cv);
                CvDtos.Add(CvDto);
            }
            return CvDtos;
        }

        public async Task<List<CvDto>> GetCvsByPersonneAsync(long personneId)
        {
            List<Cv> Cvs = (await this.GetGenericObjectListAsync()).ToList(); // TODO: passer personneId en paramètre
            List<CvDto> CvDtos = new List<CvDto>();
            foreach (Cv Cv in Cvs.Where(x => x.PersonneId == personneId).ToList())
            {
                CvDto CvDto = _mapper.Map<CvDto>(Cv);
                CvDtos.Add(CvDto);
            }
            return CvDtos;
        }

        public async Task<CvDto> GetCvAsync(int CvId)
        {
            Cv Cv = await this.GetGenericObjectByIdAsync(CvId);
            CvDto CvDto = new CvDto();
            if (Cv != null)
            {
                CvDto = _mapper.Map<CvDto>(Cv);
            }
            else
            {
                throw new Exception($"Il n'existe pas d'objet Cv avec l'Id '{CvId}'");
            }
            return CvDto;
        }

        public async Task<int> CreateCvAsync(CvDto CvDto)
        {
            Cv Cv = new Cv()
            {
                Id = 0,
                FileName = CvDto.FileName,
                RelativePath = CvDto.RelativePath,
                FileSize = CvDto.FileSize,
                BlobFile = CvDto.BlobFile,
                PersonneId = CvDto.PersonneId
            };
            return await this.CreateGenericObjectAsync("Cv/create-with-data", Cv);
        }

        public async Task<int> UpdateCvAsync(long CvId, CvDto CvDto)
        {
            Cv Cv = new Cv()
            {
                Id = CvId,
                FileName = CvDto.FileName,
                RelativePath = CvDto.RelativePath,
                FileSize = CvDto.FileSize,
                BlobFile = CvDto.BlobFile,
                PersonneId = CvDto.PersonneId
            };
            return await this.UpdateGenericObjectAsync(CvId, Cv);
        }

        public async Task<long> DeleteCvAsync(long CvId)
        {
            return await this.DeleteGenericObjectAsync(CvId);
        }
    }
}
