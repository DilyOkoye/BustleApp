using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BustleApp_api.Domain.UserProfileAggregate;
using BustleApp_api.Domain.UserProfileAggregate.Dtos;
using BustleApp_api.Domain.Utilities;
using BustleApp_api.Repository.DatabaseContext;
using BustleApp_api.Repository.Implementations;
using BustleApp_api.Repository.MappingConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BustleApp_api.Repository.Repositories
{
    public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {
        private readonly IOptions<Configurations> config;
        private readonly IConfiguration _config;

        public UserProfileRepository(BustleContext context, IOptions<Configurations> config) : base(context)
        {
            this.config = config;
        }

        public async Task<UserProfileDto> GetUserForView(int Id)
        {

            var users = await _context.UserProfile.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (users != null)
            {
                return MappingProfile.MappingConfigurationSetups().Map<UserProfileDto>(users);
            }
            return new UserProfileDto();
        }

        public async Task<UserProfileDto> GetUserForEdit(UserProfileDto input)
        {
            var users = await _context.UserProfile.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (users != null)
            {
                UserProfile user = new UserProfile
                {
                    userEmail = input.userEmail,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    MiddleName = input.MiddleName,
                    PhoneNumber = input.PhoneNumber,
                };

                _context.UserProfile.Update(user);
                await _context.SaveChangesAsync();
                return MappingProfile.MappingConfigurationSetups().Map<UserProfileDto>(user);
            }
            return new UserProfileDto();

        }


        public async Task<int> DeleteUser(int Id)
        {
            var users = await _context.UserProfile.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (users != null)
            {
                _context.UserProfile.Remove(users);
                return await _context.SaveChangesAsync();

            }
            return 0;
        }


        public async Task<LoginResponseDto> CreateOrEditUsers(UserProfileDto input)
        {
            
            if (input.Id == null || input.Id == 0)
            {
              return  await Create(input);
            }
            else
            {
                return await Update(input);
            }

        }

        protected virtual async Task<LoginResponseDto> Create(UserProfileDto input)
        {
            var rtv = new LoginResponseDto();
            input.userPassword = Cryptors.GetSHAHashData(input.userPassword);
            input.IsLockoutEnabled = 0;
            input.DateCreated = DateTime.Now;
            input.userEmail = input.userEmail;
            input.ShouldChangePasswordOnNextLogin = 1;
            input.AccessFailedCount = 0;
            input.businessName = input.businessName;
            input.Country = input.Country;
            UserProfile userDto = MappingProfile.MappingConfigurationSetups().Map<UserProfile>(input);
            
            _context.UserProfile.Add(userDto);
           int res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                rtv.ResponseCode = 0;
                rtv.ResponseText = "Successfull";
                return rtv;
            }
            else 
            {
                rtv.ResponseCode = -2;
                rtv.ResponseText = "Failed";
                return rtv;

            }


        }

        protected virtual async Task<LoginResponseDto> Update(UserProfileDto input)
        {
            var rtv = new LoginResponseDto();
            var users = await _context.UserProfile.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (users != null)
            {
                UserProfile user = new UserProfile
                {
                    userEmail = input.userEmail,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    MiddleName = input.MiddleName,
                    PhoneNumber = input.PhoneNumber,
                };

                _context.UserProfile.Update(users);
                int res = await _context.SaveChangesAsync();

                if (res > 0)
                {
                    rtv.ResponseCode = 0;
                    rtv.ResponseText = "Successfull";
                    return rtv;
                }
                
            }
            rtv.ResponseCode = -2;
            rtv.ResponseText = "Failed";
            return rtv;

        }


        public List<UserProfileDto> GetAllUsers(UserProfileDto input)
        {

            var allUsers = (from user in _context.UserProfile.ToList()

                            select new UserProfileDto
                            {
                                userPassword = user.userPassword,
                                userEmail = user.userEmail,
                                Id = user.Id,
                                DateCreated = user.DateCreated,
                                LastName = user.LastName,
                                FirstName = user.FirstName,
                                MiddleName = user.MiddleName,

                            }).ToList();

            // Map Records
            List<UserProfileDto> userDto = MappingProfile.MappingConfigurationSetups().Map<List<UserProfileDto>>(allUsers);

            //Apply Sort
            if (input.PagedResultDto != null)
            {
                userDto = Sort(input.PagedResultDto.Sort, input.PagedResultDto.SortOrder, userDto);

                // Apply search
                if (!string.IsNullOrEmpty(input.PagedResultDto.Search))
                {
                    userDto = userDto.Where(p =>  p.userEmail != null && p.userEmail.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.FirstName != null && p.FirstName.ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.LastName != null && p.LastName.ToLower().ToString().Contains(input.PagedResultDto.Search.ToLower())
                    || p.MiddleName != null && p.MiddleName.ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.PhoneNumber != null && p.PhoneNumber.ToLower().ToString().Contains(input.PagedResultDto.Search.ToLower())
                    ).ToList();

                }
            }

            return userDto;

        }


        public List<UserProfileDto> Sort(string order, string orderDir, List<UserProfileDto> data)
        {
            // Initialization.
            List<UserProfileDto> lst = new List<UserProfileDto>();

            try
            {

                // Sorting
                switch (order)
                {
                    case "0":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.PhoneNumber).ToList()
                                                                                                 : data.OrderBy(p => p.PhoneNumber).ToList();
                        break;

                    case "1":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.userEmail).ToList()
                                                                                                 : data.OrderBy(p => p.userEmail).ToList();
                        break;

                    case "2":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.FirstName).ToList()
                                                                                                 : data.OrderBy(p => p.FirstName).ToList();
                        break;

                    case "3":

                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.LastName).ToList()
                                                                                                 : data.OrderBy(p => p.LastName).ToList();
                        break;

                    case "4":

                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.MiddleName).ToList()
                                                                                                 : data.OrderBy(p => p.MiddleName).ToList();
                        break;



                    default:

                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.userEmail).ToList()
                                                                                                 : data.OrderBy(p => p.userEmail).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {


            }

            // info.
            return lst;
        }

        public async Task<LoginResponseDto> AutheticateUser(LoginRequestDto input)
        {

            string uname = string.Empty;
            string pass = string.Empty;
            var returnProp = new LoginResponseDto();
            UserProfile userProfile = null;
            try
            {
                try
                {
                    userProfile = await _context.UserProfile.Where(p => p.userEmail.ToUpper().Equals(input.userEmail.ToUpper().Trim())).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    returnProp.ResponseCode = 400;
                    returnProp.ResponseText = string.Format("Failure to Authenticate Information. Please contact {0} local contact center", config.Value.CompanyName);
                    return returnProp;

                }
                if (userProfile == null)
                {

                    returnProp.ResponseCode = 400;
                    returnProp.ResponseText = string.Format("User Credentials Does Not Exist. Please contact {0}  contact center", config.Value.CompanyName);
                    return returnProp;

                }


                if (userProfile.AccessFailedCount >= Convert.ToInt32(config.Value.LoginCount))
                {
                    userProfile.AccessFailedCount = 1;
                    _context.UserProfile.Update(userProfile);
                    await _context.SaveChangesAsync();

                    returnProp.ResponseCode = 400;
                    returnProp.ResponseText = string.Format("User Locked. Contact administrator");
                    return returnProp;
                }

                var subscriptions = await _context.Subscription.Where(p => p.UserProfileId == userProfile.Id).FirstOrDefaultAsync();
                if (subscriptions != null)
                {
                    returnProp.SubscriptionName = subscriptions.Name;
                    returnProp.SubscriptionStartDate = subscriptions.StartDate.ToString("dddd, dd MMMM yyyy");
                    returnProp.SubscriptionEndDate = subscriptions.EndDate.ToString("dddd, dd MMMM yyyy");
                    returnProp.SubscriptionExpiryDaysLeft = subscriptions.EndDate.Day - subscriptions.StartDate.Day;
                }


                string compare = Cryptors.GetSHAHashData(input.userPassword);
                var com = await _context.UserProfile.Where(i => i.userPassword.Trim() == compare.Trim() && i.userEmail.Trim().ToUpper() == input.userEmail.Trim().ToUpper()).FirstOrDefaultAsync();
                if (com != null)
                {

                    if (userProfile.ShouldChangePasswordOnNextLogin == 1)
                    {

                        returnProp.ResponseCode = 2;
                        returnProp.ResponseText = string.Format("Enforce userPassword Change");
                        return returnProp;
                    }

                    returnProp.ResponseCode = 0;
                    returnProp.ResponseText = "Login Successful";
                    returnProp.EnforcePassChange = 0;
                    returnProp.RoleId = userProfile.RoleId;
                    returnProp.FullName = string.Format("{0} {1}", userProfile.FirstName, userProfile.LastName);
                    returnProp.UserId = userProfile.Id;

                    var subscription = await _context.Subscription.Where(p => p.UserId == userProfile.UserId).FirstOrDefaultAsync();
                    if (subscription != null)
                        returnProp.Status = subscription.Status;
                    else
                        returnProp.Status = "InActive";

                    userProfile.IsLockoutEnabled = 0;
                    userProfile.AccessFailedCount = 0;
                    userProfile.ShouldChangePasswordOnNextLogin = 0;
                    _context.UserProfile.Update(userProfile);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    if (userProfile.AccessFailedCount >= Convert.ToInt32(config.Value.LoginCount))
                    {
                        userProfile.AccessFailedCount = 1;
                        _context.UserProfile.Update(userProfile);
                        await _context.SaveChangesAsync();

                        returnProp.ResponseCode = 400;
                        returnProp.ResponseText = string.Format("User Locked. Contact administrator");
                        return returnProp;
                    }
                    if (userProfile.AccessFailedCount < Convert.ToInt32(config.Value.LoginCount))
                    {

                        userProfile.AccessFailedCount = Convert.ToInt16(userProfile.AccessFailedCount + 1);
                        _context.UserProfile.Update(userProfile);
                        await _context.SaveChangesAsync();

                        returnProp.ResponseCode = 3;
                        returnProp.ResponseText = "Invalid Login Id/userPassword.Enter userPassword (" + userProfile.AccessFailedCount + "/" + Convert.ToInt32(config.Value.LoginCount) + ")";
                        return returnProp;
                    }
                }


            }

            catch (Exception ex)
            {
                returnProp.ResponseCode = 400;
                returnProp.ResponseText = string.Format("Failure to Authenticate Information. Please contact {0} local contact center", config.Value.CompanyName);
                return returnProp;
            }
            return returnProp;


        }



    }
}
