using FluentAssertions;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TestOkur.Sabit.Tests.Extensions;
using TestOkur.Sabit.Tests.Fixtures;
using Xunit;

namespace TestOkur.Sabit.Tests.EndpointTests
{
    public class ErrorsTests : IClassFixture<WebApplicationFactory>
    {
        private const string ApiPath = "api/errors";

        private readonly WebApplicationFactory _webApplicationFactory;

        public ErrorsTests(WebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task ShouldUploadFile()
        {
            const string testImagePath = "ss.png";

            var client = _webApplicationFactory.CreateClient();
            await using var stream = File.OpenRead(testImagePath);
            var response = await client.PostAsync($"{ApiPath}/upload", new MultipartFormDataContent()
            {
                {
                    new ByteArrayContent(stream.ToByteArray()), "file", testImagePath
                },
            });

            response.IsSuccessStatusCode.Should().BeTrue();
            var imagePath = await response.Content.ReadAsStringAsync();
            imagePath.Should().Contain(testImagePath);
        }
    }
}