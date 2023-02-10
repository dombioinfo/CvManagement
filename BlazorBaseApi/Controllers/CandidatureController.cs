using AutoMapper;
using BlazorBaseModel;
using BlazorBaseModel.Db;
using BlazorBaseModel.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidatureController : AppController
    {
        private readonly ILogger<CandidatureController> _logger;

        public CandidatureController(
            MysqlDbContext dbContext
            , ILogger<CandidatureController> logger) : base(dbContext)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<Candidature>> GetCandidatures()
        {
            List<Candidature> candidatures = new List<Candidature>();
            candidatures = await _dbContext.Candidatures.ToListAsync();
            return candidatures;
        }

        [HttpGet("{id}")]
        public async Task<Candidature> GetCandidature(int id)
        {
            Candidature? candidature = await _dbContext.Candidatures.FirstOrDefaultAsync(x => x.Id == id);
            return candidature != null ? candidature : new Candidature();
        }

        [HttpPost()]
        public async Task<long> CreateCandidature()
        {
            Candidature candidature = new Candidature();
            await _dbContext.AddAsync(candidature);
            await _dbContext.SaveChangesAsync();

            return candidature.Id;
        }

        [HttpPut("{id}")]
        public async Task UpdateCandidature(int id, Candidature candidatueRequest)
        {
            Candidature? candidature = await _dbContext.Candidatures
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (candidature == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement Candidature pour l'Id '{id}'");
            }
            candidature.DateCandidature = candidatueRequest.DateCandidature;
            candidature.PersonneId = candidatueRequest.PersonneId;
            candidature.Annotation = candidatueRequest.Annotation;
            await _dbContext.AddAsync(candidature);
            await _dbContext.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteCandidature(int id)
        {
            Candidature? candidature = await _dbContext.Candidatures
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (candidature == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement Candidature pour l'Id '{id}'");
            }
            _dbContext.Remove(candidature);
            await _dbContext.SaveChangesAsync();
        }
    }
}
