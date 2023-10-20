using BlazorBaseModel;
using BlazorBaseModel.Db;
using BlazorBaseModel.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CvController : AppController
    {
        private readonly ILogger<CvController> _logger;

        public CvController(
            MysqlDbContext dbContext
            , ILogger<CvController> logger) : base(dbContext)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<Cv>> GetCvs()
        {
            List<Cv> Cvs = new List<Cv>();
            Cvs = await _dbContext.Cvs.ToListAsync();
            return Cvs;
        }

        [HttpGet("{id}")]
        public async Task<Cv> GetCv(long id)
        {
            Cv? Cv = await _dbContext.Cvs.FirstOrDefaultAsync(x => x.Id == id);
            return Cv != null ? Cv : new Cv();
        }

        [HttpPost()]
        public async Task<long> CreateCv()
        {
            Cv Cv = new Cv();
            await _dbContext.AddAsync(Cv);
            await _dbContext.SaveChangesAsync();

            return Cv.Id;
        }

        [HttpPost("create-with-data", Name = "PostCvWithData")]
        public async Task<long> PostCvWithData(Cv CvRequest)
        {
            CvRequest.Id = 0;
            await _dbContext.AddAsync(CvRequest);
            await _dbContext.SaveChangesAsync();

            return CvRequest.Id;
        }

/*
        [HttpPut("{id}")]
        public async Task UpdateCv(long id, Cv CvRequest)
        {
            Cv? Cv = await _dbContext.Cvs
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (Cv == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement Cv pour l'Id '{id}'");
            }
            Cv.BlobFile = CvRequest.BlobFile;
            Cv.FileName = CvRequest.FileName;
            Cv.RelativePath = CvRequest.RelativePath;
            Cv.FileSize = CvRequest.FileSize;
            Cv.PersonneId = CvRequest.PersonneId;
            await _dbContext.SaveChangesAsync();
        }
*/
        [HttpDelete("{id}")]
        public async Task DeleteCv(long id)
        {
            Cv? Cv = await _dbContext.Cvs
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (Cv == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement Cv pour l'Id '{id}'");
            }
            _dbContext.Remove(Cv);
            await _dbContext.SaveChangesAsync();
        }
    }
}
