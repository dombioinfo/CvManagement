using BlazorBaseModel.Model;
using BlazorBaseModel.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListeItemController : AppController
    {
        private readonly ILogger<ListeItemController> _logger;

        public ListeItemController(
            SqliteDbContext dbContext
            , ILogger<ListeItemController> logger) : base(dbContext)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetListeItem")]
        public async Task<IEnumerable<ListeItem>> GetListeItem()
        {
            List<ListeItem> listeItems = new List<ListeItem>();
            listeItems = await _dbContext.ListeItems.ToListAsync();
            return listeItems;
        }

        [HttpGet("{id}", Name = "GetListeItemById")]
        public async Task<ListeItem> GetListeItem(int id)
        {
            ListeItem? listeType = await _dbContext.ListeItems.FirstOrDefaultAsync(p => p.Id == id);
            return listeType != null ? listeType : new ListeItem();
        }

        [HttpPost(Name = "PostListeItem")]
        public async Task<long> CreateListeItem()
        {
            ListeItem listeItem = new ListeItem();
            listeItem.DateCreation = DateTime.Now;
            await _dbContext.AddAsync(listeItem);
            await _dbContext.SaveChangesAsync();

            return listeItem.Id;
        }

        [HttpPost("create-with-data", Name = "PostListeItemWithData")]
        public async Task<long> CreateListeItemWithData(ListeItem listeItem)
        {
            listeItem.DateCreation = DateTime.Now;
            await _dbContext.AddAsync(listeItem);
            await _dbContext.SaveChangesAsync();

            return listeItem.Id;
        }

        [HttpPut("{id}", Name = "PutListeItem")]
        public async Task UpdateListeItem(long id, ListeItem listeItemRequest)
        {
            ListeItem? listeItem = await _dbContext.ListeItems
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (listeItem == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement ListeItem pour l'Id '{id}'");
            }
            listeItem.Code = listeItemRequest.Code;
            listeItem.DefaultLibelle = listeItemRequest.DefaultLibelle;
            //listeItem.ListeTypeId = listeItemRequest.ListeTypeId;
            listeItem.Actif = listeItemRequest.Actif;
            listeItem.DateUpdate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
        }

        [HttpDelete("{id}", Name = "DeleteListeItem")]
        public async Task DeleteListeItem(long id)
        {
            ListeItem? listeItem = await _dbContext.ListeItems
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (listeItem == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement ListeItem pour l'Id '{id}'");
            }
            _dbContext.Remove(listeItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
