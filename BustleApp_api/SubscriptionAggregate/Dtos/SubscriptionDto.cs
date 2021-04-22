using System;
using BustleApp_api.Domain.Utilities;

namespace BustleApp_api.Domain.SubscriptionAggregate.Dtos
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UserId { get; set; }
        public string Status { get; set; }
        public PagedResultDto PagedResultDto { get; set; }
        public string Username { get; set; }
    }
}
