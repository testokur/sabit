using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TestOkur.Sabit.Models;
using TestOkur.Sabit.Tests.Fixtures;
using Xunit;

namespace TestOkur.Sabit.Tests.EndpointTests
{
    public class CitiesTests : IClassFixture<WebApplicationFactory>
    {
        private readonly WebApplicationFactory _webApplicationFactory;

        public CitiesTests(WebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task ShouldReturnCitiesAndDistricts()
        {
            var client = _webApplicationFactory.CreateClient();
            var response = await client.GetAsync("/api/cities");
            var cities = await response.Content.ReadAsAsync<IEnumerable<City>>();
            CitiesAndDistrictsShouldNotBeEmpty(cities);
            CitiesShouldBeOrdered(cities);
            DistrictsShouldBeOrdered(cities);
        }

        private void CitiesAndDistrictsShouldNotBeEmpty(IEnumerable<City> cities)
        {
            cities.Should().NotBeEmpty()
                .And
                .NotContain(c => c.Districts == null || !c.Districts.Any())
                .And
                .NotContain(c => c.Districts.Select(d => d.Name).Any(string.IsNullOrEmpty));
        }

        private void CitiesShouldBeOrdered(IEnumerable<City> cities)
        {
            cities.Select(c => c.Name)
                .Should()
                .BeInAscendingOrder(new TrStringComparer());
        }

        private void DistrictsShouldBeOrdered(IEnumerable<City> cities)
        {
            foreach (var city in cities)
            {
                city.Districts
                    .Select(t => t.Name)
                    .Should()
                    .BeInAscendingOrder(new TrStringComparer());
            }
        }

        private class TrStringComparer : IComparer<object>
        {
            private static readonly CultureInfo Culture = new CultureInfo("tr-TR");

            public int Compare(object x, object y) =>
                string.Compare((string)x, (string)y, false, Culture);
        }
    }
}