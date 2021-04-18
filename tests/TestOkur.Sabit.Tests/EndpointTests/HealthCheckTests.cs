using FluentAssertions;
using System.Threading.Tasks;
using TestOkur.Sabit.Tests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace TestOkur.Sabit.Tests.EndpointTests
{
    public class HealthCheckTests : IClassFixture<WebApplicationFactory>
    {
        private readonly WebApplicationFactory _webApplicationFactory;
        private readonly ITestOutputHelper _testOutputHelper;

        public HealthCheckTests(
            WebApplicationFactory webApplicationFactory,
            ITestOutputHelper testOutputHelper)
        {
            _webApplicationFactory = webApplicationFactory;
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("api/live")]
        [InlineData("api/ready")]
        public async Task When_ApplicationIsUpAndRunning_Then_HealthCheckEndpoints_Should_ReturnSuccess(
            string endpoint)
        {
            var healthCheckResponse = await _webApplicationFactory.CreateClient().GetAsync(endpoint);
            _testOutputHelper.WriteLine($"{endpoint} : {await healthCheckResponse.Content.ReadAsStringAsync()}");
            healthCheckResponse.Should().BeSuccessful();
        }
    }
}
