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
            SqliteDbContext dbContext
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
            Cv.DateCreation = DateTime.Now;
            await _dbContext.AddAsync(Cv);
            await _dbContext.SaveChangesAsync();

            return Cv.Id;
        }

        [HttpPost("create-with-data", Name = "PostCvWithData")]
        public async Task<long> PostCvWithData(Cv CvRequest)
        {
            CvRequest.Id = 0;
            CvRequest.DateCreation = DateTime.Now;
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
            Cv.DateUpdate = DateTime.Now;
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

        [HttpGet("{id}/download")]
        public async Task<FileContentResult> DownloadCv(long id) 
        {
            Cv? cv = await _dbContext.Cvs
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (cv == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement Cv pour l'Id '{id}'");
            }

            return File(cv?.BlobFile, "application/octet-stream");
        }

    }
}
