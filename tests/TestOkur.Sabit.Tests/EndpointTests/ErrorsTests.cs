using FluentAssertions;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using TestOkur.Sabit.Models;
using TestOkur.Sabit.Tests.Extensions;
using TestOkur.Sabit.Tests.Fixtures;
using TestOkur.Testing;
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

        [Theory]
        [TestOkurAutoData]
        public async Task When_ErrorMessagePosted_Then_ErrorMessageShouldBePublished(
            ErrorModel model)
        {
            var bus =_webApplicationFactory.Services.GetRequiredService<IBusControl>();
            await bus.StartAsync();
            var client = _webApplicationFactory.CreateClient();
            var response = await client.PostAsync(ApiPath, StringContentHelper.Create(model));
            response.IsSuccessStatusCode.Should().BeTrue();
            await bus.StopAsync();
            ErrorConsumer.Received.Count.Should().BeGreaterOrEqualTo(1);
        }
    }
}