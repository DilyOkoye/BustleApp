using System;
using System.Collections.Generic;
using AutoMapper;
using BustleApp_api.CartAggregate.Dtos;
using BustleApp_api.Domain.UserProfileAggregate.Dtos;
using BustleApp_api.InventoryAggregate.Dtos;

namespace BustleApp_api.Repository.MappingConfigurations
{
    public class MappingProfile
    {
        public static Mapper MappingConfigurationSetups()
        {

            var config = new MapperConfiguration(cfg =>
            {
                // User Profile Mapping
                cfg.CreateMap<UserProfileDto, UserProfile>();
                cfg.CreateMap<UserProfile, UserProfileDto>();
                cfg.CreateMap<List<UserProfile>, List<UserProfileDto>>();
                cfg.CreateMap<List<UserProfileDto>, List<UserProfile>>();

                //  Inventory Mapping
                cfg.CreateMap<InventoryDto, Inventory>();
                cfg.CreateMap<Inventory, InventoryDto>();
                cfg.CreateMap<List<Inventory>, List<InventoryDto>>();
                cfg.CreateMap<List<InventoryDto>, List<Inventory>>();


                //  Inventory Cart
                cfg.CreateMap<CartDto, Cart>();
                cfg.CreateMap<Cart, CartDto>();
                cfg.CreateMap<List<Cart>, List<CartDto>>();
                cfg.CreateMap<List<CartDto>, List<Cart>>();

            });

            var mapper = new Mapper(config);

            return mapper;
        }
    }
}
