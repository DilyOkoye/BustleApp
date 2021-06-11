using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BustleApp_api.CartAggregate.Dtos;
using BustleApp_api.Domain.Interfaces;
using BustleApp_api.Domain.UserProfileAggregate.Dtos;
using BustleApp_api.InventoryAggregate.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BustleApp_api.Controllers
{
   // [Route("api/[controller]")]
   // [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET: /<User Profile Controller>/
        [HttpGet]
        [Route("GetUsersView")]
        public async Task<UserProfileDto> GetUserForView(int Id)
        {
            return await _unitOfWork.UserProfiles.GetUserForView(Id);
        }

        [HttpPost]
        [Route("CreateOrEditUsers")]
        public async Task<LoginResponseDto> CreateOrEditUsers(UserProfileDto input)
        {
          return  await _unitOfWork.UserProfiles.CreateOrEditUsers(input);
        }

        [HttpGet]
        [Route("GetUserForEdit")]
        public async Task GetUserForEdit(UserProfileDto input)
        {
            await _unitOfWork.UserProfiles.GetUserForEdit(input);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public async Task DeleteUser(int Id)
        {
            await _unitOfWork.UserProfiles.DeleteUser(Id);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public List<UserProfileDto> GetAllUsers(UserProfileDto input)
        {
            return _unitOfWork.UserProfiles.GetAllUsers(input);
        }

        [HttpPost]
        [Route("AutheticateUser")]
        public async Task<LoginResponseDto> AutheticateUser(LoginRequestDto input)
        {
            return await _unitOfWork.UserProfiles.AutheticateUser(input);
        }


        

    }
}
