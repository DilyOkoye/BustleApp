using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BustleApp_api.Domain.SubscriptionAggregate;
using BustleApp_api.Domain.SubscriptionAggregate.Dtos;
using BustleApp_api.Repository.DatabaseContext;
using BustleApp_api.Repository.Implementations;
using BustleApp_api.Repository.MappingConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BustleApp_api.Repository.Repositories
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(BustleContext context) : base(context)
        {

        }

        public async Task<SubscriptionDto> GetSubscriptionForView(int Id)
        {
            var tags = await _context.Subscription.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (tags != null)
            {
                return MappingProfile.MappingConfigurationSetups().Map<SubscriptionDto>(tags);
            }
            return new SubscriptionDto();
        }

        public async Task<SubscriptionDto> GetSubscriptionForEdit(SubscriptionDto input)
        {
            var users = await _context.Subscription.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (users != null)
            {
                Subscription sub = new Subscription
                {
                    Description = input.Description,
                    EndDate = input.EndDate,
                    DateCreated = input.DateCreated,
                    Id = (int)input.Id,
                    Name = input.Name,
                    StartDate = input.StartDate,
                    Status = input.Status,
                };

                _context.Subscription.Update(sub);
                await _context.SaveChangesAsync();

                return MappingProfile.MappingConfigurationSetups().Map<SubscriptionDto>(sub);
            }
            return new SubscriptionDto();
        }

        public async Task<int> DeleteSubscription(int Id)
        {
            var tags = await _context.Subscription.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (tags != null)
            {
                _context.Subscription.Remove(tags);
                return await _context.SaveChangesAsync();

            }
            return 0;
        }


        public async Task CreateOrEditSubscription(SubscriptionDto input)
        {
            if (input.Id == null || input.Id == 0)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }

        }

        protected virtual async Task Create(SubscriptionDto input)
        {
            Subscription tagDto = MappingProfile.MappingConfigurationSetups().Map<Subscription>(input);
            _context.Subscription.Add(tagDto);
            await _context.SaveChangesAsync();

        }

        protected virtual async Task Update(SubscriptionDto input)
        {
            var tags = await _context.Subscription.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (tags != null)
            {
                Subscription sub = new Subscription
                {
                    Description = input.Description,
                    EndDate = input.EndDate,
                    DateCreated = input.DateCreated,
                    Id = (int)input.Id,
                    Name = input.Name,
                    StartDate = input.StartDate,
                    Status = input.Status,
                };
                _context.Subscription.Update(sub);
                await _context.SaveChangesAsync();
            }

        }

        public List<SubscriptionDto> GetAllSubscription(SubscriptionDto input)
        {

            var query = (from subscription in _context.Subscription.ToList()
                         join user in _context.UserProfile.ToList()
                              on subscription.UserProfileId equals user.Id
                         select new SubscriptionDto
                         {
                             Username = user.UserName,
                             Name = subscription.Name,
                             Id = subscription.Id,
                             DateCreated = subscription.DateCreated,
                             Status = subscription.Status,
                             UserId = subscription.UserId

                         }).ToList();

            // Map Records
            List<SubscriptionDto> ratingDto = MappingProfile.MappingConfigurationSetups().Map<List<SubscriptionDto>>(query);

            if (input.PagedResultDto != null)
            {
                //Apply Sort
                ratingDto = Sort(input.PagedResultDto.Sort, input.PagedResultDto.SortOrder, ratingDto);

                // Apply search
                if (!string.IsNullOrEmpty(input.PagedResultDto.Search))
                {
                    ratingDto = ratingDto.Where(p => p.Status != null && p.Status.ToLower().ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.Username != null && p.Username.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.DateCreated != null && p.DateCreated.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.Name != null && p.Name.ToString().ToLower().ToString().Contains(input.PagedResultDto.Search.ToLower())
                    ).ToList();

                }
            }
            return ratingDto;

        }


        public List<SubscriptionDto> Sort(string order, string orderDir, List<SubscriptionDto> data)
        {
            // Initialization.
            List<SubscriptionDto> lst = new List<SubscriptionDto>();

            try
            {

                // Sorting
                switch (order)
                {
                    case "0":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Name).ToList()
                                                                                                 : data.OrderBy(p => p.Name).ToList();
                        break;

                    case "1":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Status).ToList()
                                                                                                 : data.OrderBy(p => p.Status).ToList();
                        break;

                    case "2":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.DateCreated).ToList()
                                                                                                 : data.OrderBy(p => p.DateCreated).ToList();
                        break;


                    case "3":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Username).ToList()
                                                                                                 : data.OrderBy(p => p.Username).ToList();
                        break;

                    default:

                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Name).ToList()
                                                                                                 : data.OrderBy(p => p.Name).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {


            }

            // info.
            return lst;
        }

    }
}
