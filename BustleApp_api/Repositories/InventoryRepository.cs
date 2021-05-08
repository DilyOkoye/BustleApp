using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BustleApp_api.Domain.SubscriptionAggregate.Dtos;
using BustleApp_api.InventoryAggregate;
using BustleApp_api.InventoryAggregate.Dtos;
using BustleApp_api.Repository.DatabaseContext;
using BustleApp_api.Repository.Implementations;
using BustleApp_api.Repository.MappingConfigurations;
using BustleApp_api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BustleApp_api.Repositories
{
    public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(BustleContext context) : base(context)
        {

        }

        public InventoryDto GetInventoryDetails(int id)
        {
            try
            {
                var query = (from Inventory in _context.Inventory.ToList()

                             select new InventoryDto
                             {
                                 Name = Inventory.Name,
                                 Description = Inventory.Description,
                                 Id = Inventory.Id,
                                 CostPrice = Formatters.FormatAmount(Inventory.CostPrice),
                                 SellingPrice = Formatters.FormatAmount(Inventory.SellingPrice),
                                 Quantity = Inventory.Quantity,
                                 DateCreated = Inventory.DateCreated,
                                 Status = Inventory.Status,
                                 UserId = Inventory.UserId

                             }).ToList().FirstOrDefault();

                return query;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<InventoryDto> GetInventoryForView(int Id)
        {
            var tags = await _context.Inventory.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (tags != null)
            {
                var records = GetInventoryDetails(Id);

                return MappingProfile.MappingConfigurationSetups().Map<InventoryDto>(records);
            }
            return new InventoryDto();
        }

        public async Task<InventoryDto> GetInventoryForEdit(InventoryDto input)
        {
            var users = await _context.Inventory.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (users != null)
            {
                Inventory tagDto = MappingProfile.MappingConfigurationSetups().Map<Inventory>(input);
                _context.Inventory.Update(tagDto);
                await _context.SaveChangesAsync();

                return MappingProfile.MappingConfigurationSetups().Map<InventoryDto>(tagDto);
            }
            return new InventoryDto();
        }

        public async Task<int> DeleteInventory(int Id)
        {
            var tags = await _context.Inventory.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (tags != null)
            {
                _context.Inventory.Remove(tags);
                return await _context.SaveChangesAsync();

            }
            return 0;
        }


        public async Task CreateOrEditInventory(InventoryDto input)
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

        protected virtual async Task Create(InventoryDto input)
        {
            Inventory inventory = MappingProfile.MappingConfigurationSetups().Map<Inventory>(input);
            inventory.DateCreated = DateTime.Now;
            inventory.Status = "Active";
            _context.Inventory.Add(inventory);
            await _context.SaveChangesAsync();

        }

        protected virtual async Task Update(InventoryDto input)
        {
            var tags = await _context.Inventory.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (tags != null)
            {
                Inventory tagDto = MappingProfile.MappingConfigurationSetups().Map<Inventory>(input);
                _context.Inventory.Update(tagDto);
                await _context.SaveChangesAsync();
            }

        }

        public List<InventoryDto> GetAllInventory(InventoryDto input)
        {

            var query = (from Inventory in _context.Inventory.ToList()

                         select new InventoryDto
                         {
                             Name = Inventory.Name,
                             Description = Inventory.Description,
                             Id = Inventory.Id,
                             CostPrice = Formatters.FormatAmount(Inventory.CostPrice),
                             SellingPrice = Formatters.FormatAmount(Inventory.SellingPrice),
                             Quantity = Inventory.Quantity,
                             DateCreated = Inventory.DateCreated,
                             Status = Inventory.Status,
                             UserId = Inventory.UserId

                         }).ToList();

            // Map Records
            List<InventoryDto> invDto = MappingProfile.MappingConfigurationSetups().Map<List<InventoryDto>>(query);

            if (input.PagedResultDto != null)
            {
                //Apply Sort
                invDto = Sort(input.PagedResultDto.Sort, input.PagedResultDto.SortOrder, invDto);

                // Apply search
                if (!string.IsNullOrEmpty(input.PagedResultDto.Search))
                {
                    invDto = invDto.Where(p => p.Status != null && p.Status.ToLower().ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.Name != null && p.Name.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.Description != null && p.Name.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.CostPrice != null && p.CostPrice.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.SellingPrice != null && p.CostPrice.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.Quantity != null && p.Quantity.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.DateCreated != null && p.DateCreated.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.Name != null && p.Name.ToString().ToLower().ToString().Contains(input.PagedResultDto.Search.ToLower())
                    ).ToList();

                }
            }
            return invDto;

        }


        public List<InventoryDto> Sort(string order, string orderDir, List<InventoryDto> data)
        {
            // Initialization.
            List<InventoryDto> lst = new List<InventoryDto>();

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
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Description).ToList()
                                                                                                 : data.OrderBy(p => p.Description).ToList();
                        break;

                    case "2":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CostPrice).ToList()
                                                                                                 : data.OrderBy(p => p.CostPrice).ToList();
                        break;


                    case "3":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.SellingPrice).ToList()
                                                                                                 : data.OrderBy(p => p.SellingPrice).ToList();
                        break;

                    case "4":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Quantity).ToList()
                                                                                                 : data.OrderBy(p => p.Quantity).ToList();
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
