using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Kapitalist.Common.ApiModels;
using Kapitalist.RatesApi.Database;
using MediatR;
using DB = Kapitalist.RatesApi.Models;

namespace Kapitalist.RatesApi.Commands
{
    internal class AddRatesSnapshot
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
            private readonly IValidator<RatesSnapshot> validator;
            private readonly RatesDataContext context;
            private readonly IMapper mapper;

            public CommandHandler(IValidator<RatesSnapshot> validator, RatesDataContext context, IMapper mapper)
            {
                this.validator = validator;
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await validator.ValidateAndThrowAsync(request.RatesSnapshot, cancellationToken);

                var rates = mapper.Map<DB.RatesSnapshot>(request.RatesSnapshot);

                await context.RatesSnapshots.AddAsync(rates, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}