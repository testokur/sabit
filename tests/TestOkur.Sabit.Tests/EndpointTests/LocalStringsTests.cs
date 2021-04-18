using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TestOkur.Sabit.Models;
using TestOkur.Sabit.Tests.Fixtures;
using Xunit;

namespace TestOkur.Sabit.Tests.EndpointTests
{
    public class LocalStringsTests : IClassFixture<WebApplicationFactory>
    {
        private readonly WebApplicationFactory _webApplicationFactory;

        public LocalStringsTests(WebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task ShouldReturnLocalizationStrings()
        {
            var client = _webApplicationFactory.CreateClient();
            var response = await client.GetAsync("/api/local-strings");
            var localStrings = await response.Content.ReadAsAsync<IEnumerable<LocalString>>();
            localStrings.Should().NotBeEmpty()
                .And.NotContain(
                    x => string.IsNullOrEmpty(x.Value) ||
                         string.IsNullOrEmpty(x.Name));
        }
    }
}