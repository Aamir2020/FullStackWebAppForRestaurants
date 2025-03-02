namespace FullStackWebAppForRestaurants.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public String Name { get; set; }
        public ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}