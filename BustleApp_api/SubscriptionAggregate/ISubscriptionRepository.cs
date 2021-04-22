using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BustleApp_api.Domain.Interfaces;
using BustleApp_api.Domain.SubscriptionAggregate.Dtos;

namespace BustleApp_api.Domain.SubscriptionAggregate
{
    
    public interface ISubscriptionRepository : IGenericRepository<Subscription>
    {
        Task<SubscriptionDto> GetSubscriptionForView(int Id);
        Task CreateOrEditSubscription(SubscriptionDto input);
        Task<SubscriptionDto> GetSubscriptionForEdit(SubscriptionDto input);
        Task<int> DeleteSubscription(int Id);
        List<SubscriptionDto> GetAllSubscription(SubscriptionDto input);
    }
}
