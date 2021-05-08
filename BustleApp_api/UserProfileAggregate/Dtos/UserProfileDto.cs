using System;
using BustleApp_api.Domain.Utilities;

namespace BustleApp_api.Domain.UserProfileAggregate.Dtos
{
    public class UserProfileDto
    {
        public int? Id { get; set; }
        public int? AccessFailedCount { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public string EmailAddress { get; set; }
        public int? IsActive { get; set; }
        public int? IsEmailConfirmed { get; set; }
        public int? IsLockoutEnabled { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int? ShouldChangePasswordOnNextLogin { get; set; }
        public string UserName { get; set; }
        public PagedResultDto PagedResultDto { get; set; }
    }
}
