using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BustleApp_api.CartAggregate;
using BustleApp_api.CartAggregate.Dtos;
using BustleApp_api.Repository.DatabaseContext;
using BustleApp_api.Repository.Implementations;
using BustleApp_api.Repository.MappingConfigurations;
using BustleApp_api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BustleApp_api.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(BustleContext context) : base(context)
        {

        }

        public async Task<CartDto> GetCartForView(int Id)
        {
            var tags = await _context.Cart.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (tags != null)
            {
                return MappingProfile.MappingConfigurationSetups().Map<CartDto>(tags);
            }
            return new CartDto();
        }

        public async Task<CartDto> GetCartForEdit(CartDto input)
        {
            var users = await _context.Cart.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (users != null)
            {
                Cart tagDto = MappingProfile.MappingConfigurationSetups().Map<Cart>(input);
                _context.Cart.Update(tagDto);
                await _context.SaveChangesAsync();

                return MappingProfile.MappingConfigurationSetups().Map<CartDto>(tagDto);
            }
            return new CartDto();
        }

        public async Task<int> DeleteCart(int Id)
        {
            var tags = await _context.Inventory.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (tags != null)
            {
                _context.Inventory.Remove(tags);
                return await _context.SaveChangesAsync();

            }
            return 0;
        }


        public async Task CreateOrEditCart(CartDto input)
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

        protected virtual async Task Create(CartDto input)
        {
            Cart tagDto = MappingProfile.MappingConfigurationSetups().Map<Cart>(input);
            _context.Cart.Add(tagDto);
            await _context.SaveChangesAsync();

        }

        protected virtual async Task Update(CartDto input)
        {
            var tags = await _context.Cart.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (tags != null)
            {
                Cart tagDto = MappingProfile.MappingConfigurationSetups().Map<Cart>(input);
                _context.Cart.Update(tagDto);
                await _context.SaveChangesAsync();
            }

        }

        public List<CartDto> GetAllCart(CartDto input)
        {

            var query = (from cart in _context.Cart.ToList()
                         join inventory in _context.Inventory.ToList()
                             on cart.InventoryId equals inventory.Id

                         join user in _context.UserProfile.ToList()
                        on cart.UserProfileId equals user.Id
                         select new CartDto
                         {
                             Description = cart.Description,
                             Price = Formatters.FormatAmount(cart.Price),
                             Quantity = cart.Quantity,
                             InventoryId = cart.InventoryId,
                             InventoryName = inventory.Description,
                             DateCreated = cart.DateCreated.ToString("dddd, dd MMMM yyyy"),
                             Status = cart.Status,
                             UserId = cart.UserId,
                             Fullname = string.Format("{0} {1}", user.FirstName, user.LastName)


                         }).ToList().Skip((input.PagedResultDto.Page - 1) * input.PagedResultDto.SkipCount).Take(input.PagedResultDto.MaxResultCount);

            // Map Records
            List<CartDto> invDto = MappingProfile.MappingConfigurationSetups().Map<List<CartDto>>(query);

            //Apply Sort
            invDto = Sort(input.PagedResultDto.Sort, input.PagedResultDto.SortOrder, invDto);

            // Apply search
            if (!string.IsNullOrEmpty(input.PagedResultDto.Search))
            {
                invDto = invDto.Where(p => p.Status != null && p.Status.ToLower().ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                || p.Description != null && p.Description.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                || p.Price != null && p.Price.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                || p.Quantity != null && p.Quantity.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                || p.Fullname != null && p.Fullname.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                || p.Quantity != null && p.Quantity.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                || p.DateCreated != null && p.DateCreated.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                || p.InventoryName != null && p.InventoryName.ToString().ToLower().ToString().Contains(input.PagedResultDto.Search.ToLower())
                ).ToList();

            }
            return invDto;

        }


        public List<CartDto> Sort(string order, string orderDir, List<CartDto> data)
        {
            // Initialization.
            List<CartDto> lst = new List<CartDto>();

            try
            {

                // Sorting
                switch (order)
                {
                    case "0":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Description).ToList()
                                                                                                 : data.OrderBy(p => p.Description).ToList();
                        break;

                    case "1":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Price).ToList()
                                                                                                 : data.OrderBy(p => p.Price).ToList();
                        break;

                    case "2":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Fullname).ToList()
                                                                                                 : data.OrderBy(p => p.Fullname).ToList();
                        break;


                    case "3":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.InventoryName).ToList()
                                                                                                 : data.OrderBy(p => p.InventoryName).ToList();
                        break;

                    case "4":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Quantity).ToList()
                                                                                                 : data.OrderBy(p => p.Quantity).ToList();
                        break;

                    default:

                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Description).ToList()
                                                                                                 : data.OrderBy(p => p.Description).ToList();
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
