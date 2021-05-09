using BustleApp_api.Domain.Interfaces;
using BustleApp_api.InventoryAggregate.Dtos;
using BustleApp_api.Repository.DatabaseContext;
using BustleApp_api.Repository.Implementations;
using BustleApp_api.Repository.MappingConfigurations;
using BustleApp_api.ShoppingListAggregate;
using BustleApp_api.ShoppingListAggregate.Dtos;
using BustleApp_api.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BustleApp_api.Repositories
{
   

    public class ShoppingListRepository : GenericRepository<ShoppingList>, IShoppingListRepository
    {
        public ShoppingListRepository(BustleContext context) : base(context)
        {

        }

        public ShoppingListDto GetShoppingDetails(int id)
        {
            try
            {
                var query = (from shopping in _context.ShoppingList.ToList().Where(o => o.Id == id)
                            
                             select new ShoppingListDto
                             {
                                 Description = shopping.Description,
                                 Price = Formatters.FormatAmount(shopping.Price),
                                 Quantity = shopping.Quantity,
                                 DateCreated = shopping.DateCreated.ToString("dddd, dd MMMM yyyy"),
                                 Status = shopping.Status,
                                 UserId = shopping.UserId,


                             }).ToList().FirstOrDefault();

                return query;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<ShoppingListDto> GetShoppingListForView(int Id)
        {
            var rec = await _context.ShoppingList.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (rec != null)
            {
                var records = GetShoppingDetails(Id);

                return MappingProfile.MappingConfigurationSetups().Map<ShoppingListDto>(records);
            }
            return new ShoppingListDto();
        }

        public async Task<ShoppingListDto> GetShoppingListForEdit(ShoppingListDto input)
        {
            var list = await _context.ShoppingList.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (list != null)
            {
                decimal price = 0.0m;
                DateTime dt = new DateTime();
                ShoppingList shoppingList = new ShoppingList
                {
                    Description = input.Description,
                    Name = input.Name,
                    Quantity = input.Quantity,
                    Id = (int)input.Id,
                    UserId = input.UserId,
                    Price = decimal.TryParse(input.Price, out price) ? price : 0.00m,
                    DateCreated =DateTime.TryParse(input.DateCreated, out dt)?dt:DateTime.Now,
                    Status = input.Status
                };

                _context.ShoppingList.Update(shoppingList);
                await _context.SaveChangesAsync();

                return MappingProfile.MappingConfigurationSetups().Map<ShoppingListDto>(shoppingList);
            }
            return new ShoppingListDto();
        }

        public async Task<int> DeleteShoppingList(int Id)
        {
            var shoppingList = await _context.ShoppingList.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (shoppingList != null)
            {
                _context.ShoppingList.Remove(shoppingList);
                return await _context.SaveChangesAsync();

            }
            return 0;
        }

        public async Task UpdateInventory(int Id)
        {
            var shoppingList = await _context.ShoppingList.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (shoppingList != null)
            {
                Inventory inventory = new Inventory();
                inventory.DateCreated = DateTime.Now;
                inventory.Status = "Active";
                inventory.Name = shoppingList.Name;
                inventory.Quantity = shoppingList.Quantity;
                inventory.SellingPrice = shoppingList.Price;
                inventory.Quantity = shoppingList.Quantity;
                inventory.UserId = shoppingList.UserId;
                inventory.CostPrice = shoppingList.Price;
                inventory.Description = shoppingList.Description;
                _context.Inventory.Add(inventory);
                await _context.SaveChangesAsync();

            }

        }


        public async Task CreateOrEditShoppingList(ShoppingListDto input)
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

        protected virtual async Task Create(ShoppingListDto input)
        {
            ShoppingList list = MappingProfile.MappingConfigurationSetups().Map<ShoppingList>(input);
            list.DateCreated = DateTime.Now;
            list.Status = "Active";
            _context.ShoppingList.Add(list);
            await _context.SaveChangesAsync();

        }

        protected virtual async Task Update(ShoppingListDto input)
        {
            var shoppingRec = await _context.ShoppingList.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            if (shoppingRec != null)
            {
                decimal price = 0.0m;
                DateTime dt = new DateTime();
                ShoppingList shoppingList = new ShoppingList
                {
                    Description = input.Description,
                    Name = input.Name,
                    Quantity = input.Quantity,
                    Id = (int)input.Id,
                    UserId = input.UserId,
                    Price = decimal.TryParse(input.Price, out price) ? price : 0.00m,
                    DateCreated = DateTime.TryParse(input.DateCreated, out dt) ? dt : DateTime.Now,
                    Status = input.Status
                };
                shoppingRec = shoppingList;
                _context.ShoppingList.Update(shoppingRec);
                await _context.SaveChangesAsync();
            }

        }

        public List<ShoppingListDto> GetAllShoppingList(ShoppingListDto input)
        {

            var query = (from shopping in _context.ShoppingList.ToList()
                        
                         select new ShoppingListDto
                         {
                             Description = shopping.Description,
                             Price = Formatters.FormatAmount(shopping.Price),
                             Quantity = shopping.Quantity,
                             DateCreated = shopping.DateCreated.ToString("dddd, dd MMMM yyyy"),
                             Status = shopping.Status,
                             UserId = shopping.UserId,
                         }).ToList();

            // Map Records
            List<ShoppingListDto> invDto = MappingProfile.MappingConfigurationSetups().Map<List<ShoppingListDto>>(query);

            if (input.PagedResultDto != null)
            {
                //Apply Sort
                invDto = Sort(input.PagedResultDto.Sort, input.PagedResultDto.SortOrder, invDto);

                // Apply search
                if (!string.IsNullOrEmpty(input.PagedResultDto.Search))
                {
                    invDto = invDto.Where(p => p.Status != null && p.Status.ToLower().ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.Description != null && p.Description.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.Price != null && p.Price.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.Quantity != null && p.Quantity.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.Name != null && p.Name.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.Quantity != null && p.Quantity.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    || p.DateCreated != null && p.DateCreated.ToString().ToLower().Contains(input.PagedResultDto.Search.ToLower())
                    ).ToList();

                }
            }
            return invDto;

        }


        public List<ShoppingListDto> Sort(string order, string orderDir, List<ShoppingListDto> data)
        {
            // Initialization.
            List<ShoppingListDto> lst = new List<ShoppingListDto>();

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
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Name).ToList()
                                                                                                 : data.OrderBy(p => p.Name).ToList();
                        break;


                    case "3":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.DateCreated).ToList()
                                                                                                 : data.OrderBy(p => p.DateCreated).ToList();
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
