using System.Threading;
using System.Threading.Tasks;
using Kapitalist.Common.ApiModels;
using MediatR;

namespace Kapitalist.RatesApi.Commands
{
    public class AddRatesSnapshot
    {
        public class Command : IRequest
        {
            public Command(RatesSnapshot ratesSnapshot)
            {
                RatesSnapshot = ratesSnapshot;
            }

            public RatesSnapshot RatesSnapshot { get; }
        }
        
        public class CommandHandler : IRequestHandler<Command>
        {
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var rates = request.RatesSnapshot;

                return Unit.Value;
            }
        }
    }
}