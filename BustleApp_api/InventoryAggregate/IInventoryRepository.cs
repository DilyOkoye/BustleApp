using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BustleApp_api.Domain.Interfaces;
using BustleApp_api.InventoryAggregate.Dtos;

namespace BustleApp_api.InventoryAggregate
{
   

    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
        Task<InventoryDto> GetInventoryForView(int Id);
        Task CreateOrEditInventory(InventoryDto input);
        Task<InventoryDto> GetInventoryForEdit(InventoryDto input);
        Task<int> DeleteInventory(int Id);
        List<InventoryDto> GetAllInventory(InventoryDto input);
    }
}
