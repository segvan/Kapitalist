using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kapitalist.Common.HttpHandlers
{
    public class EnsureStatusCodeDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}