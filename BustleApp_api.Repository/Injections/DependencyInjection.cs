using System;
using BustleApp_api.Domain.Interfaces;
using BustleApp_api.Domain.UserProfileAggregate;
using BustleApp_api.Repository.Implementations;
using BustleApp_api.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BustleApp_api.Repository.Injections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
           
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Auto Mapper Configurations
            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new MappingProfile());
            //});

            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);
            return services;

        }
    }
}
