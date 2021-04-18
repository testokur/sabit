using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestOkur.Sabit.Extensions;

namespace TestOkur.Sabit.Controllers
{
    [ApiController]
    [Route("api/errors")]
    public class ErrorsController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ErrorsController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            await file.SaveAsync(Path.Combine(_hostingEnvironment.WebRootPath, "uploads"));
            return Ok($@"\uploads\{file.FileName}");
        }
    }
}