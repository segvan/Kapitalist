using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Kapitalist.RatesApi.Commands;
using Kapitalist.RatesApi.Proto;
using MediatR;
using ApiModel = Kapitalist.Common.ApiModels;

namespace Kapitalist.RatesApi
{
    public class RatesService : Proto.RatesService.RatesServiceBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public RatesService(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public override async Task<Empty> AddRatesSnapshot(RatesSnapshot request, ServerCallContext context)
        {
            var ratesSnapshot = mapper.Map<ApiModel.RatesSnapshot>(request);
            await mediator.Send(new AddRatesSnapshot.Command(ratesSnapshot));
            return new Empty();
        }
    }
}