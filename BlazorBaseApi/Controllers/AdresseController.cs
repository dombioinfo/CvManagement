using BlazorBaseModel;
using BlazorBaseModel.Model;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdresseController : ControllerBase
    {
        private readonly ILogger<AdresseController> _logger;

        public AdresseController(ILogger<AdresseController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [HttpGet()]
        public IEnumerable<AdresseDto> GetAdresses()
        {
            return new List<AdresseDto>();
        }

        [Route("{id}")]
        [HttpGet()]
        public AdresseDto GetAdresse(int id)
        {
            return new AdresseDto();
        }

        [Route("{id}")]
        [HttpPost()]
        public AdresseDto SetAdresse(int id)
        {
            //_dbContext.Adresse.Get
            return new AdresseDto();
        }
    }
}
