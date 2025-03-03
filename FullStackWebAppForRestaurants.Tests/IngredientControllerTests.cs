using FullStackWebAppForRestaurants.Controllers;
using FullStackWebAppForRestaurants.Data;
using FullStackWebAppForRestaurants.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackWebAppForRestaurants.Tests
{
    public class IngredientControllerTests
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _context;

        public IngredientControllerTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));
            serviceCollection.AddScoped<Repository<Ingredient>>();
            serviceCollection.AddScoped<IngredientController>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
            _context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            var controller = _serviceProvider.GetRequiredService<IngredientController>();

            var result = controller.Create();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName); // Checks if the default view is returned
        }

        [Fact]
        public async Task Create_Post_InvalidModelState_ReturnsViewResult()
        {
            var controller = _serviceProvider.GetRequiredService<IngredientController>();
            controller.ModelState.AddModelError("Name", "Required");
            var ingredient = new Ingredient { Name = "" }; // Invalid ingredient

            var result = await controller.Create(ingredient);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(ingredient, viewResult.Model);
        }

        [Fact]
        public async Task Create_Post_ValidModelState_RedirectsToIndex()
        {
            var controller = _serviceProvider.GetRequiredService<IngredientController>();
            var ingredient = new Ingredient { Name = "Test Ingredient" };

            var result = await controller.Create(ingredient);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName);
        }

        [Fact]
        public async Task Edit_Get_ReturnsViewResult_WithIngredient()
        {

            var controller = _serviceProvider.GetRequiredService<IngredientController>();
            var ingredient = new Ingredient { Name = "Test Ingredient" };
            _context.Add(ingredient);
            await _context.SaveChangesAsync();

            var result = await controller.Edit(ingredient.IngredientId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Ingredient>(viewResult.Model);
            Assert.Equal(ingredient.Name, model.Name);
        }

        [Fact]
        public async Task Delete_Get_ReturnsViewResult_WithIngredient()
        {
            var controller = _serviceProvider.GetRequiredService<IngredientController>();
            var ingredient = new Ingredient { Name = "Test Ingredient" };
            _context.Add(ingredient);
            await _context.SaveChangesAsync();

            var result = await controller.Delete(ingredient.IngredientId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Ingredient>(viewResult.Model);
            Assert.Equal(ingredient.Name, model.Name);
        }

        [Fact]
        public async Task Delete_Post_RemovesIngredient_RedirectsToIndex()
        {
            var controller = _serviceProvider.GetRequiredService<IngredientController>();
            var ingredient = new Ingredient { Name = "Test Ingredient" };
            _context.Add(ingredient);
            await _context.SaveChangesAsync();

            var result = await controller.Delete(ingredient);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName);

            var deletedIngredient = await _context.Ingredients.FindAsync(ingredient.IngredientId);
            Assert.Null(deletedIngredient);
        }


    }
}
