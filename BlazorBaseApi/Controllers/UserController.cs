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
    public class UserController : AppController
    {
        private readonly ILogger<UserController> _logger;

        public UserController(
            MysqlDbContext dbContext
            , ILogger<UserController> logger) : base(dbContext)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<User>> GetUsers()
        {
            List<User> users = new List<User>();
            users = await _dbContext.Users.ToListAsync();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<User> GetUser(int id)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user != null ? user : new User();
        }

        [HttpPost()]
        public async Task<long> CreateUser()
        {
            User user = new User();
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        [HttpPut("{id}")]
        public async Task UpdateUser(int id, User userRequest)
        {
            User? user = await _dbContext.Users
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement User pour l'Id '{id}'");
            }
            user.Login = userRequest.Login;
            user.Age = userRequest.Age;
            user.Nom = userRequest.Nom;
            user.Prenom = userRequest.Prenom;
            user.ProfilId = userRequest.ProfilId;
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteUser(int id)
        {
            User? user = await _dbContext.Users
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception($"Il n'existe pas d'enregistrement User pour l'Id '{id}'");
            }
            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
