using FullStackWebAppForRestaurants.Data;
using FullStackWebAppForRestaurants.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullStackWebAppForRestaurants.Controllers
{
    public class IngredientController : Controller
    {
        private Repository<Ingredient> ingredients;

        public IngredientController(ApplicationDbContext context)
        {
            ingredients = new Repository<Ingredient>(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await ingredients.GetAllAsync());
        }
    }
}
