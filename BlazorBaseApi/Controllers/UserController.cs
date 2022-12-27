using BlazorBaseModel;
using BlazorBaseModel.Model;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly string[] NomList = new[]
        {
            "Jeannin", "Albanese"
        };
        private static readonly string[] PrenomList = new[]
        {
            "Dominique", "Virginie", "Dorian", "Béryl"
        };

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [HttpGet()]
        public IEnumerable<User> GetUsers()
        {
            return Enumerable.Range(1, 5).Select(index => new User
            {
                Id = Random.Shared.Next(1, 8),
                Nom = NomList[Random.Shared.Next(NomList.Length)],
                Prenom = PrenomList[Random.Shared.Next(PrenomList.Length)],
                Age = Random.Shared.Next(3, 42)
            })
            .ToArray();
        }

        [Route("{id}")]
        [HttpGet()]
        public IEnumerable<User> GetUser(int id)
        {
            return Enumerable.Range(1, 1).Select(index => new User
            {
                Id = id,
                Nom = NomList[Random.Shared.Next(NomList.Length)],
                Prenom = PrenomList[Random.Shared.Next(PrenomList.Length)],
                Age = Random.Shared.Next(3, 42)
            })
            .ToArray();
        }
    }
}
