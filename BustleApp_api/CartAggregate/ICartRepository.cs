using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BustleApp_api.CartAggregate.Dtos;
using BustleApp_api.Domain.Interfaces;

namespace BustleApp_api.CartAggregate
{
   

    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<CartDto> GetCartForView(int Id);
        Task CreateOrEditCart(CartDto input);
        Task<CartDto> GetCartForEdit(CartDto input);
        Task<int> DeleteCart(int Id);
        List<CartDto> GetAllCart(CartDto input);
    }
}
