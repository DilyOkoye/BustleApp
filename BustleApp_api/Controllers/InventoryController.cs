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
    public class InventoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public InventoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET: /<User Inventory>/
        [HttpGet]
        [Route("GetInventoryForView")]
        public async Task<InventoryDto> GetInventoryForView(int Id)
        {
            return await _unitOfWork.Inventories.GetInventoryForView(Id);
        }

        [HttpPost]
        [Route("CreateOrEditInventory")]
        public async Task CreateOrEditInventory(InventoryDto input)
        {
            await _unitOfWork.Inventories.CreateOrEditInventory(input);
        }

        [HttpGet]
        [Route("GetInventoryForEdit")]
        public async Task GetInventoryForEdit(InventoryDto input)
        {
            await _unitOfWork.Inventories.GetInventoryForEdit(input);
        }

        [HttpPost]
        [Route("DeleteInventory")]
        public async Task DeleteInventory(int Id)
        {
            await _unitOfWork.Inventories.DeleteInventory(Id);
        }

        [HttpGet]
        [Route("GetAllInventory")]
        public List<InventoryDto> GetAllInventory(InventoryDto input)
        {
            return _unitOfWork.Inventories.GetAllInventory(input);
        }

    }
}
