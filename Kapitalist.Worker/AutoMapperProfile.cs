using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Proto = Kapitalist.RatesGrpcClient.Proto;
using ApiModel = Kapitalist.Common.ApiModels;

namespace Kapitalist.Worker
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApiModel.Rate, Proto.Rate>();
            CreateMap<ApiModel.RatesSnapshot, Proto.RatesSnapshot>()
                .ForMember(dest => dest.RateType,
                    opts => opts.MapFrom(x => (int) x.RateType))
                .ForMember(dest => dest.TimeStamp,
                    opts => opts.MapFrom(x => Timestamp.FromDateTime(x.TimeStamp)));
        }
    }
}