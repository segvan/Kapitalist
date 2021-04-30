using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Kapitalist.Common.HttpHandlers
{
    public class LoggerDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger logger;

        public LoggerDelegatingHandler(ILogger<LoggerDelegatingHandler> logger)
        {
            this.logger = logger;
        }

        public LoggerDelegatingHandler(HttpMessageHandler innerHandler, ILogger logger)
            : base(innerHandler)
        {
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Guid requestId = Guid.NewGuid();
            logger.LogInformation($"HTTP request: {requestId} -- {request.Method} -- {request.RequestUri}");

            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation(
                    $"HTTP response: {requestId} -- {(int) response.StatusCode} {response.ReasonPhrase}");
            }
            else
            {
                logger.LogError(
                    $"HTTP response: {requestId} -- {(int) response.StatusCode} {response.ReasonPhrase}");
                logger.LogError(
                    $"HTTP response: {requestId} -- {await response.Content.ReadAsStringAsync(cancellationToken)}");
            }

            return response;
        }
    }
}