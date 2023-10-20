using BlazorBaseModel.Model;
using BlazorBaseModel.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListeTypeController : AppController
    {
        private readonly ILogger<ListeTypeController> _logger;

        public ListeTypeController(
            MysqlDbContext dbContext
            , ILogger<ListeTypeController> logger) : base(dbContext)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetListeType")]
        public async Task<IEnumerable<ListeType>> GetListeType()
        {
            List<ListeType> listeTypes = new List<ListeType>();
            listeTypes = await _dbContext.ListeTypes.ToListAsync();
            return listeTypes;
        }

        [HttpGet("{id}", Name = "GetListeTypeById")]
        public async Task<ListeType> GetListeType(int id)
        {
            ListeType? listeType = await _dbContext.ListeTypes.FirstOrDefaultAsync(p => p.Id == id);
            return listeType != null ? listeType : new ListeType();
        }

        [HttpGet("{listeTypeCode}/listeitems", Name = "GetListeItemByListeTypeCode")]
        public async Task<ListeType?> GetListeItemsByListeTypeCode(string listeTypeCode)
        {
            ListeType? listeType = await _dbContext.ListeTypes
                .Include(x => x.ListeItems)
                .Where(p => p.Code == listeTypeCode)
                .FirstOrDefaultAsync();
            return listeType;
        }

        [HttpPost(Name = "PostListeType")]
        public async Task<long> CreateListeType()
        {
            ListeType personne = new ListeType();
            await _dbContext.AddAsync(personne);
            await _dbContext.SaveChangesAsync();

            return personne.Id;
        }

        [HttpPost("create-with-data", Name = "PostListeTypeWithData")]
        public async Task<long> CreateListeTypeWithData(ListeType listeType)
        {
            await _dbContext.AddAsync(listeType);
            await _dbContext.SaveChangesAsync();

            return listeType.Id;
        }

        [HttpPut("{id}", Name = "PutListeType")]
        public async Task UpdateListeType(long id, ListeType listeTypeRequest)
        {
            ListeType? listeType = await _dbContext.ListeTypes
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (listeType == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement ListeType pour l'Id '{id}'");
            }
            listeType.Code = listeTypeRequest.Code;
            listeType.DefaultLibelle = listeTypeRequest.DefaultLibelle;
            listeType.Actif = listeTypeRequest.Actif;
            await _dbContext.SaveChangesAsync();
        }

        [HttpDelete("{id}", Name = "DeleteListeType")]
        public async Task DeleteListeType(long id)
        {
            ListeType? listeType = await _dbContext.ListeTypes
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (listeType == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement ListeType pour l'Id '{id}'");
            }
            _dbContext.Remove(listeType);
            await _dbContext.SaveChangesAsync();
        }
    }
}
