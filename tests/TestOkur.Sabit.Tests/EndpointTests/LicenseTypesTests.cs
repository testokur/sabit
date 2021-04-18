using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TestOkur.Sabit.Models;
using TestOkur.Sabit.Tests.Fixtures;
using Xunit;

namespace TestOkur.Sabit.Tests.EndpointTests
{
    public class LicenseTypesTests : IClassFixture<WebApplicationFactory>
    {
        private readonly WebApplicationFactory _webApplicationFactory;

        public LicenseTypesTests(WebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task ShouldReturnLicenseTypes()
        {
            var client = _webApplicationFactory.CreateClient();
            var response = await client.GetAsync("/api/license-types");
            var licenseTypes = await response.Content.ReadAsAsync<IEnumerable<LicenseType>>();
            licenseTypes.Should().NotContain(
                x => x.Id <= 0 ||
                     string.IsNullOrEmpty(x.Name) ||
                     x.MaxAllowedDeviceCount <= 0 ||
                     x.MaxAllowedRecordCount < 0);
        }
    }
}