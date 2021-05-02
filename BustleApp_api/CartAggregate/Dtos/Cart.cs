using System;
namespace BustleApp_api.CartAggregate.Dtos
{
    public class Cart
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int InventoryId { get; set; }
        public int? UserProfileId { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserId { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string Status { get; set; }
    }
}
