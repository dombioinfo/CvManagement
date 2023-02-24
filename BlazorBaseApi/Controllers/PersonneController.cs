using BlazorBaseModel.Model;
using BlazorBaseModel.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonneController : AppController
    {
        private readonly ILogger<PersonneController> _logger;

        public PersonneController(
            MysqlDbContext dbContext
            , ILogger<PersonneController> logger) : base(dbContext)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPersonnes")]
        public async Task<IEnumerable<Personne>> GetPersonnes()
        {
            List<Personne> personnes = new List<Personne>();
            personnes = await _dbContext.Personnes.ToListAsync();
            return personnes;
        }

        [HttpGet("{id}", Name = "GetPersonneById")]
        public async Task<Personne> GetPersonne(int id)
        {
            Personne? personne = await _dbContext.Personnes.FirstOrDefaultAsync(p => p.Id == id);
            return personne != null ? personne : new Personne();
        }

        [HttpPost(Name = "PostPersonne")]
        public async Task<long> CreatePersonne()
        {
            Personne personne = new Personne();
            await _dbContext.AddAsync(personne);
            await _dbContext.SaveChangesAsync();

            return personne.Id;
        }

        [HttpPost("create-with-data", Name = "PostPersonneWithData")]
        public async Task<long> CreatePersonneWithData(Personne personne)
        {
            await _dbContext.AddAsync(personne);
            await _dbContext.SaveChangesAsync();

            return personne.Id;
        }

        [HttpPut("{id}", Name = "PutPersonne")]
        public async Task UpdatePersonne(long id, Personne personneRequest)
        {
            Personne? personne = await _dbContext.Personnes
                .Where(p => p.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (personne == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement Personne pour l'Id '{id}'");
            }
            personne.DateNaissance = personneRequest.DateNaissance;
            personne.Nom = personneRequest.Nom;
            personne.Prenom = personneRequest.Prenom;
            personne.Email = personneRequest.Email;
            personne.Tel = personneRequest.Tel;
            await _dbContext.SaveChangesAsync();
        }

        [HttpDelete("{id}", Name = "DeletePersonne")]
        public async Task DeletePersonne(long id)
        {
            Personne? personne = await _dbContext.Personnes
                .Where(p => p.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (personne == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement Personne pour l'Id '{id}'");
            }
            _dbContext.Remove(personne);
            await _dbContext.SaveChangesAsync();
        }
    }
}
