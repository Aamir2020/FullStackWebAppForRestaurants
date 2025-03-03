using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using FullStackWebAppForRestaurants.Controllers;
using FullStackWebAppForRestaurants.Data;
using FullStackWebAppForRestaurants.Models;

namespace FullStackWebAppForRestaurants.Tests
{
    public class ProductControllerTest : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductController _controller;
        private readonly string _webRootPath = Path.GetTempPath();

        public ProductControllerTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(m => m.WebRootPath).Returns(_webRootPath);

            _controller = new ProductController(_context, envMock.Object);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithProducts()
        {
            _context.Add(new Product { Name = "Product1", Price = 10, Stock = 5, CategoryId = 1 });
            await _context.SaveChangesAsync();

            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task AddEdit_Get_WithIdZero_ReturnsAddView_WithNewProduct()
        {
            var result = await _controller.AddEdit(0);

            var viewResult = Assert.IsType<ViewResult>(result);

            var operation = viewResult.ViewData["Operation"] as string;
            Assert.Equal("Add", operation);
            Assert.IsType<Product>(viewResult.Model);
        }

        [Fact]
        public async Task AddEdit_Get_WithValidId_ReturnsEditView_WithProduct()
        {

            var category = new Category { CategoryId = 1, Name = "TestCategory" };
            _context.Add(category);
            await _context.SaveChangesAsync();

            var product = new Product
            {
                Name = "Product1",
                Price = 10,
                Stock = 5,
                CategoryId = category.CategoryId,
                Category = category,
                ProductIngredients = new List<ProductIngredient>() // ensure this is not null
            };
            _context.Add(product);
            await _context.SaveChangesAsync();

            var result = await _controller.AddEdit(product.ProductId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var operation = viewResult.ViewData["Operation"] as string;
            Assert.Equal("Edit", operation);
            var model = Assert.IsType<Product>(viewResult.Model);
            Assert.Equal(product.ProductId, model.ProductId);
        }

        [Fact]
        public async Task AddEdit_Post_Create_NewProduct_RedirectsToIndex()
        {
            var product = new Product { ProductId = 0, Name = "New Product", Price = 20, Stock = 10 };
            int[] ingredientIds = new int[] { 1, 2 };
            int catId = 1;

            var result = await _controller.AddEdit(product, ingredientIds, catId);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var addedProduct = await _context.Set<Product>().FirstOrDefaultAsync(p => p.Name == "New Product");
            Assert.NotNull(addedProduct);
            Assert.Equal(catId, addedProduct.CategoryId);
        }

        [Fact]
        public async Task AddEdit_Post_Update_Product_RedirectsToIndex()
        {
            var product = new Product { Name = "Existing Product", Price = 30, Stock = 15, CategoryId = 1 };
            await _context.Set<Product>().AddAsync(product);
            await _context.SaveChangesAsync();

            int productId = product.ProductId;
            product.Name = "Updated Product";
            int[] ingredientIds = new int[] { 3, 4 };
            int catId = 2;

            var result = await _controller.AddEdit(product, ingredientIds, catId);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var updatedProduct = await _context.Set<Product>().FindAsync(productId);
            Assert.NotNull(updatedProduct);
            Assert.Equal("Updated Product", updatedProduct.Name);
            Assert.Equal(catId, updatedProduct.CategoryId);
        }

        [Fact]
        public async Task Delete_Post_ValidId_RedirectsToIndex()
        {
            var product = new Product { Name = "ToDelete", Price = 50, Stock = 20, CategoryId = 1 };
            await _context.Set<Product>().AddAsync(product);
            await _context.SaveChangesAsync();

            var result = await _controller.Delete(product.ProductId);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var deletedProduct = await _context.Set<Product>().FindAsync(product.ProductId);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public async Task Delete_Post_InvalidId_RedirectsToIndex()
        {
            var result = await _controller.Delete(999); // non-existent id

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

    }
}
