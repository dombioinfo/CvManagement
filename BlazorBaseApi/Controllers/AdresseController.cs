using BlazorBaseModel;
using BlazorBaseModel.Db;
using BlazorBaseModel.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdressesController : AppController
    {
        private readonly ILogger<AdressesController> _logger;

        public AdressesController(
            MysqlDbContext dbContext
            , ILogger<AdressesController> logger) : base(dbContext)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<Adresse>> GetAdresses()
        {
            List<Adresse> adresses = new List<Adresse>();
            adresses = await _dbContext.Adresses.ToListAsync();
            return adresses;
        }

        [HttpGet("{id}")]
        public async Task<Adresse> GetAdresse(int id)
        {
            Adresse? adresse = await _dbContext.Adresses.FirstOrDefaultAsync(x => x.Id == id);
            return adresse != null ? adresse : new Adresse();
        }

        [HttpPost()]
        public async Task<long> CreateAdresse(Adresse adresseRequest)
        {
            adresseRequest.Id = 0;
            await _dbContext.AddAsync(adresseRequest);
            await _dbContext.SaveChangesAsync();

            return adresseRequest.Id;
        }

        [HttpPut("{id}")]
        public async Task UpdateAdresse(int id, Adresse adresseRequest)
        {
            Adresse? adresse = await _dbContext.Adresses
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (adresse == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement Adresse pour l'Id '{id}'");
            }
            adresse.Rue = adresseRequest.Rue;
            adresse.Complement = adresseRequest.Complement;
            adresse.Ville = adresseRequest.Ville;
            adresse.CodePostal = adresseRequest.CodePostal;
            adresse.PersonneId = adresseRequest.PersonneId;
            await _dbContext.AddAsync(adresse);
            await _dbContext.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteAdresse(int id)
        {
            Adresse? adresse = await _dbContext.Adresses
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (adresse == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement Adresse pour l'Id '{id}'");
            }
            _dbContext.Remove(adresse);
            await _dbContext.SaveChangesAsync();
        }
    }
}
