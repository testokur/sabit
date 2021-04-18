using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TestOkur.Sabit.Extensions
{
    public static class FormFileExtensions
    {
        public static async Task SaveAsync(this IFormFile file, string folderPath)
        {
            var path = Path.Combine(folderPath, file.FileName);

            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}