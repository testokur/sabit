using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOkur.Sabit.Configuration;

namespace TestOkur.Sabit.Infrastructure
{
    public class CachedJsonDataSource : IJsonDataSource
    {
        private readonly IJsonDataSource _decoratedJsonDataSource;
        private readonly IMemoryCache _memoryCache;
        private readonly AppConfiguration _appConfiguration;

        public CachedJsonDataSource(
            IMemoryCache memoryCache,
            IJsonDataSource decoratedJsonDataSource,
            AppConfiguration appConfiguration)
        {
            _memoryCache = memoryCache;
            _decoratedJsonDataSource = decoratedJsonDataSource;
            _appConfiguration = appConfiguration;
        }

        public Task<T> ReadAsync<T>(string path, CancellationToken cancellationToken = default)
        {
            return _memoryCache.GetOrCreateAsync(
                path, entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromSeconds(_appConfiguration.CacheDurationSec);
                    return _decoratedJsonDataSource.ReadAsync<T>(path, cancellationToken);
                });
        }
    }
}