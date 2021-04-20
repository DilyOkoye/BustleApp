using System;
namespace BustleApp_api.Domain.UserProfileAggregate.Dtos
{
    public class LoginResponseDto
    {
        public int EnforcePassChange { get; set; }
        public string FullName { get; set; }
        public long UserId { get; set; }
        public string EmailAddress { set; get; }
        public string Status { set; get; }
        public int? RoleId { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseText { get; set; }
        public string Url { get; set; }
        public string SubscriptionName { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public int SubscriptionExpiryDaysLeft { get; set; }
    }
}
