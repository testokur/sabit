using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using TestOkur.Sabit.Configuration;
using TestOkur.Sabit.Infrastructure;
using TestOkur.Testing;
using Xunit;

namespace TestOkur.Sabit.Tests.Infrastructure
{
    public class CachedJsonDataSourceTests
    {
        [Theory]
        [TestOkurAutoData]
        public async Task ShouldCache(
             Mock<IJsonDataSource> jsonDataSourceMock,
             string path,
             string expectedResult,
             AppConfiguration appConfiguration)
        {
            jsonDataSourceMock.Setup(x => x.ReadAsync<string>(path, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var cachedJsonDataSource = new CachedJsonDataSource(
                new MemoryCache(new MemoryCacheOptions()),
                jsonDataSourceMock.Object,
                appConfiguration);
            await cachedJsonDataSource.ReadAsync<string>(path);
            await cachedJsonDataSource.ReadAsync<string>(path);
            var result = await cachedJsonDataSource.ReadAsync<string>(path);
            result.Should().Be(expectedResult);

            jsonDataSourceMock.Verify(x => x.ReadAsync<string>(path, It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
