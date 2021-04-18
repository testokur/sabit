using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TestOkur.Sabit.Infrastructure;
using TestOkur.Sabit.Models;

namespace TestOkur.Sabit.Services
{
    public class CityService : ICityService
    {
        private const string FilePath = "cities.json";
        private const string Dir = "Data";

        private readonly IJsonDataSource _jsonDataSource;

        public CityService(IJsonDataSource jsonDataSource)
        {
            _jsonDataSource = jsonDataSource;
        }

        public Task<IEnumerable<City>> GetCitiesAsync(CancellationToken cancellationToken = default)
        {
            return _jsonDataSource.ReadAsync<IEnumerable<City>>(
                Path.Join(Dir, FilePath),
                cancellationToken);
        }
    }
}
