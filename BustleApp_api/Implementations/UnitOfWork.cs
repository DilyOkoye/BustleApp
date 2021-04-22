using System;
using BustleApp_api.Domain.Interfaces;
using BustleApp_api.Domain.SubscriptionAggregate;
using BustleApp_api.Domain.UserProfileAggregate;
using BustleApp_api.Repository.DatabaseContext;

namespace BustleApp_api.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BustleContext _context;
        public IUserProfileRepository UserProfiles { get; }
        public ISubscriptionRepository Subscriptions { get; }

        public UnitOfWork(BustleContext bustleContext,
             IUserProfileRepository userProfileRepository, ISubscriptionRepository subscriptionRepository
          )
        {
            this._context = bustleContext;
            this.UserProfiles = userProfileRepository;
            this.Subscriptions = subscriptionRepository;

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
