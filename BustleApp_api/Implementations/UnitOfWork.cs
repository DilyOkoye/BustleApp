using System;
using BustleApp_api.CartAggregate;
using BustleApp_api.Domain.Interfaces;
using BustleApp_api.Domain.SubscriptionAggregate;
using BustleApp_api.Domain.UserProfileAggregate;
using BustleApp_api.InventoryAggregate;
using BustleApp_api.Repositories;
using BustleApp_api.Repository.DatabaseContext;
using BustleApp_api.ShoppingListAggregate;

namespace BustleApp_api.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BustleContext _context;
        public IShoppingListRepository ShoppingLists { get; }
        public IUserProfileRepository UserProfiles { get; }
        public ISubscriptionRepository Subscriptions { get; }
        public IInventoryRepository Inventories { get; }
        public ICartRepository Carts { get; }

        public UnitOfWork(BustleContext bustleContext,
             IUserProfileRepository userProfileRepository,
             ISubscriptionRepository subscriptionRepository,
             IInventoryRepository InventoryRepository,
             ICartRepository cartRepository,
             IShoppingListRepository shoppingRepository
          )
        {
            this._context = bustleContext;
            this.UserProfiles = userProfileRepository;
            this.Subscriptions = subscriptionRepository;
            this.Inventories = InventoryRepository;
            this.Carts = cartRepository;
            this.ShoppingLists = shoppingRepository;


        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
