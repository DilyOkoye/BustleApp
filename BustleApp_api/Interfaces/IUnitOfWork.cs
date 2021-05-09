using System;
using BustleApp_api.CartAggregate;
using BustleApp_api.Domain.SubscriptionAggregate;
using BustleApp_api.Domain.UserProfileAggregate;
using BustleApp_api.InventoryAggregate;
using BustleApp_api.ShoppingListAggregate;

namespace BustleApp_api.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IShoppingListRepository ShoppingLists { get; }
        IUserProfileRepository UserProfiles { get; }
        ISubscriptionRepository Subscriptions { get; }
        IInventoryRepository Inventories { get; }
        ICartRepository Carts { get; }
        int Complete();
    }
}
