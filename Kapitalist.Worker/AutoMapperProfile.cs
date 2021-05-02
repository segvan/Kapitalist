using System;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Kapitalist.Common.Enums;
using Proto = Kapitalist.RatesGrpcClient.Proto;
using ApiModel = Kapitalist.Common.ApiModels;

namespace Kapitalist.Worker
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DateTime, Timestamp>().ConvertUsing(x => Timestamp.FromDateTime(x));
            CreateMap<RateType, int>().ConvertUsing(x => (int)x);
            CreateMap<ApiModel.Rate, Proto.Rate>();
            CreateMap<ApiModel.RatesSnapshot, Proto.RatesSnapshot>();
        }
    }
}