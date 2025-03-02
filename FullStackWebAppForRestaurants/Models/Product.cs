using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackWebAppForRestaurants.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public String? Name { get; set; }
        public decimal Price { get; set; }
        public String? Description { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<ProductIngredient>? ProductIngredients { get; set; }

    }
}