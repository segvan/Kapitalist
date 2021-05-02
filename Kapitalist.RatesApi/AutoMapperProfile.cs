using System;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using ApiModel = Kapitalist.Common.ApiModels;

namespace Kapitalist.RatesApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Timestamp, DateTime>().ConvertUsing(x => x.ToDateTime());
            CreateMap<Proto.Rate, ApiModel.Rate>();
            CreateMap<Proto.RatesSnapshot, ApiModel.RatesSnapshot>();
        }
    }
}