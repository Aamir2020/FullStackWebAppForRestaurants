using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FullStackWebAppForRestaurants.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public String Name { get; set; }

        [ValidateNever]
        public ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}