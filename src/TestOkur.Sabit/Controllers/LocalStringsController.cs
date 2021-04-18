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
    [Route("api/local-strings")]
    [Authorize]
    public class LocalStringsController : ControllerBase
    {
        private const string FilePath = "local-strings.json";
        private const string Dir = "Data";

        private readonly IJsonDataSource _jsonDataSource;

        public LocalStringsController(IJsonDataSource jsonDataSource)
        {
            _jsonDataSource = jsonDataSource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocalString>), StatusCodes.Status200OK)]
        public Task<IEnumerable<LocalString>> GetAsync()
        {
            return _jsonDataSource.ReadAsync<IEnumerable<LocalString>>(
                Path.Join(Dir, FilePath));
        }
    }
}