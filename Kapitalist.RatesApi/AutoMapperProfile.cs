using System;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Kapitalist.RatesApi.Proto;
using ApiModel = Kapitalist.Common.ApiModels;
using DB = Kapitalist.RatesApi.Models;

namespace Kapitalist.RatesApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Timestamp, DateTime>().ConvertUsing(x => x.ToDateTime());
            CreateMap<Rate, ApiModel.Rate>();
            CreateMap<RatesSnapshot, ApiModel.RatesSnapshot>();
            
            CreateMap<ApiModel.Rate, DB.Rate>().ReverseMap();
            CreateMap<ApiModel.RatesSnapshot, DB.RatesSnapshot>().ReverseMap();
        }
    }
}