using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BustleApp_api.Domain.Interfaces;
using BustleApp_api.Domain.UserProfileAggregate.Dtos;

namespace BustleApp_api.Domain.UserProfileAggregate
{
   

    public interface IUserProfileRepository : IGenericRepository<UserProfile>
    {
        Task<UserProfileDto> GetUserForView(int Id);
        Task CreateOrEditUsers(UserProfileDto input);
        Task<UserProfileDto> GetUserForEdit(UserProfileDto input);
        Task<int> DeleteUser(int Id);
        List<UserProfileDto> GetAllUsers(UserProfileDto input);
        Task<LoginResponseDto> AutheticateUser(LoginRequestDto input);
    }
}
