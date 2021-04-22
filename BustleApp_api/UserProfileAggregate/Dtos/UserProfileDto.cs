using System;
using BustleApp_api.Domain.Utilities;

namespace BustleApp_api.Domain.UserProfileAggregate.Dtos
{
    public class UserProfileDto
    {
        public int? Id { get; set; }
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public int SubscriptionExpiryDaysLeft { get; set; }
        public int? AccessFailedCount { get; set; }
        public string AuthenticationSource { get; set; }
        public string ConcurrencyStamp { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public string EmailAddress { get; set; }
        public string EmailConfirmationCode { get; set; }
        public int? IsActive { get; set; }
        public int? IsEmailConfirmed { get; set; }
        public int? IsLockoutEnabled { get; set; }
        public int? IsPhoneNumberConfirmed { get; set; }
        public int? IsTwoFactorEnabled { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Password { get; set; }
        public string PasswordResetCode { get; set; }
        public string PhoneNumber { get; set; }
        public int? ShouldChangePasswordOnNextLogin { get; set; }
        public string UserName { get; set; }
        public string SignInToken { get; set; }
        public string GoogleAuthenticatorKey { get; set; }
        public PagedResultDto PagedResultDto { get; set; }
    }
}
