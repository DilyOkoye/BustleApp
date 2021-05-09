using System;
using BustleApp_api.CartAggregate.Dtos;
using BustleApp_api.Domain.SubscriptionAggregate.Dtos;
using BustleApp_api.Domain.UserProfileAggregate.Dtos;
using BustleApp_api.InventoryAggregate.Dtos;
using BustleApp_api.ShoppingListAggregate.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BustleApp_api.Repository.DatabaseContext
{
    
    public class BustleContext : DbContext
    {
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<ShoppingList> ShoppingList { get; set; }

        public BustleContext(DbContextOptions<BustleContext> options) : base(options)
        { }
           

    }
}
