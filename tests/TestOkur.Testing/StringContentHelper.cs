using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestOkur.Testing
{
    public static class StringContentHelper
    {
        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

        public static StringContent Create(object obj)
        {
            return new StringContent(
                JsonSerializer.Serialize(obj, SerializerOptions),
                Encoding.UTF8, MediaTypeNames.Application.Json);
        }
    }
}