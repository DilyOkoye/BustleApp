using System;
using BustleApp_api.CartAggregate;
using BustleApp_api.Domain.SubscriptionAggregate;
using BustleApp_api.Domain.UserProfileAggregate;
using BustleApp_api.InventoryAggregate;

namespace BustleApp_api.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
       
        IUserProfileRepository UserProfiles { get; }
        ISubscriptionRepository Subscriptions { get; }
        IInventoryRepository Inventories { get; }
        ICartRepository Carts { get; }
        int Complete();
    }
}
