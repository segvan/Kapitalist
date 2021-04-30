using System.Net.Http;

namespace Kapitalist.Common.Extensions
{
    public static class HttpClientFactoryExtensions
    {
        public static HttpClient CreateGenericClient(this IHttpClientFactory factory)
        {
            return factory.CreateClient("GenericClient");
        }
    }
}