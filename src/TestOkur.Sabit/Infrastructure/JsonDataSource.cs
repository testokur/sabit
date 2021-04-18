using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace TestOkur.Sabit.Infrastructure
{
    public class JsonDataSource : IJsonDataSource
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<T> ReadAsync<T>(string path, CancellationToken cancellationToken = default)
        {
            await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return await JsonSerializer.DeserializeAsync<T>(
                stream,
                JsonSerializerOptions,
                cancellationToken);
        }
    }
}