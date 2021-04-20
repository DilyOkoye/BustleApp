using System;
using BustleApp_api.Domain.SubscriptionAggregate.Dtos;
using BustleApp_api.Domain.UserProfileAggregate.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BustleApp_api.Repository.DatabaseContext
{
    
    public class BustleContext : DbContext
    {
        public BustleContext(DbContextOptions<BustleContext> options) : base(options)
        { }
            public DbSet<UserProfile> UserProfile { get; set; }
            public DbSet<Subscription> Subscription { get; set; }


    }
}
