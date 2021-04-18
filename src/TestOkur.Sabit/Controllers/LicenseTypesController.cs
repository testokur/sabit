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
    [Route("api/license-types")]
    [Authorize]
    public class LicenseTypesController : ControllerBase
    {
        private const string FilePath = "license-types.json";
        private const string Dir = "Data";

        private readonly IJsonDataSource _jsonDataSource;

        public LicenseTypesController(IJsonDataSource jsonDataSource)
        {
            _jsonDataSource = jsonDataSource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LicenseType>), StatusCodes.Status200OK)]
        public Task<IEnumerable<LicenseType>> GetAsync()
        {
            return _jsonDataSource.ReadAsync<IEnumerable<LicenseType>>(
                Path.Join(Dir, FilePath));
        }
    }
}