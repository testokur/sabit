using System.IO;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestOkur.Sabit.Extensions;
using TestOkur.Sabit.Models;

namespace TestOkur.Sabit.Controllers
{
    [ApiController]
    [Route("api/errors")]
    public class ErrorsController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IPublishEndpoint _publishEndpoint;

        public ErrorsController(IWebHostEnvironment hostingEnvironment, IPublishEndpoint publishEndpoint)
        {
            _hostingEnvironment = hostingEnvironment;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostAsync(ErrorModel model)
        {
            await _publishEndpoint.Publish(model);
            return Accepted();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            await file.SaveAsync(Path.Combine(_hostingEnvironment.WebRootPath, "uploads"));
            return Ok($@"\uploads\{file.FileName}");
        }
    }
}