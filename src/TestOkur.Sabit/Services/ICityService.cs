using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestOkur.Sabit.Models;

namespace TestOkur.Sabit.Services
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetCitiesAsync(CancellationToken cancellationToken = default);
    }
}