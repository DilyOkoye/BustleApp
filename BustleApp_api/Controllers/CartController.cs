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
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET: /<Carts>/
        [HttpGet]
        [Route("GetCartForView")]
        public async Task<CartDto> GetCartForView(int Id)
        {
            return await _unitOfWork.Carts.GetCartForView(Id);
        }

        [HttpPost]
        [Route("CreateOrEditCart")]
        public async Task CreateOrEditCart(CartDto input)
        {
            await _unitOfWork.Carts.CreateOrEditCart(input);
        }

        [HttpGet]
        [Route("GetCartForEdit")]
        public async Task GetCartForEdit(CartDto input)
        {
            await _unitOfWork.Carts.GetCartForEdit(input);
        }

        [HttpPost]
        [Route("DeleteCart")]
        public async Task DeleteCart(int Id)
        {
            await _unitOfWork.Carts.DeleteCart(Id);
        }

        [HttpGet]
        [Route("GetAllCart")]
        public List<CartDto> GetAllCart(CartDto input)
        {
            return _unitOfWork.Carts.GetAllCart(input);
        }
    }
}
