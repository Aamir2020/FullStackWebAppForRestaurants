namespace FullStackWebAppForRestaurants.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public String Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}