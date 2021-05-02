using System;
using BustleApp_api.Domain.Utilities;

namespace BustleApp_api.InventoryAggregate.Dtos
{
    public class InventoryDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CostPrice { get; set; }
        public string SellingPrice { get; set; }
        public int? Quantity { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UserId { get; set; }
        public string Status { get; set; }
        public PagedResultDto PagedResultDto { get; set; }
    }
}
