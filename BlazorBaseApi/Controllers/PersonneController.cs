using BlazorBaseModel;
using BlazorBaseModel.Model;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonneController : ControllerBase
    {
        private readonly ILogger<PersonneController> _logger;

        public PersonneController(ILogger<PersonneController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [HttpGet()]
        public IEnumerable<PersonneDto> GetPersonnes()
        {
            return new List<PersonneDto>();
        }

        [Route("{id}")]
        [HttpGet()]
        public PersonneDto GetPersonne(int id)
        {
            return new PersonneDto();
        }

        [Route("{id}")]
        [HttpPost()]
        public PersonneDto SetPersonne(int id)
        {
            //_dbContext.Personne.Get
            return new PersonneDto();
        }
    }
}
