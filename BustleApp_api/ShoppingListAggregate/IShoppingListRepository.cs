using BustleApp_api.Domain.Interfaces;
using BustleApp_api.ShoppingListAggregate.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BustleApp_api.ShoppingListAggregate
{
    public interface IShoppingListRepository : IGenericRepository<ShoppingList>
    {
        Task<ShoppingListDto> GetShoppingListForView(int Id);
        Task CreateOrEditShoppingList(ShoppingListDto input);
        Task<ShoppingListDto> GetShoppingListForEdit(ShoppingListDto input);
        Task<int> DeleteShoppingList(int Id);
        Task UpdateInventory(int Id);
        List<ShoppingListDto> GetAllShoppingList(ShoppingListDto input);
    }

}
