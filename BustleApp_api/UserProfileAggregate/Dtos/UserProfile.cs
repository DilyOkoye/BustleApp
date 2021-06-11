using System;
namespace BustleApp_api.Domain.UserProfileAggregate.Dtos
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int? AccessFailedCount { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public string userEmail { get; set; }
        public int? IsActive { get; set; }
        public int? IsEmailConfirmed { get; set; }
        public int? IsLockoutEnabled { get; set; }
        public string FirstName { get; set; }
        public string industryType { get; set; }
        public string businessName { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string userAddress { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string userPassword   { get; set; }
        public string PhoneNumber { get; set; }
        public int? ShouldChangePasswordOnNextLogin { get; set; }
        public string UserName { get; set; }
    }   
}
