using System;
using System.Collections.Generic;
using AutoMapper;
using BustleApp_api.Domain.UserProfileAggregate.Dtos;

namespace BustleApp_api.Repository.MappingConfigurations
{
    public class MappingProfile
    {
        public static Mapper MappingConfigurationSetups()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserProfileDto, UserProfile>();
                cfg.CreateMap<UserProfile, UserProfileDto>();
                cfg.CreateMap<List<UserProfile>, List<UserProfileDto>>();
                cfg.CreateMap<List<UserProfileDto>, List<UserProfile>>();

            });

            var mapper = new Mapper(config);

            return mapper;
        }
    }
}
