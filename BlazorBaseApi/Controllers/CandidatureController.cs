using BlazorBaseModel;
using BlazorBaseModel.Model;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatureController : ControllerBase
    {
        private readonly ILogger<CandidatureController> _logger;

        public CandidatureController(ILogger<CandidatureController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [HttpGet()]
        public IEnumerable<CandidatureDto> GetCandidatures()
        {
            return new List<CandidatureDto>();
        }

        [Route("{id}")]
        [HttpGet()]
        public CandidatureDto GetCandidature(int id)
        {
            return new CandidatureDto();
        }

        [Route("{id}")]
        [HttpPost()]
        public CandidatureDto SetCandidature(int id)
        {
            //_dbContext.Candidature.Get
            return new CandidatureDto();
        }
    }
}
