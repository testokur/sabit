using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TestOkur.Sabit.Infrastructure;
using TestOkur.Sabit.Models;

namespace TestOkur.Sabit.Controllers
{
    [ApiController]
    [Route("/api/cities")]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        private const string FilePath = "cities.json";
        private const string Dir = "Data";

        private readonly IJsonDataSource _jsonDataSource;

        public CitiesController(IJsonDataSource jsonDataSource)
        {
            _jsonDataSource = jsonDataSource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<City>), StatusCodes.Status200OK)]
        public Task<IEnumerable<City>> GetAsync()
        {
            return _jsonDataSource.ReadAsync<IEnumerable<City>>(
                Path.Join(Dir, FilePath));
        }
    }
}
