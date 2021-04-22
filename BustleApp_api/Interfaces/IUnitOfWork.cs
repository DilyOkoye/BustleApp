using System;
using BustleApp_api.Domain.SubscriptionAggregate;
using BustleApp_api.Domain.UserProfileAggregate;

namespace BustleApp_api.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
       
        IUserProfileRepository UserProfiles { get; }
        ISubscriptionRepository Subscriptions { get; }
        int Complete();
    }
}
