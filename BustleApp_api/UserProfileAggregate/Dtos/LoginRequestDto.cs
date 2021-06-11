using System;
namespace BustleApp_api.Domain.UserProfileAggregate.Dtos
{
    public class LoginRequestDto
    {
        public string userEmail { get; set; }
        public string userPassword { get; set; }
    }
}
