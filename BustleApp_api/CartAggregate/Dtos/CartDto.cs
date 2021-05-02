using System;
using BustleApp_api.Domain.Utilities;

namespace BustleApp_api.CartAggregate.Dtos
{
    public class CartDto
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public string Description { get; set; }
        public int? UserProfileId { get; set; }
        public string DateCreated { get; set; }
        public int? UserId { get; set; }
        public string Status { get; set; }
        public string InventoryName { get; set; }
        public string Price { get; set; }
        public int? Quantity { get; set; }
        public string Fullname { get; set; }
        public PagedResultDto PagedResultDto { get; set; }
    }
}
