using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestOkur.Sabit.Models;
using TestOkur.Sabit.Services;

namespace TestOkur.Sabit.Controllers
{
    [ApiController]
    [Route("/api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<City>), StatusCodes.Status200OK)]
        public Task<IEnumerable<City>> GetAsync()
        {
            return _cityService.GetCitiesAsync();
        }
    }
}
