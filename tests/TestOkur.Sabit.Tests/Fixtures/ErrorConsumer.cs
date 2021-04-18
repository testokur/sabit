using System.Collections.Concurrent;
using System.Threading.Tasks;
using MassTransit;
using TestOkur.Sabit.Models;

namespace TestOkur.Sabit.Tests.Fixtures
{
    public class ErrorConsumer : IConsumer<ErrorModel>
    {
        public static readonly ConcurrentBag<ErrorModel> Received = new ConcurrentBag<ErrorModel>();

        public Task Consume(ConsumeContext<ErrorModel> context)
        {
            Received.Add(context.Message);

            return Task.CompletedTask;
        }
    }
}