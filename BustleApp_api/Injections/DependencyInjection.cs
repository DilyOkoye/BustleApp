using System;
using BustleApp_api.CartAggregate;
using BustleApp_api.Domain.Interfaces;
using BustleApp_api.Domain.SubscriptionAggregate;
using BustleApp_api.Domain.UserProfileAggregate;
using BustleApp_api.InventoryAggregate;
using BustleApp_api.Repositories;
using BustleApp_api.Repository.Implementations;
using BustleApp_api.Repository.Repositories;
using BustleApp_api.ShoppingListAggregate;
using Microsoft.Extensions.DependencyInjection;

namespace BustleApp_api.Repository.Injections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IShoppingListRepository, ShoppingListRepository>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
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
