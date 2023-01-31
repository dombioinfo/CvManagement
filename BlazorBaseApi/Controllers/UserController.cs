using BlazorBaseModel;
using BlazorBaseModel.Model;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : AppController
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, MysqlDbContext dbContext) : base(dbContext)
        {
            _logger = logger;
        }

        [Route("")]
        [HttpGet()]
        public IEnumerable<UserDto> GetUsers()
        {
            return new List<UserDto>();
        }

        [Route("{id}")]
        [HttpGet()]
        public UserDto GetUser(int id)
        {
            return null; //this._dbContext.Users.FirstOrDefault(u => u.Id == id) ?? new UserDto();
        }
    }
}
