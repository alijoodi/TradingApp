using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.TradingUserDtos;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        protected AutoMapperProfile()
        {
            CreateMap<TradingUser, TradingUserDto>().ReverseMap();
            CreateMap<TradingUser, TradingUserDto>().ReverseMap();
        }
    }
}