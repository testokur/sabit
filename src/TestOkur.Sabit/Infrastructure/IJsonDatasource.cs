using System.Threading;
using System.Threading.Tasks;

namespace TestOkur.Sabit.Infrastructure
{
    public interface IJsonDataSource
    {
        Task<T> ReadAsync<T>(
            string path,
            CancellationToken cancellationToken = default);
    }
}
