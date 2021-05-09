using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BustleApp_api.ShoppingListAggregate.Dtos
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public int? UserId { get; set; }
        public string Status { get; set; }
    }
}
