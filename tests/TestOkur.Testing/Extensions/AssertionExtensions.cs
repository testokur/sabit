using System.Net.Http;
using TestOkur.Testing;

namespace FluentAssertions
{
    public static class AssertionExtensions
    {
        public static HttpResponseMessageAssertions Should(this HttpResponseMessage actualValue)
        {
            return new HttpResponseMessageAssertions(actualValue);
        }
    }
}