using FullStackWebAppForRestaurants.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FullStackWebAppForRestaurants.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductIngredient> ProductIngredients { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductIngredient>()
            .HasKey(pi => new { pi.ProductId, pi.IngredientId });

        modelBuilder.Entity<ProductIngredient>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductIngredients)
            .HasForeignKey(pi => pi.ProductId);

        modelBuilder.Entity<ProductIngredient>()
            .HasOne(pi => pi.Ingredient)
            .WithMany(i => i.ProductIngredients)
            .HasForeignKey(pi => pi.IngredientId);

        //Seed Data
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Appetizer" },
            new Category { CategoryId = 2, Name = "Entree" },
            new Category { CategoryId = 3, Name = "Side Dish" },
            new Category { CategoryId = 4, Name = "Dessert" },
            new Category { CategoryId = 5, Name = "Beverage" }
            );

        //Seed Data
        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { IngredientId = 1, Name = "Beef" },
            new Ingredient { IngredientId = 2, Name = "Chicken" },
            new Ingredient { IngredientId = 3, Name = "Fish" },
            new Ingredient { IngredientId = 4, Name = "Tortilla" },
            new Ingredient { IngredientId = 5, Name = "Lettuce" },
            new Ingredient { IngredientId = 6, Name = "Tomato" }
            );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductId = 1,
                Name = "Beef Taco",
                Price = 2.50m,
                Description = "A delicious beef taco",
                Stock = 100,
                CategoryId = 2
            },
            new Product
            {
                ProductId = 2,
                Name = "Chicken Taco",
                Price = 1.99m,
                Description = "A delicious chicken taco",
                Stock = 101,
                CategoryId = 2
            },
            new Product
            {
                ProductId = 3,
                Name = "Fish Taco",
                Price = 1.99m,
                Description = "A delicious fish taco",
                Stock = 90,
                CategoryId = 2
            }
            );

        modelBuilder.Entity<ProductIngredient>().HasData(
            new ProductIngredient { ProductId = 1, IngredientId = 1 },
            new ProductIngredient { ProductId = 1, IngredientId = 4 },
            new ProductIngredient { ProductId = 1, IngredientId = 5 },
            new ProductIngredient { ProductId = 1, IngredientId = 6 },
            new ProductIngredient { ProductId = 2, IngredientId = 2 },
            new ProductIngredient { ProductId = 2, IngredientId = 4 },
            new ProductIngredient { ProductId = 2, IngredientId = 5 },
            new ProductIngredient { ProductId = 2, IngredientId = 6 },
            new ProductIngredient { ProductId = 3, IngredientId = 3 },
            new ProductIngredient { ProductId = 3, IngredientId = 4 },
            new ProductIngredient { ProductId = 3, IngredientId = 5 },
            new ProductIngredient { ProductId = 3, IngredientId = 6 }
            );

    }

}