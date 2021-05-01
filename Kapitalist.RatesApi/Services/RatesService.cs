using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Kapitalist.RatesApi
{
    public class RatesService : Proto.RatesService.RatesServiceBase
    {
        private readonly ILogger<RatesService> logger;

        public RatesService(ILogger<RatesService> logger)
        {
            this.logger = logger;
        }

        public override Task<Empty> AddRatesSnapshot(Proto.RatesSnapshot request, ServerCallContext context)
        {
            logger.LogInformation(request.Source);
            return Task.FromResult(new Empty());
        }
    }
}