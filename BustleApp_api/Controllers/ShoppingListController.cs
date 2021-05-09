using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BustleApp_api.CartAggregate.Dtos;
using BustleApp_api.Domain.Interfaces;
using BustleApp_api.Domain.UserProfileAggregate.Dtos;
using BustleApp_api.InventoryAggregate.Dtos;
using BustleApp_api.ShoppingListAggregate.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BustleApp_api.Controllers
{
    

    public class ShoppingListController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET: /<Carts>/
        [HttpGet]
        [Route("GetShoppingListForView")]
        public async Task<ShoppingListDto> GetShoppingListForView(int Id)
        {
            return await _unitOfWork.ShoppingLists.GetShoppingListForView(Id);
        }

        [HttpPost]
        [Route("CreateOrEditShoppingList")]
        public async Task CreateOrEditShoppingList(ShoppingListDto input)
        {
            await _unitOfWork.ShoppingLists.CreateOrEditShoppingList(input);
        }

        [HttpGet]
        [Route("GetShoppingListForEdit")]
        public async Task GetShoppingListForEdit(ShoppingListDto input)
        {
            await _unitOfWork.ShoppingLists.GetShoppingListForEdit(input);
        }

        [HttpPost]
        [Route("DeleteShoppingList")]
        public async Task DeleteShoppingList(int Id)
        {
            await _unitOfWork.ShoppingLists.DeleteShoppingList(Id);
        }

        [HttpGet]
        [Route("GetAllShoppingList")]
        public List<ShoppingListDto> GetAllShoppingList(ShoppingListDto input)
        {
            return _unitOfWork.ShoppingLists.GetAllShoppingList(input);
        }
    }
}
